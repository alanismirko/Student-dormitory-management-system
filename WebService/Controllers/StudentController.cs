using System;
using System.Web.Http;
using WebService.Models;
using BusinessLayer;
using ModelLayer;
using DevOne.Security.Cryptography.BCrypt;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;

namespace WebService.Controllers
{
    public class StudentController : ApiController
    {
        StudentHandler studentHandler = new StudentHandler();

        [HttpPost]
        [Route("api/Student/Register")]
        public IHttpActionResult Register(RegisterRequest register)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                //student info
                var student = new StudentModel()
                {
                    Email = register.Email,
                    FirstName = register.FirstName,
                    LastName = register.LastName,
                    PhoneNumber = register.PhoneNumber,
                    DateOfBirth = register.DateOfBirth,
                    EducationEndDate = register.EducationEndDate,
                    Nationality = register.Nationality

                };
                //password hash
                string mySalt = BCryptHelper.GenerateSalt();
                var hashedPassword = BCryptHelper.HashPassword(register.Password, mySalt);
                var result = studentHandler.Create(student, hashedPassword);
                return Ok("Success");
            }
            catch (Exception)
            {
                return Ok("Something went wrong");
            }
        }


        [HttpPost]
        public IHttpActionResult Authenticate([FromBody] LoginRequest login)
        {
            var loginResponse = new LoginResponse { };

            bool isEmailPasswordValid = false;

            if (login != null)
                isEmailPasswordValid = validateEmailPassword(login.Email, login.Password);

            //if credentials are valid
            if (isEmailPasswordValid)
            {
                //create response object with the user information and the token
                //token
                loginResponse.Token = createToken(login.Email);
                //user info
                StudentModel student = studentHandler.GetByEmail(login.Email);
                loginResponse.Id = student.Id;
                loginResponse.Email = student.Email;
                loginResponse.FirstName = student.FirstName;
                loginResponse.LastName = student.LastName;
                loginResponse.PhoneNumber = student.PhoneNumber;
                loginResponse.DateOfBirth = student.DateOfBirth;
                loginResponse.EducationEndDate = student.EducationEndDate;
                loginResponse.Nationality = student.Nationality;

                //return the token
                return Ok(loginResponse);
            }
            else
            {
                // if credentials are not valid send unauthorized status code in response
                //loginResponse.responseMsg.StatusCode = HttpStatusCode.Unauthorized;
                //response = ResponseMessage(loginResponse.responseMsg);
                //return response;
                return Unauthorized();
            }
        }

        private bool validateEmailPassword(string email, string password)
        {
            //get the hashed password from the database
            string realPassword = studentHandler.GetStudentPassword(email);
            if (realPassword == null)
            {
                return false;
            }
            //check if the hashed password in the database matches with the input password
            bool doesPasswordsMatch = BCryptHelper.CheckPassword(password, realPassword);
            if (doesPasswordsMatch)
            {
                return true;
            }
            else return false;
        }

        [HttpGet]
        [Route("api/Student/FindByBooking")]
        [Authorize(Roles = "Admin")]
        public StudentModel FindByBookingId(int bookingId)
        {
            return studentHandler.FindByBookingId(bookingId);
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
                new Claim(ClaimTypes.Role, "Asshole")
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

        [HttpPut]
        [Route("api/Student/UpdateInfo")]
        public IHttpActionResult UpdateInfo([FromBody] StudentModel student)
        {
            try
            {
                bool operation = studentHandler.Update(student);
                if (operation)
                {
                    return Ok("Update was successful");
                }
                return NotFound();

            }
            catch (Exception)
            {
                return Ok("Something went wrong");
            }
        }


    }
}

