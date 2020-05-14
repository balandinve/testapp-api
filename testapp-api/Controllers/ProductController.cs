using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using testapp_api.Models;
using testapp_api.Services;

namespace testapp_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private ProductService _productService;

        public ProductController(ProductService productService)
        {
            _productService = productService;
        }
        [HttpGet]
        [Route("list")]
        public IActionResult GetAll([FromQuery]ProductFilter filter)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var items = _productService.GetAll(filter);
                    return Ok(new { Products = items.Item1, Quantity = items.Item2 });
                }
                catch (Exception ex)
                {
                    return BadRequest("Ошибка при получении списка сотрудников.");
                }
            }
            else
            {
                return BadRequest("Invalid params");
            }
        }

        [HttpGet]
        [Route("Quantity")]
        public IActionResult GetQuantity()
        {
            try
            {
                return Ok(_productService.getQuantity());
            }
            catch (Exception ex)
            {
                return BadRequest("Taking quantity error.");
            }
        }
    }
}