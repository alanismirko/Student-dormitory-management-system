using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using WebApplication.Models;

namespace WebApplication.Controllers
{
    public class RoomsController : Controller
    {
        string Baseurl = "https://localhost:44382/api/";



        // GET: Rooms with a search filter attribute
        public async Task<ActionResult> Rooms(string searchString, string searchDescription)
        {
            List<RoomModel> rooms = new List<RoomModel>();
            using (var client = new HttpClient())
            {
                //Passing service base url
                client.BaseAddress = new Uri(Baseurl);

                client.DefaultRequestHeaders.Clear();

                //Define request data format  
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage Res = await client.GetAsync("Rooms");

                //Checking the response is successful or not which is sent using HttpClient  
                if (Res.IsSuccessStatusCode)
                {
                    //Storing the response details recieved from web api   
                    var roomsResponse = Res.Content.ReadAsStringAsync().Result;

                    //Deserializing the response recieved from web api and storing into the Room list  
                    rooms = JsonConvert.DeserializeObject<List<RoomModel>>(roomsResponse);

                    // The rooms array is filtered with the search string if the string is not null, empty or whitespace
                    if (!string.IsNullOrEmpty(searchString) && !string.IsNullOrWhiteSpace(searchString))
                        rooms = rooms.FindAll(room => room.Price.ToString().Equals(searchString));
                    if (!string.IsNullOrEmpty(searchDescription) && !string.IsNullOrWhiteSpace(searchDescription))
                        rooms = rooms.FindAll(room => room.Description.Contains(searchDescription.ToLower()));
                    


                }
            }
            //returning the rooms list to view 
            return View(rooms);
        }

        
        public async Task<ActionResult> Room(int id)
        {
            RoomModel room = new RoomModel();
            using (var client = new HttpClient())
            {
                //Passing service base url
                client.BaseAddress = new Uri(Baseurl);

                client.DefaultRequestHeaders.Clear();

                //Define request data format  
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage Res = await client.GetAsync("Rooms/" + id);

                //Checking the response is successful or not which is sent using HttpClient  
                if (Res.IsSuccessStatusCode)
                {
                    //Storing the response details recieved from web api   
                    var roomResponse = Res.Content.ReadAsStringAsync().Result;

                    //Deserializing the response recieved from web api and storing into the Employee list  
                    room = JsonConvert.DeserializeObject<RoomModel>(roomResponse);
                }
            }
            //returning the rooms list to view 
            return View(room);
        }


       
    }

}    


    
