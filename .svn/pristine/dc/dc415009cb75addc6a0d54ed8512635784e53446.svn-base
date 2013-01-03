using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PettiInn.SOA.DTO
{
    public class AvailabilityResultDTO
    {
        private IEnumerable<DateTime> _DTSlots = new List<DateTime>();

        public IEnumerable<DateTime> DTSlots
        {
            get { return _DTSlots; }
            set { _DTSlots = value; }
        }

        private IEnumerable<RoomBookingDTO> _Bookings = new List<RoomBookingDTO>();

        public IEnumerable<RoomBookingDTO> Bookings
        {
            get { return _Bookings; }
            set { _Bookings = value; }
        }

        private IEnumerable<RoomTypeDTO> _RoomTypes = new List<RoomTypeDTO>();

        public IEnumerable<RoomTypeDTO> RoomTypes
        {
            get { return _RoomTypes; }
            set { _RoomTypes = value; }
        }
    }
}
