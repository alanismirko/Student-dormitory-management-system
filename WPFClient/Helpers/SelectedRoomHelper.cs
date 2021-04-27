using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFClient.Models;

namespace WPFClient.Helpers
{
    public class SelectedRoomHelper
    {
        public IEnumerable<RoomCast> rooms { get; set; }
        private static readonly SelectedRoomHelper instance = new SelectedRoomHelper();

        private SelectedRoomHelper()
        {
        }

        public static SelectedRoomHelper Instance
        {
            get
            {
                return instance;
            }
        }

        public RoomCast GetSelectedRoom(int id)
        {
            RoomCast nullRoom = null;
            foreach (RoomCast room in rooms)
            {
                if (room.Id == id)
                    return room;
            }
            return nullRoom = null;
        }
    }
}

