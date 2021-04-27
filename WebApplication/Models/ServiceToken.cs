using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication.Models
{
    public class ServiceToken
    {
        public string Access_token { get; set; }
        public string Token_type { get; set; }
        public string Email { get; set; }

        public ServiceToken()
        {
        }

        public ServiceToken(string access_token, string token_type, string email)
        {
            Access_token = access_token;
            Token_type = token_type;
            Email = email;
        }
    }
}