using ModelLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;

namespace WebService.Models
{
    public class LoginResponseAdmin
    {
        public int Id { get; set; }
        public string Email { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string PhoneNumber { get; set; }

        public int EmployeeNumber { get; set; }

        public DateTime ModificationDate { get; set; }

        public string Token { get; set; }
    }
}