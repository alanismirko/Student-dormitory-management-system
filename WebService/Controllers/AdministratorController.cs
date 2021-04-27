using RESTWebAPI.Models;
using BusinessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ModelLayer;
using DevOne.Security.Cryptography.BCrypt;
using WebService.Models;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;

namespace WebService.Controllers
{
    [Authorize]
    public class AdministratorController : ApiController
    {
        AdministratorHandler adminHandler = new AdministratorHandler();
        List<AdministratorModel> Administrators = new List<AdministratorModel>();

        // GET: api/Administrators
        public List<AdministratorModel> Get()
        {
            return Administrators;
        }



        // GET: api/Administrator/5

        public AdministratorModel Get(int id)
        {
            return Administrators.Where(x => x.Id == id).FirstOrDefault();
        }

        // POST: api/Administrator
        [HttpPost]
        [Route("api/Administrator/Register")]
        public IHttpActionResult Register(AdministratorUser model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                int id = adminHandler.CheckEmailAvailability(model.Email);
                if (id == 0)
                {
                    //administrator info
                    var administrator = new AdministratorModel()
                    {
                        Email = model.Email,
                        FirstName = model.FirstName,
                        LastName = model.LastName,
                        PhoneNumber = model.PhoneNumber,
                        EmployeeNumber = model.employeeNumber,
                    };
                    //password hash
                    string mySalt = BCryptHelper.GenerateSalt();
                    string hashedPassword = BCryptHelper.HashPassword(model.Password, mySalt);
                    adminHandler.Create(administrator, hashedPassword);
                    return Ok("Success");
                }
                else return Ok("Email taken");
            }
            catch (Exception)
            {
                return Ok("Something went wrong");
            }
        }

        // DELETE: api/Administrator/5
        public void Delete(int id)
        {
            Administrators.RemoveAll(x => x.Id == id);
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("api/Administrator/Info")]
        public IHttpActionResult GetAdministratorInfo([FromBody] LoginRequest login)
        {
            var loginResponse = new LoginResponseAdmin { };
            //create response object with the user information and the token
            //token
            loginResponse.Token = createToken(login.Email);
            //user info
            AdministratorModel administrator = adminHandler.GetAdministratorInfo(login.Email);
            loginResponse.Id = administrator.Id;
            loginResponse.Email = administrator.Email;
            loginResponse.FirstName = administrator.FirstName;
            loginResponse.LastName = administrator.LastName;
            loginResponse.PhoneNumber = administrator.PhoneNumber;
            loginResponse.EmployeeNumber = administrator.EmployeeNumber;
            loginResponse.ModificationDate = administrator.modificationDate;

            //return the token
            return Ok(loginResponse);
        }

        [HttpPost]
        [Route("api/Administrator/LogIn")]
        [AllowAnonymous]
        public IHttpActionResult CheckAdminLogIn([FromBody] LoginRequest login)
        {
            string realPassword = adminHandler.GetAdministratorPassword(login.Email);
            if (realPassword == null)
            {
                return Ok("Incorrect credentials");
            }
            bool doesPasswordsMatch = BCryptHelper.CheckPassword(login.Password, realPassword);
            if (doesPasswordsMatch)
            {
                return Ok("ok");
            }
            else return Ok("Incorrect credentials");
        }

        [Authorize(Roles ="Admin")]
        public IHttpActionResult updateInfo([FromBody] AdministratorModel admin)
        {
            bool checkForUpdates = adminHandler.CheckModificationDate(admin.modificationDate, admin.Id);
            if (checkForUpdates == false)
                return Ok("Updates Happend, close this window to see changes");
            bool response = adminHandler.Update(admin);
            if (response)
                return Ok("ok");
            return Ok("Something went wrong");
        }

        private string createToken(string email)
        {
            //Set issued at date
            DateTime issuedAt = DateTime.UtcNow;
            //set the time when it expires
            DateTime expires = DateTime.UtcNow.AddMinutes(10);

            //http://stackoverflow.com/questions/18223868/how-to-encrypt-jwt-security-token
            var tokenHandler = new JwtSecurityTokenHandler();

            //create a identity and add claims to the user which we want to log in
            ClaimsIdentity claimsIdentity = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Role, "Admin")
        });

            const string sec = "401b09eab3c013d4ca54922bb802bec8fd5318192b0a75f201d8b3727429090fb337591abd3e44453b954555b7a0812e1081c39b740293f765eae731f5a65ed1";
            var now = DateTime.UtcNow;
            var securityKey = new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey(System.Text.Encoding.Default.GetBytes(sec));
            var signingCredentials = new Microsoft.IdentityModel.Tokens.SigningCredentials(securityKey, Microsoft.IdentityModel.Tokens.SecurityAlgorithms.HmacSha256Signature);


            //create the jwt
            var token =
                (JwtSecurityToken)
                    tokenHandler.CreateJwtSecurityToken(issuer: "https://localhost:44382/", audience: "https://localhost:44382/",
                        subject: claimsIdentity, notBefore: issuedAt, expires: expires, signingCredentials: signingCredentials);
            var tokenString = tokenHandler.WriteToken(token);

            return tokenString;
        }

    }
}


