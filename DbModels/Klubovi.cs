using System;
using System.Collections.Generic;

namespace biljar.DbModels
{
    public partial class Klubovi
    {
        public Klubovi()
        {
            Korisnici = new HashSet<Korisnici>();
        }

        public int IdKlubovi { get; set; }
        public string Ime { get; set; }
        public string Lokacija { get; set; }
        public string Kontakt { get; set; }

        public virtual ICollection<Korisnici> Korisnici { get; set; }
    }
}
