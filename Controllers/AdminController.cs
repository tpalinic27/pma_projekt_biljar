using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using biljar.DbModels;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using biljar.Models;
using biljar.Services;

namespace biljar.Controllers
{
    public class AdminController : Controller
    {

        private AdminService _adminService;

        public AdminController(AdminService adminService)
        {
            _adminService = adminService;
        }


        public IActionResult AdminHome()
        {
            if (HttpContext.Session.GetString("SessionAdmin") == null)
            {
                return RedirectToRoute(new
                {
                    controller = "Authentication",
                    action = "AdminSignin"
                });
            }
            else
            {
                var userInfo = JsonConvert.DeserializeObject<UserInfo>(HttpContext.Session.GetString("SessionAdmin"));
                ViewBag.user = userInfo.UserId;
                return View();
            }
        }


        public ActionResult<List<Korisnik>> SviKorisnici() 
        {
            if (HttpContext.Session.GetString("Role") != "admin") return RedirectToRoute(new { controller = "Authentication", action = "AdminSignin" });
            return View(_adminService.GetAll().ToList());
            
        }

        public ActionResult<List<Models.Klubovi>> Klubovi()
        {
            if (HttpContext.Session.GetString("Role") != "admin") return RedirectToRoute(new { controller = "Authentication", action = "AdminSignin" });
            return View(_adminService.GetAllKlubovi().ToList());

        }

    }
}
