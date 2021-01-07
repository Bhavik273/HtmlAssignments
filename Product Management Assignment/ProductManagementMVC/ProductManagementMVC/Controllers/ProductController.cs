using ProductManagementMVC.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using PagedList;

namespace ProductManagementMVC.Controllers
{
    [Authorize]
    public class ProductController : Controller
    {
        private readonly string _SmallImagePath =  ConfigurationManager.AppSettings["SmallImagePath"].ToString();
        private readonly string _LargeImagePath = ConfigurationManager.AppSettings["LargeImagePath"].ToString();
        private readonly string _ApiServer = ConfigurationManager.AppSettings["ApiAddress"].ToString();
        private static List<ProductListView> products = null;
        private static List<string> CategoryList = null;
        // GET: Product
        public ActionResult Index()
        {
            string userName = Session["UserName"].ToString();
            if (userName == null)
                userName = "Not Found";
            ViewBag.Message = "Welcome " + userName;
            return View();
        }

        public  ActionResult AddProduct()
        {
            using(var client =  new HttpClient())
            {
                client.BaseAddress = new Uri(_ApiServer);
                //int ProductId = client.GetAsync("Products/Id").Result.Content.ReadAsAsync<int>().Result;
                //if (CategoryList == null)
                //{
                //    var list = client.GetAsync("Category/List").Result.Content.ReadAsAsync<List<string>>().Result;
                //    CategoryList = new SelectList(list);
                //}

                var result = client.GetAsync("Products/Id").Result.Content.ReadAsAsync<CategoryResponseViewModel>().Result;
                var CategoryList = new SelectList(result.CategoryList);
                ViewBag.CategoryList=CategoryList;
                ViewBag.Id = result.id;
                return View();
            }
        }

        [HttpPost]
        public ActionResult AddProduct(Product product,HttpPostedFileBase SmallImage,HttpPostedFileBase LargeImage,int ID)
        {

            if (ModelState.IsValid)
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(_ApiServer);

                    string FileName = ID + "." + Path.GetExtension(SmallImage.FileName);
                    product.SmallImagePath = _SmallImagePath + FileName;

                    if (LargeImage != null)
                    {
                        product.LargeImagePath = _LargeImagePath + ID+"."+Path.GetExtension(LargeImage.FileName);
                    }
                    var model = new ProductManagementModels.Product
                    {
                        Category = product.Category,
                        Name = product.Name,
                        Price = product.Price,
                        Quantity = product.Quantity,
                        ShortDescription = product.ShortDescription,
                        LongDescription = product.LongDescription,
                        SmallImagePath = product.SmallImagePath,
                        LargeImagePath = product.LargeImagePath
                    };
                    var result = client.PostAsJsonAsync("Products/Add", model).Result;
                    if (result.IsSuccessStatusCode)
                    {
                        SmallImage.SaveAs(Server.MapPath(product.SmallImagePath));
                        if (LargeImage != null)
                            LargeImage.SaveAs(Server.MapPath(product.LargeImagePath));
                        TempData["isSuccess"] = true;
                        TempData["MsgTitle"] = "Success!";
                        TempData["Msg"] = product.Name + " Added Successfully";

                    }
                    else
                    {
                        TempData["isSuccess"] = false;
                        TempData["MsgTitle"] = "Failure!";
                        TempData["Msg"] = product.Name + "Not Added";
                    }

                    //Reflect newly added product in the Local ProductList
                    GetProductList();
                    return RedirectToAction("AddProduct");
                }
            }
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_ApiServer);
                //int ProductId = client.GetAsync("Products/Id").Result.Content.ReadAsAsync<int>().Result;
                //if (CategoryList == null)
                //{
                //    var list = client.GetAsync("Category/List").Result.Content.ReadAsAsync<List<string>>().Result;
                //    CategoryList = new SelectList(list);
                //}

                var result = client.GetAsync("Products/Id").Result.Content.ReadAsAsync<CategoryResponseViewModel>().Result;
                var CategoryList = new SelectList(result.CategoryList);
                ViewBag.CategoryList = CategoryList;
                ViewBag.Id = result.id;
                return View();
            }
        }

        public ActionResult EditProduct(string searchString,string sortOrder, string currentFilter, int? page)
        {
            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }
            ViewBag.CurrentFilter = searchString;
            if (products == null)
            {
                //Prolem Occured while fetching list
                if (!GetProductList())

                    return View();
            }

            //Temporary list for search and sort results
            var tempList = products;

            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.PricesortParm = sortOrder == "price" ? "price_desc" : "price";
            if (!String.IsNullOrEmpty(searchString))
            {
                searchString = searchString.ToLower();
                tempList = products.Where(product=> product.Name.ToLower().Contains(searchString)).ToList();
            }
            switch (sortOrder)
            {
                case "name_desc":
                    tempList = tempList.OrderByDescending(product => product.Name).ToList();
                    break;
                case "price":
                    tempList = tempList.OrderBy(product => product.Price).ToList();
                    break;
                case "price_desc":
                    tempList = tempList.OrderByDescending(product => product.Price).ToList();
                    break;
                default:
                    tempList = tempList.OrderBy(product => product.Name).ToList();
                    break;
            }
            int pageSize = 5;
            int pageNumber = (page ?? 1);
            return View(tempList.ToPagedList(pageNumber, pageSize));
        }


        public ActionResult DeleteProduct(int id)
        {
            using(var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_ApiServer);
                var result = client.DeleteAsync("Products/Delete/" + id).Result;
                if (result.IsSuccessStatusCode)
                {
                    var product = result.Content.ReadAsAsync<ProductManagementModels.Product>().Result;
                    var SmallImage = Server.MapPath(product.SmallImagePath);
                    System.IO.File.Delete(SmallImage);
                    if (product.LargeImagePath != null)
                        System.IO.File.Delete(Server.MapPath(product.LargeImagePath));
                    
                    TempData["isSuccess"] = true;
                    TempData["Msg"] = product.Name + " Remove Successfully!";
                } else
                {
                    TempData["isSuccess"] = false;
                    TempData["Msg"] = "Delete Failed!";
                }
            }
            GetProductList();
            return RedirectToAction("EditProduct");
        }

        [ActionName("Add Category")]
        public ActionResult AddCategory()
        {
            return View("AddCategory");
        }

        [HttpPost]
        [ActionName("Add Category")]
        public ActionResult AddCategory(string category)
        {
            using(var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_ApiServer);
                var model = new ProductManagementModels.Category
                {
                    ProductCategory = category
                };
                var result = client.PostAsJsonAsync("Category/Add", model).Result;
                ModelState.Clear();
                TempData["isSuccess"] = true;
                TempData["Msg"] = "Category "+category + " Added Successfully!";
                return RedirectToAction("Add Category");
            }
        }

        public ActionResult UpdateProduct(int id)
        {
            Product product = null;
            using(var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_ApiServer);
                var result = client.GetAsync("Products/Get/" + id).Result;
                if(result.IsSuccessStatusCode)
                {
                    var model = result.Content.ReadAsAsync<ProductManagementModels.Product>().Result;
                    product = new Product
                    {
                        Name=model.Name,
                        Category=model.Category,
                        Price=model.Price,
                        Quantity=model.Quantity,
                        ShortDescription=model.ShortDescription,
                        LongDescription=model.LongDescription,
                        SmallImagePath=model.SmallImagePath,
                        LargeImagePath=model.LargeImagePath
                    };
                    ViewBag.Id = id;
                    return View(product);
                }
            }
            TempData["isSuccess"] = false;
            TempData["Msg"] = "Something went Wrong";
            return RedirectToAction("EditProduct");
        }
        [HttpPost]
        public ActionResult UpdateProduct(int id,Product product,HttpPostedFileBase SmallImage,HttpPostedFileBase LargeImage)
        {
            if (ModelState.IsValid)
            {
                if (SmallImage != null)
                {
                    System.IO.File.Delete(Server.MapPath(product.SmallImagePath));
                    string FileName = id + "." + Path.GetExtension(SmallImage.FileName);
                    product.SmallImagePath = _SmallImagePath + FileName;
                    SmallImage.SaveAs(Server.MapPath(product.SmallImagePath));
                }

                if (LargeImage != null)
                {
                    if(!String.IsNullOrEmpty(product.LargeImagePath))
                        System.IO.File.Delete(Server.MapPath(product.LargeImagePath));
                    string FileName = id + "." + Path.GetExtension(LargeImage.FileName);
                    product.LargeImagePath = _LargeImagePath + FileName;
                    LargeImage.SaveAs(Server.MapPath(product.LargeImagePath));
                }
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(_ApiServer);
                    var model = new ProductManagementModels.Product
                    {
                        Id = id,
                        Name = product.Name,
                        Category = product.Category,
                        Price = product.Price,
                        Quantity = product.Quantity,
                        ShortDescription = product.ShortDescription,
                        LongDescription = product.LongDescription,
                        LargeImagePath = product.LargeImagePath,
                        SmallImagePath = product.SmallImagePath,
                    };
                    var result = client.PostAsJsonAsync("Products/Update", model).Result;
                    if (result.IsSuccessStatusCode)
                    {
                        TempData["isSuccess"] = true;
                        TempData["Msg"] = product.Name + " Updated Successfully";
                    }
                }
                GetProductList();
                return RedirectToAction("EditProduct");
            }
            return View();
        }

        [NonAction]
        //Fetch product list from database
        public bool GetProductList()
        {
            products = new List<ProductListView>();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_ApiServer);
                var result = client.GetAsync("Products/ListAll").Result;
                if (result.IsSuccessStatusCode)
                {
                    List<ProductManagementModels.Product> productEntities = result.Content.ReadAsAsync<List<ProductManagementModels.Product>>().Result;
                    foreach (var item in productEntities)
                    {
                        products.Add(new ProductListView
                        {
                            Name = item.Name,
                            ShortDescription = item.ShortDescription,
                            SmallImagePath = item.SmallImagePath,
                            Quantity = item.Quantity,
                            Id = item.Id,
                            Category = item.Category,
                            Price = item.Price
                        });
                    }
                    return true;
                }
                else
                {
                    ViewBag.isSuccess = false;
                    ViewBag.MsgTitle = "Failed!";
                    ViewBag.Msg = "Product List Not Availale";
                    return false;
                }
            }
        }
    }
}