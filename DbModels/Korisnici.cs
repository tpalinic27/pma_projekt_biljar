using System;
using System.Collections.Generic;

namespace biljar.DbModels
{
    public partial class Korisnici
    {
        public int IdKorisnici { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Liga { get; set; }
        public int? IdKlubovi { get; set; }
        public string Username { get; set; }

        public virtual Klubovi IdKluboviNavigation { get; set; }
    }
}
