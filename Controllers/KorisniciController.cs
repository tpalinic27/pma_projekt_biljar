using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using biljar.DbModels;
using Microsoft.AspNetCore.Http;
using biljar.Services;
using System.Data.Entity;

namespace biljar.Controllers
{
    public class KorisniciController : Controller
    {
        private readonly pma_biljarContext _korisniciContext;
        private readonly KorisnikService _ks;

        public KorisniciController(pma_biljarContext korisniciContext,KorisnikService ks)
        {
            _ks = ks;
            _korisniciContext = korisniciContext;
        }
        public IActionResult Korisnici()
        {
            if (HttpContext.Session.GetString("Role") != "korisnik") return RedirectToRoute(new { controller = "Authentication", action = "AdminSignin" });
            var mojrez = _ks.GetAll2();
            return View(mojrez);
        }

        public IActionResult PrikazKorisnika() 
        {
            if (HttpContext.Session.GetString("Role") != "korisnik")
            {
                return RedirectToRoute(new
                {
                    controller = "Authentication",
                    action = "SignIn"
                });
            }
            var res= _ks.GetKorisnikDB(int.Parse(HttpContext.Session.GetString("Id")));
            

            return View(res);
        }
        public IActionResult AdminKorisnici()
        {
            if (HttpContext.Session.GetString("Role") != "Admin") return RedirectToRoute(new { controller = "Authentication", action = "AdminSignin" });
            



            //var userInfo = JsonConvert.DeserializeObject<UserInfo>(HttpContext.Session.GetString("SessionAdmin"));
            //ViewBag.user = userInfo.UserId;
            var rezultati = _korisniciContext.Korisnici.ToList();
            return View(rezultati);
        }
    }
}
