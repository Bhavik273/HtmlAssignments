using ProductManagementModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ProductManagementWebApi.Controllers
{
    [RoutePrefix("Products")]
    public class ProductsController : ApiController
    {
        private readonly ProductManagementContext _productManagementContext = new ProductManagementContext();

        [Route("Add")]
        public IHttpActionResult Post(Product product)
        {
            try
            {
                var temp = _productManagementContext.ProductEntities.Add(product);
                _productManagementContext.SaveChanges();
                return Ok(temp);
            }
            catch (Exception)
            {
                return StatusCode(HttpStatusCode.InternalServerError);
            }
        }

        [Route("ListAll")]
        public List<Product> Get()
        {
            return _productManagementContext.ProductEntities.ToList();
        }

        [Route("Delete/{id}")]
        public IHttpActionResult Delete([FromUri]int id)
        {
            Product product = _productManagementContext.ProductEntities.FirstOrDefault(e => e.Id == id);
            if (product != null)
            {
                _productManagementContext.ProductEntities.Remove(product);
                _productManagementContext.SaveChanges();
                return Ok(product);
            }
            return NotFound();
        }

        [HttpGet]
        [Route("Id")]        
        public IHttpActionResult GetProductId()
        {
            int id = _productManagementContext.ProductEntities.Count()>0 ? _productManagementContext.ProductEntities.Max(entity => entity.Id)+1:1;
            var list = _productManagementContext.CategoryEntities.ToList();
            List<string> CategoryList = new List<string>();
            foreach (var item in list)
            {
                CategoryList.Add(item.ProductCategory);
            }

            return Ok(new ProductManagementMVC.Models.CategoryResponseViewModel
            {
                id = id,
                CategoryList = CategoryList
            });
        }

        [Route("Get/{id:int}")]
        public Product GetProduct([FromUri]int id)
        {
            return _productManagementContext.ProductEntities.FirstOrDefault(entity => entity.Id == id);
        }

        [Route("Update")]
        public IHttpActionResult UpdateProduct(Product entity)
        {
            var product = _productManagementContext.ProductEntities.FirstOrDefault(p => p.Id == entity.Id);
            if (product != null)
            {
                product.Name = entity.Name;
                product.Price = entity.Price;
                product.Quantity += entity.Quantity;
                product.ShortDescription = entity.ShortDescription;
                product.SmallImagePath = entity.SmallImagePath;
                product.LargeImagePath = entity.LargeImagePath;
                product.LongDescription = entity.LongDescription;
                _productManagementContext.SaveChanges();
            }
            return Ok();

        }
    }
}
