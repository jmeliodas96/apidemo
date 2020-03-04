using System;
using System.Net;
using System.Net.Mail;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Logging;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using apidemo.Controllers;
using apidemo.Models;
using Microsoft.AspNetCore.Mvc;

namespace apidemo.Controllers
{
    [ApiController]
    [Route("api/[products]")]
    public class ProductController : ControllerBase
    {
        private readonly BakeryContext db;  
        public ProductController(BakeryContext db) => this.db = db;
        public List<Product> Products { get; set; } = new List<Product>();  
        
        [HttpGet]
        public async Task OnGetAsync()
        {
            Products = await db.Products.ToListAsync();
            if (Products == null)
            {
                return NotFound();
            }
            return Products;
        }
    }    
}