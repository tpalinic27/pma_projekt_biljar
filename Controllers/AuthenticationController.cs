using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using biljar.DbModels;
using biljar.Mapper;
using biljar.Models;
using biljar.Models.AuthenticationModel;
using biljar.Repositories;
using biljar.Services;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Web.Helpers;
using Newtonsoft.Json.Linq;

namespace biljar.Controllers
{
    public class AuthenticationController : Controller
    {
        private readonly pma_biljarContext _dbContext;

        public AuthenticationController(pma_biljarContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IActionResult KlubOdaberi(int id)
        {
            
            return View(_dbContext.Klubovi.Select(x => KluboviMapper.FromDatabase(x)).ToList());
        }

        [HttpPost]
        public IActionResult SignIn([FromForm] RegisterData register)
        {
            
            var valid = Verify(register.Email, register.Password);
            if (valid != null)
            {
                HttpContext.Session.SetString("Role", "korisnik");
                HttpContext.Session.SetString("Id", valid.IdKorisnici.ToString());
                HttpContext.Session.SetString("SessionUser", JsonConvert.SerializeObject(valid));


                return RedirectToRoute(new
                {
                    controller = "Korisnici",
                    action = "PrikazKorisnika"
                });
            }
            else
            {
                return View();
            }
        }
        public IActionResult SignIn()
        {

            return View();
        }


        [HttpPost]
        public IActionResult SignUp(string email, string username, string password, string potvrdite, string liga)
        {

            var valid = VerifySignUp(email, username, password, potvrdite, liga);
            if (valid != null)
            {
                HttpContext.Session.SetString("Role", "korisnik");
                HttpContext.Session.SetString("Id", valid.IdKorisnici.ToString());
                HttpContext.Session.SetString("SessionUser", JsonConvert.SerializeObject(valid));

                return Redirect("~/register/" + valid.IdKorisnici);
            }
            else
            {
                return View();
            }

        }
        public IActionResult SignUp()
        {

            return View();
        }

        [HttpPost]
        public IActionResult AdminSignin([FromForm] RegisterData register)
        {

            var valid = VerifyAdmin(register.Email, register.Password);
            if (valid != null)
            {

                return RedirectToRoute(new
                {
                    controller = "Home",
                    action = "Index"
                });
            }
            else
            {
                return View();
            }

        }
        public IActionResult AdminSignin()
        {

            return View();
        }

        [HttpPost]
        public IActionResult Register(RegisterData registerData)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // redirektat cemo se kod kuce da refreshamo User.Identity
            return RedirectToRoute("/");

        }

        //Verification methods:
        public DbModels.Korisnici Verify(string email, string password)
        {
            var result = _dbContext.Korisnici.Where(x => (x.Email.Equals(email) && x.Password.Equals(password))).FirstOrDefault();
            if (result == null)
            {
                return null;
            }
            else
            {

                var userInfo = new UserInfo() { Email = result.Email, UserId = result.IdKorisnici };
                HttpContext.Session.SetString("SessionUser", JsonConvert.SerializeObject(userInfo));
                return result;
            }

        }
        public DbModels.Korisnici VerifySignUp(string email, string username, string password, string passwordConfirm, string liga)
        {

            if (password == null || username == null || passwordConfirm == null || email == null || password != passwordConfirm || liga == null)
            {

                return null;
            }
            else
            {
                var result = _dbContext.Korisnici.Where(x => x.Username.Equals(username)).FirstOrDefault();


                if (result == null  )
                {

                    Models.Korisnik k = new Models.Korisnik(null, email, username, password, liga, null);
                    var dbKorisnik = KorisniciMapper.ToDatabase(k);
                    _dbContext.Korisnici.Add(dbKorisnik);
                    _dbContext.SaveChanges();
                    var result2 = _dbContext.Korisnici.Where(x => x.Email.Equals(email)).FirstOrDefault();
                    Console.WriteLine("Result user: " + result2);
                    var userInfo = new UserInfo() { Email = result2.Email, UserId = result2.IdKorisnici };
                    HttpContext.Session.SetString("SessionUser", JsonConvert.SerializeObject(userInfo));
                    return result2;
                }
                else
                {
                    ViewBag.Message = "Korisnik već postoji";
                    return null;
                }
            }

        }
        public DbModels.Admin VerifyAdmin(string email, string password)
        {
            var result = _dbContext.Admin.Where(x => (x.Email.Equals(email) && x.Password.Equals(password))).FirstOrDefault();
            if (result == null)
            {
                return null;
            }
            else
            {
                var userInfo = new UserInfo() { Email = result.Email, UserId = result.IdAdmin };
                HttpContext.Session.SetString("SessionAdmin", JsonConvert.SerializeObject(userInfo));
                HttpContext.Session.SetString("Role", "admin");
                return result;
            }

        }
    }
}
