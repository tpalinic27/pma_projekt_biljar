using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace biljar.Models
{
    public class Korisnik
    {
        public int? Id { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Liga { get; set; }
        public int? KlubId { get; set; }

        public Korisnik(int? id, string email,string username, string password, string liga, int? klub)
        {
            Id = id.HasValue ? id : 0;
            Email = email;
            Username = username;
            Password = password;
            Liga = liga;
            KlubId = klub;
        }
    }
}
