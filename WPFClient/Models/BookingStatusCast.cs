using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFClient.Models
{
    public enum BookingStatus : int
    {
        Pending = 0,
        Accepted = 1,
        Cancelled = 2,
        Living = 3
    }
}
