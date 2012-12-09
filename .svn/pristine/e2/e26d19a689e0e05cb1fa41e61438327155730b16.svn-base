using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PettiInn.DAL.Entities;
using PettiInn.DAL.Entities.EF5;

namespace PettiInn.SOA.DTO
{
    public class HotelDTO : DTOBase<Hotel>
    {
        public HotelDTO() { }

        public HotelDTO(Hotel entity)
            : base(entity)
        {

        }

        public HotelDTO(NHResult<Hotel> result)
            : base(result)
        {

        }

        public HotelDTO(IEnumerable<Query> query, Hotel entity)
            : base(query, entity)
        {
            
        }

        public string Name { get; set; }

        public int Sort { get; set; }

        public string Location { get; set; }

        public string Address { get; set; }

        private List<RoomTypeDTO> _RoomTypes = new List<RoomTypeDTO>();

        public List<RoomTypeDTO> RoomTypes
        {
            get { return _RoomTypes; }
            set { _RoomTypes = value; }
        }

        private List<RoomDTO> _Rooms = new List<RoomDTO>();

        public List<RoomDTO> Rooms
        {
            get { return _Rooms; }
            set { _Rooms = value; }
        }
    }
}
