using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFClient.Models
{
    class BookingCast
    {
        public int Id { get; set; }

        public DateTime CreationDate { get; set; }

        public DateTime MoveInDate { get; set; }

        public DateTime MoveOutDate { get; set; }

        public BookingStatus Status { get; set; }

        public int RoomId { get; set; }

    }
}
