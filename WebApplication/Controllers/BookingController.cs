using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using WebApplication.Models;

namespace WebApplication.Controllers
{
    [Route("[controller]")]
    public class BookingController : Controller
    {
        //web service
        string Baseurl = "https://localhost:44382/api/";


        static List<BookingModel> bookings = new List<BookingModel>();

        // GET: Booking
        public ActionResult Index()
        {
            return View();
        }

        // GET: Booking/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Booking/Create
        [HttpGet]
        public ActionResult Create()
        {
            if(Session["UserID"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login", "Home");
            }
            
        }

        public ActionResult Message()
        {

            return View();
        }

        [Route("{id}")]
        public async Task<ActionResult> Cancel(int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);


                HttpResponseMessage Res = await client.GetAsync("Bookings/Cancel/"+id);

                if (Res.IsSuccessStatusCode)
                {
                    return RedirectToAction("UserAccount", "Home");
                }
                return RedirectToAction("UserAccount", "Home");
            }
        }

        [Route("{id}")]
        public async Task<ActionResult> Finalize(int id)
        {
            using(var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);


                HttpResponseMessage Res = await client.GetAsync("Bookings/Finalize/" + id);

                if (Res.IsSuccessStatusCode)
                {
                    return RedirectToAction("UserAccount", "Home");
                }
                return RedirectToAction("UserAccount", "Home");
            }
        }

        // POST: Booking/Create
        [Route("{id}")]
        [HttpPost]
        public async Task<ActionResult> Create(BookingModel booking, int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);

                booking.RoomId = id;
                booking.UserId = Convert.ToInt32(Session["UserId"]);

                var json = JsonConvert.SerializeObject(booking);
                var data = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage Res = await client.PostAsync("Booking", data);

                if(Res.IsSuccessStatusCode)
                {
                    return RedirectToAction("Message");
                }
                return RedirectToAction("Rooms");
            }
        }
        // GET: Booking/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Booking/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Booking/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Booking/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
