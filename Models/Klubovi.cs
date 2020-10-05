using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace biljar.Models
{
    public class Klubovi
    {
        [Key]
        public int? Id_klubovi { get; set; }
        public string Ime { get; set; }
        public string Lokacija { get; set; }
        public string Kontakt { get; set; }

        public Klubovi(int? id, string ime, string lokacija, string kontakt)
        {
            Id_klubovi = id.HasValue ? id : 0;
            Ime = ime;
            Lokacija = lokacija;
            Kontakt = kontakt;
        }

    }
}
