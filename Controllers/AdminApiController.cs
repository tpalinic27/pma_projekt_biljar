using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using biljar.DtoMappers;
using biljar.Models;
using biljar.Services;

namespace biljar.Controllers
{
    [Route("api/admin")]
    [ApiController]
    public class AdminApiController : ControllerBase
    {
        private AdminService _adminService;
        public AdminApiController(AdminService adminService)
        {
            _adminService = adminService;
        }

        [HttpGet("allusers")]
        public ActionResult<List<Korisnik>> Get()
        {
            return _adminService.GetAll().ToList();
        }





        [HttpDelete("deletekorisnik/{id}")]
        public void DeleteKorisnik(int id)
        {
            
            _adminService.DeleteKorisnik(id);
        }

        [HttpDelete("deleteklub/{id}")]
        public void DeleteKlub(int id)
        {

            _adminService.DeleteKlub(id);
        }

        [HttpPost("addklub")]
        public IActionResult AddKlub(JObject json)
        {

            string ime =json["i"].ToObject<string>();
            string lokacija = json["l"].ToObject<string>();
            string kontakt = json["k"].ToObject<string>();

            Klubovi novi = new Klubovi(null, ime, lokacija, kontakt);
            _adminService.AddKlub(novi);


            return Ok();
        }

        [HttpPut("editklub")]
        public IActionResult EditKlub(JObject json) 
        {
            string ime = json["ime"].ToObject<string>();
            string lokacija = json["lokacija"].ToObject<string>();
            string kontakt = json["kontakt"].ToObject<string>();
            int id = json["id"].ToObject<int>();
            Klubovi promjena = new Klubovi(id, ime, lokacija, kontakt);
            _adminService.EditKlub(promjena);
            return Ok();
        }
    }
}
