using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Ticketing.Web.Models;

namespace Ticketing.Web.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost("ticket")]
        public IActionResult Ticket()
        {
            SetCookie("ticketId", "test", 30);
            return RedirectToAction("Processing"); 
        }

        public IActionResult Applause()
        {
            return View();
        }
        
        public IActionResult Redirect()
        {
            return Redirect("https://youtu.be/kQQ9npyZNiw?t=572");
        }
        public IActionResult Processing()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public void SetCookie(string key, string value, int? expireTime)  
        {  
            CookieOptions option = new CookieOptions();  

            if (expireTime.HasValue)  
                    option.Expires = DateTime.Now.AddMinutes(expireTime.Value);  
            else  
                    option.Expires = DateTime.Now.AddMilliseconds(10);  
            
            Response.Cookies.Append(key, value, option);  
            }  
    }
}
