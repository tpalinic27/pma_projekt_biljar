using System;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using biljar.Controllers;
using System.Threading.Tasks;
using biljar.Models;


namespace biljar.Mapper
{
    public class KorisniciMapper
    {
        public static Korisnik FromDatabase(DbModels.Korisnici k)
        {

            return new Korisnik(
                k.IdKorisnici,
                k.Email, 
                k.Username,
                k.Password,
                k.Liga,
                k.IdKlubovi
                ); 

        }
        public static DbModels.Korisnici ToDatabase(Korisnik k)
        {
            return new DbModels.Korisnici
            {
                IdKorisnici = k.Id.HasValue ? k.Id.Value : 0,
                Email = k.Email,
                Username = k.Username,
                Password = k.Password,
                Liga = k.Liga,
                IdKlubovi = k.KlubId

            };
        }
    }
}
