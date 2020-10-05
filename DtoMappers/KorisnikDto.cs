using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using biljar.DbModels;
using biljar.Models;
using Newtonsoft.Json.Linq;

namespace biljar.DtoMappers
{
    public static class KorisnikDto
    {


        public static Korisnik FromJson(JObject json)
        {
            var id = json["IdKorisnici"].ToObject<int?>();
            var email = json["Email"].ToObject<string>();
            var username = json["Username"].ToObject<string>();
            var password = json["Password"].ToObject<string>();
            var liga = json["Liga"].ToObject<string>();





            return new Korisnik(id, email, username, password, liga,0);
        }


    }
}
