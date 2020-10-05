using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using biljar.Models;
using biljar.Services;
using Newtonsoft.Json;
using biljar.DtoMappers;

namespace biljar.Controllers
{
    [Route("api/korisnici")]
    [ApiController]
    public class KorisniciApiController : ControllerBase
    {
        private List<Korisnik> _korisnik;
        private readonly KorisnikService _korisnikService;
        public KorisniciApiController(KorisnikService korisnikService)
        {
            _korisnikService = korisnikService;
            _korisnik = new List<Korisnik>();
        }

        [HttpGet]
        public ActionResult<List<Korisnik>> Get()
        {
            var userInfo = JsonConvert.DeserializeObject<UserInfo>(HttpContext.Session.GetString("SessionUser"));
            var userId = userInfo.UserId;
            return _korisnikService.GetAll(userId).ToList();
            
        }

        [HttpGet("klubovi")]
        public ActionResult<List<Klubovi>> GetKlubovi() 
        {
            if (HttpContext.Session.GetString("Role") != "korisnik") return RedirectToRoute(new { controller = "Authentication", action = "AdminSignin" });
            var k= _korisnikService.GetKlubovi().ToList();
            return k;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Korisnik>> GetOne(int id)
        {
            return await _korisnikService.GetAsync(id);
        }

        [HttpPost("save")] //POST api/courses/save
        public ActionResult<Korisnik> Save([FromBody] JObject json)
        {
            var proizvodi = KorisnikDto.FromJson(json);

            _korisnikService.Save(proizvodi);
            return RedirectToRoute(new
            {
                controller = "Home",
                action = "Index"
            });
        }


        [HttpPut("unosklub/{id}")]
        public IActionResult UnosiKlub (int id) 
        {
            if (HttpContext.Session.GetString("Role") != "korisnik") return RedirectToRoute(new { controller = "Authentication", action = "AdminSignin" });

            string a = HttpContext.Session.GetString("Id");
            //var id = json["id"].ToObject<int>();

            var idk = int.Parse(HttpContext.Session.GetString("Id"));

            JObject kj = JObject.Parse(HttpContext.Session.GetString("SessionUser"));

            Korisnik korisnik = KorisnikDto.FromJson(kj);




            
            
             korisnik.KlubId = id;
             _korisnikService.Edit(korisnik);
            return Ok();
            
   

          

   
        }




    }
}
