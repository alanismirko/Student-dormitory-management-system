using ModelLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;

namespace WebService.Models
{
    public class LoginResponse
    {
        public int Id { get; set; }
        public string Email { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string PhoneNumber { get; set; }

        public DateTime DateOfBirth { get; set; }

        public DateTime EducationEndDate { get; set; }

        public string Nationality { get; set; }
        public string Token { get; set; }
        //public HttpResponseMessage responseMsg { get; set; }

        public LoginResponse()
        {
            this.Token = "";
            //this.responseMsg = new HttpResponseMessage() { StatusCode = System.Net.HttpStatusCode.Unauthorized };
        }
    }
}