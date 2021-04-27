using BusinessLayer;
using ModelLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebService.Models;

namespace WebService.Controllers
{
    public class BookingController : ApiController
    {
        RoomHandler roomHandler = new RoomHandler();
        BookingHandler bookingHandler = new BookingHandler();

        // GET: api/Booking
        public IEnumerable<BookingModel> Get()
        {
            return bookingHandler.GetAll();
        }

        // GET: api/Booking/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Booking
        public IHttpActionResult Post([FromBody] BookingModel booking)
        {
            if (roomHandler.isAvailable(booking.RoomId)){
                bookingHandler.Create(booking, booking.UserId);
                return Ok();
            }
            else
            {
                return Ok("Couldn't be booked");
            }
            
        }

        // PUT: api/Booking/5
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/Booking/5
        public void Delete(int id)
        {
        }

        [HttpGet]
        [Route("api/Bookings/Cancel/{bookingId}")]
        public IHttpActionResult Cancel(int bookingId)
        {
            int rowsAffected = bookingHandler.ChangeBookingStatus(BookingStatus.Cancelled, bookingId);
            if (rowsAffected == 1)
                return Ok("Booking successfully cancelled");
            else
                return BadRequest();
        }

        [HttpGet]
        [Route("api/Bookings/Finalize/{bookingId}")]
        public IHttpActionResult Finalize(int bookingId)
        {
            BookingModel booking = bookingHandler.Get(bookingId);
            int roomId = booking.RoomId;
            bool isSuccessful = bookingHandler.Finalize(bookingId, roomId);
            if (isSuccessful)
                return Ok("Booking successfully finalized");
            else
                return BadRequest();
        }



        [Route("api/Bookings/AllStudentBookings/{studentId}")]
        public IEnumerable<BookingModel> GetAllStudentBookings(int studentId)
        {
            return bookingHandler.GetAllStudentBookings(studentId);
        }

        [Authorize(Roles = "Admin")]
        [Route("api/Bookings/AllOfStatus")]
        public IEnumerable<BookingModel> GetAllBookingsOfType(int status)
        {
            return bookingHandler.GetAllBookingsOfStatus(status);
        }

        [HttpPost]
        [Route("api/Bookings/UpdateStatus")]
        public IHttpActionResult ChangeBookingStatus([FromBody] BookingStatusUpdateModel bookingStatus)
        {
            int rowsAffected = bookingHandler.ChangeBookingStatus(bookingStatus.BookingStatus, bookingStatus.Id);
            if (rowsAffected == 1)
                return Ok("Booking status changed.");
            if (rowsAffected == 0)
                return Ok("Booking error");
            return Ok("Wh... What?");
        }

        [Route("api/Bookings/CheckStatus")]
        public IHttpActionResult checkBookingStatus([FromBody] BookingStatusUpdateModel bookingStatus)
        {
            int status = bookingHandler.GetBookingStatus(bookingStatus.Id);
            int realStatus = -2;
            if (bookingStatus.BookingStatus == BookingStatus.Pending)
            {
                realStatus = 0;
            }
            if (bookingStatus.BookingStatus == BookingStatus.Accepted)
            {
                realStatus = 1;
            }
            if (bookingStatus.BookingStatus == BookingStatus.Cancelled)
            {
                realStatus = 2;
            }
            if (bookingStatus.BookingStatus == BookingStatus.Living)
            {
                realStatus = 3;
            }
            if (realStatus == status)
                return Ok("ok");
            else if (status == 0)
                return Ok("Booking status was changed to pending");
            else if (status == 1)
                return Ok("Booking was already approved");
            else if (status == 2)
                return Ok("Booking was cancelled");
            else if (status == 3)
                return Ok("Booking is already confirmed by student");
            return Ok("Booking does not exist");
        }
    }
}
