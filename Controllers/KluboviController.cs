using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using biljar.Models;
using Microsoft.EntityFrameworkCore;
using biljar.DbModels;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http;

namespace biljar.Controllers
{
    public class KluboviController : Controller
    {
        private readonly pma_biljarContext _kluboviContext;

        public KluboviController(pma_biljarContext kluboviContext)
        {
            _kluboviContext = kluboviContext;
        }
        public IActionResult Klubovi()
        {
            var rezultati = _kluboviContext.Klubovi.ToList();
            return View(rezultati);
        }

        public IActionResult AdminKlub()
        {
            if (HttpContext.Session.GetString("Role") != "Admin")
            {
                return RedirectToRoute(new
                {
                    controller = "Authentication",
                    action = "AdminSignin"
                });
            }
            //var userInfo = JsonConvert.DeserializeObject<UserInfo>(HttpContext.Session.GetString("SessionAdmin"));
            //ViewBag.user = userInfo.UserId;
            var rezultati = _kluboviContext.Klubovi.ToList();
            return View(rezultati);
        }

    }
}
