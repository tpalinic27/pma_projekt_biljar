using biljar.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace biljar.Mapper
{
    public class KluboviMapper
    {

        public static Klubovi FromDatabase(DbModels.Klubovi k)
        {
            return new Klubovi(k.IdKlubovi, k.Ime, k.Lokacija, k.Kontakt);

        }
        public static DbModels.Klubovi ToDatabase(Klubovi k)
        {
            return new DbModels.Klubovi
            {
                IdKlubovi = k.Id_klubovi.HasValue ? k.Id_klubovi.Value : 0,
                Ime = k.Ime,
                Lokacija = k.Lokacija,
                Kontakt = k.Kontakt,

            };
        }
    }
}
