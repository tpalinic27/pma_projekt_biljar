using System;
using System.Collections.Generic;

namespace biljar.DbModels
{
    public partial class Admin
    {
        public int IdAdmin { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
