using ModelLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebService.Models
{
    public class BookingStatusUpdateModel
    {
        public BookingStatus BookingStatus { set; get; }
        public int Id { set; get; }
    }
}