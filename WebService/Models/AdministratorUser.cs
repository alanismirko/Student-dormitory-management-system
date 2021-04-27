using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RESTWebAPI.Models
{
    public class AdministratorUser
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public int employeeNumber { get; set; }
    }
}