using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProductManagementModels;
using ProductManagementWebApi.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagementWebApi.Controllers.Tests
{
    [TestClass()]
    public class ProductsControllerUnitTests
    {
        [TestMethod()]
        //Test case for: Api method that returns all products list
        public void GetTest()
        {
            var controller = new ProductsController();
            var result = controller.Get();
            Assert.IsInstanceOfType(result, typeof(List<Product>));
        }


        //Test Case for: Api method that returns Product Based on Id
        //Checking if negitive Id is passed it returns null or not
        [TestMethod()]
        public void GetProductTest()
        {
            var controller = new ProductsController();
            var result = controller.GetProduct(-1);
            Assert.IsNull(result);
        }
    }
}