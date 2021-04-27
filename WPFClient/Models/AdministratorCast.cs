using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace WPFClient.Models
{
    public class AdministratorCast
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public int EmployeeNumber { get; set; }
        public DateTime modificationDate { get; set; }
        public string Token { get; set; }


        public AdministratorCast()
        {

        }


    }
}
