using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using car_dealership.Models;
using car_dealership.Services;
using Microsoft.AspNetCore.Http;

namespace car_dealership.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        const string SOME_SESSION_DATA = "Session Information";
        const string MORE_SESSION_DATA = "Session Date";


        public HomeController (IHttpContextAccessor httpContextAccessor)
        {
            this._httpContextAccessor = httpContextAccessor;
        }

        public IActionResult ClearCookie(string key)
        {
            CookieHelper cookieHelper = new CookieHelper(_httpContextAccessor, Request,
                                                         Response);
            cookieHelper.Remove(key);
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public ActionResult Index(SiteUserVM siteUser)
        {
            CookieHelper cookieHelper = new CookieHelper(_httpContextAccessor, Request,
                                                         Response);
            cookieHelper.Set("firstName", siteUser.firstName, 1);
            // Redirect to GET method so cookie is read.
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult Index()
        {
            CookieHelper cookieHelper = new CookieHelper(_httpContextAccessor, Request,
                                                         Response);
            string firstName = cookieHelper.Get("firstName");
            if (firstName != null)
            {
                ViewBag.UserName = firstName;
            }

            if (HttpContext.Session.GetString(SOME_SESSION_DATA) == null)
            {
                // Store soome data using first key-value pair.
                string someData = "Green";
                ViewBag.Color = someData;
                HttpContext.Session.SetString(SOME_SESSION_DATA, someData);

                // Store more data using second key-value pair.
                DateTime now = DateTime.Now;
                ViewBag.SessionStart = now.ToString();
                HttpContext.Session.SetString(MORE_SESSION_DATA, now.ToString());
            }
            // Session data exists so show it.
            else
            {
                ViewBag.Color = HttpContext.Session.GetString(SOME_SESSION_DATA);
                ViewBag.SessionStart = HttpContext.Session.GetString(MORE_SESSION_DATA);
            }

            return View();
        }

        public IActionResult ClearSesh()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
