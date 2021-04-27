using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFClient.Models
{
    public class RoomCast
    {
        public int Id { get; set; }
        public int RoomNumber { get; set; }
        public int Floor { get; set; }
        public int Capacity { get; set; }
        public double Area { get; set; }
        public double Price { get; set; }
        public string Description { get; set; }
        public bool IsAvailable { get; set; }
        public DateTime modificationDate { get; set; }
    }

}
