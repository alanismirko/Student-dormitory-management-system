using BusinessLayer;
using ModelLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebService.Controllers
{
    public class RoomsController : ApiController
    {
        RoomHandler roomHandler = new RoomHandler();

        // GET: api/Rooms
        public IEnumerable<RoomModel> Get()
        {
            return roomHandler.GetAllAvailable();
        }

        // GET: api/Rooms/5
        public RoomModel Get(int id)
        {
            return roomHandler.Get(id);
        }

        //GET: api/Rooms/all
        [Route("api/Rooms/all")]
        public IEnumerable<RoomModel> GetAll()
        {
            return roomHandler.GetAll();
        }

        //WPF admin endpoint
        // POST: api/Rooms
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IHttpActionResult Post([FromBody]RoomModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var response = roomHandler.Add(model);
                if (response.Id == 0)
                    return Ok("Something went wrong");
                else return Ok("Room created");
            }
            catch (Exception)
            {
                return Ok("Something went wrong");
            }
        }

        // PUT: api/Rooms/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Rooms/5
        [Authorize(Roles = "Admin")]
        public void Delete(int id)
        {
            roomHandler.Delete(id);
        }

        [Authorize(Roles = "Admin")]
        [Route("api/Rooms/Update")]
        public IHttpActionResult Update([FromBody] RoomModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                if (roomHandler.CheckDateOfModification(model.modificationDate, model.Id))
                {
                    int response = roomHandler.Update(model);
                    if (response == 2)
                        return Ok("ok");
                }
                return Ok("Updates were already made, close window to see them");
            }
            catch (Exception)
            {
                return Ok("Something went wrong");
            }
        }


    }
}
