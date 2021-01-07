using ProductManagementModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ProductManagementWebApi.Controllers
{
    [RoutePrefix("Category")]
    public class CategoryController : ApiController
    {
        private readonly ProductManagementContext _productManagementContext = new ProductManagementContext();

        [Route("Add")]
        [HttpPost]
        public IHttpActionResult AddCategory(Category category)
        {
            _productManagementContext.CategoryEntities.Add(category);
            _productManagementContext.SaveChanges();
            return Ok();
        }

        [HttpGet]
        [Route("List")]
        public List<string> GetCategoryList()
        {
            var list = _productManagementContext.CategoryEntities.ToList();
            List<string> CategoryList = new List<string>();
            foreach (var item in list)
            {
                CategoryList.Add(item.ProductCategory);
            }
            return CategoryList;
        }
    }
}
