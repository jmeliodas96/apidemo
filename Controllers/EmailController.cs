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
    [Route("api/[controller]/[action]")]
    public class EmailController : ControllerBase
    {
        private readonly BakeryContext db;  
        public EmailController(BakeryContext db) => this.db = db; 
        
        public Product Product { get; set; }

        //api/email/send
        [HttpPost]
        [EnableCors("AnotherPolicy")]
        [ActionName("send")]
        [Consumes("application/json")]
        [Produces("application/json")]
        public async Task SendEmail([FromBody]Email Email)
        {  

            var Product = await db.Products.FindAsync(Email.id);

            var body = $@"<p>Thank you, we have received your order for {Email.OrderQuantity} unit(s) of {Product.Name}!</p>
            <p>Your address is: <br/>{Email.OrderShipping.Replace("\n", "<br/>")}</p>
            Your total is ${Product.Price * Email.OrderQuantity}.<br/>
            We will contact you if we have questions about your order.  Thanks!<br/>";


            var smtp = new SmtpClient()
            {
                DeliveryMethod = SmtpDeliveryMethod.Network,

                UseDefaultCredentials = false,
                Credentials = new NetworkCredential("billypalacio25@gmail.com", "masquinita#174"),
                Port = 587,
                Host = "smtp.gmail.com",
                EnableSsl = true,
            };

            var message = new MailMessage();

            message.To.Add(Email.OrderEmail);
            message.Subject = "Fourth Coffee - New Order";
            message.Body = body;
            message.IsBodyHtml = true;
            message.From = new MailAddress("sales@fourthcoffee.com");
            await smtp.SendMailAsync(message);
    
        }


    }

    


}