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
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Cors;

namespace apidemo.Controllers
{
    [ApiController]
    [EnableCors("AllowOrigin")]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly BakeryContext db;  
        public ProductController(BakeryContext db) => this.db = db;        

        [HttpGet]
        public async Task<List<Product>> OnGetAsync()
        {
            var Products = await db.Products.ToListAsync(); 
            return Products;
        }
    }

    


}