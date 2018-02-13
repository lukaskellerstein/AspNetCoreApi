using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.Net.Http.Headers;
using System.IO;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using TestApi.Business;
using TestApi.Models;

namespace TestApi.Controllers
{
    public class CatalogController : Controller
    {
        TestService _service;

        ILogger<CatalogController> _log;


        public CatalogController(TestService service, ILogger<CatalogController> log)
        {
            _log = log;
            _service = service;

        }


        [HttpGet]
        public Product[] GetAllProducts()
        {
            var result = _service.GetAllProducts();
            return result;
        }

        [HttpPost]
        public TestDTO GetProduct([FromBody]string id)
        {
            var result = _service.GetProduct(id);
            return result;
        }


        [HttpPost]
        public TestDTO AddProduct([FromBody]Product item)
        {
            var result = _service.AddProduct(item);
            return result;
        }


        [HttpPost]
        public TestDTO RemoveProduct([FromBody]Product item)
        {
            var result = _service.RemoveProduct(item);
            return result;
        }


        [HttpPost]
        public TestDTO UpdateProduct([FromBody]Product item)
        {
            var result = _service.UpdateProduct(item);
            return result;
        }

    }
}
