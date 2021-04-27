using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using WebApplication.Models;
using Newtonsoft;
using Newtonsoft.Json;
using System.Text;
using System.Net;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace WebApplication.Controllers
{
    public class HomeController : Controller
    {
        private readonly string _baseAddress = "https://localhost:44382/api/";

        public ActionResult Index()
        {
            return View();
        }

        public async Task<ActionResult> UserAccount()
        {
            List<BookingModel> studentBookings = new List<BookingModel>();
            string userId = (Session["UserId"]).ToString();
            using (var client = new HttpClient())
            {
                //Passing service base url
                client.BaseAddress = new Uri(_baseAddress);

                client.DefaultRequestHeaders.Clear();

                //Define request data format  
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage Res = await client.GetAsync("Bookings/AllStudentBookings/"+userId);

                //Checking the response is successful or not which is sent using HttpClient  
                if (Res.IsSuccessStatusCode)
                {
                    //Storing the response details recieved from web api   
                    var bookingsResponse = Res.Content.ReadAsStringAsync().Result;

                    //Deserializing the response recieved from web api and storing into the Employee list  
                    studentBookings = JsonConvert.DeserializeObject<List<BookingModel>>(bookingsResponse);

                }
            }
            return View(studentBookings);

        }

        public ActionResult HelpPage()
        {
            return View();
        }

        public ActionResult Register()
        {
            ViewBag.Name = "Register";
            return View();

        }

        public ActionResult Login()
        {
            ViewBag.Name = "Login";
            return View();


        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(StudentRegisterModel model)
        {
            using (var client = new HttpClient())
            {

                var json = JsonConvert.SerializeObject(model);
                var data = new StringContent(json, Encoding.UTF8, "application/json");

                //HTTP POST
                var postTask = client.PostAsync("Student/Register", data);


                var result = postTask.Result;

                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Login");
                }
            }

            ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(StudentLoginModel model)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_baseAddress);

                client.DefaultRequestHeaders.Clear();

                var json = JsonConvert.SerializeObject(model);

                var data = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage Res = await client.PostAsync("Student/Authenticate", data);

                if (Res.IsSuccessStatusCode)
                {
                    var loginResponse = Res.Content.ReadAsStringAsync().Result;

                    StudentLoggedInModel student = JsonConvert.DeserializeObject<StudentLoggedInModel>(loginResponse);

                    //create user session
                    Session["UserID"] = student.Id.ToString();
                    Session["UserEmail"] = student.Email.ToString();
                    Session["UserFirstName"] = student.FirstName.ToString();
                    Session["UserLastName"] = student.LastName.ToString();
                    Session["UserPhoneNumber"] = student.PhoneNumber.ToString();
                    Session["UserDateOfBirth"] = student.DateOfBirth.ToString();
                    Session["UserEducationEndDate"] = student.EducationEndDate.ToString();
                    Session["UserNationality"] = student.Nationality.ToString();
                    Session["UserToken"] = student.Token.ToString();
                    return RedirectToAction("UserDashBoard");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Wrong credentials");
                }
            }
            return View(model);
        }

        public ActionResult UserDashBoard()
        {
            if (Session["UserID"] != null)
            {
                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("Login");
            }
        }
        public ActionResult Logout()
        {
            Session.Abandon();
            return RedirectToAction("Login");

        }

        [HttpPut]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> UpdateInfo()
        {
            string userId = (Session["UserId"]).ToString();
            using (var client = new HttpClient())
            {
                //Passing service base url
                client.BaseAddress = new Uri(_baseAddress);

                client.DefaultRequestHeaders.Clear();

                //Define request data format  
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage Res = await client.GetAsync("Student/UpdateInfo/" + userId);

                //Checking the response is successful or not which is sent using HttpClient  
                if (Res.IsSuccessStatusCode)
                {
                    //Storing the response details recieved from web api   
                
                    var updateResponse = Res.Content.ReadAsStringAsync().Result;

                    StudentLoggedInModel student = JsonConvert.DeserializeObject<StudentLoggedInModel>(updateResponse);

                    //create user session
                    Session["UserID"] = student.Id.ToString();
                    Session["UserEmail"] = student.Email.ToString();
                    Session["UserFirstName"] = student.FirstName.ToString();
                    Session["UserLastName"] = student.LastName.ToString();
                    Session["UserPhoneNumber"] = student.PhoneNumber.ToString();
                    Session["UserDateOfBirth"] = student.DateOfBirth.ToString();
                    Session["UserNationality"] = student.Nationality.ToString();
                    Session["UserToken"] = student.Token.ToString();

                }
            }
            return View("UserAccount");
        }
    }
}