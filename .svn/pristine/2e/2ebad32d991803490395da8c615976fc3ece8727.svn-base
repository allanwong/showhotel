using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PettiInn.DAL.Entities;
using PettiInn.DAL.Entities.EF5;

namespace PettiInn.SOA.DTO
{
    public class RoomTypeDTO : DTOBase<RoomType>
    {
        public RoomTypeDTO() { }

        public RoomTypeDTO(RoomType entity)
            : base(entity)
        {

        }

        public RoomTypeDTO(NHResult<RoomType> result)
            : base(result)
        {

        }

        public RoomTypeDTO(IEnumerable<Query> query, RoomType entity)
            : base(query, entity)
        {
            
        }

        public string Name { get; set; }

        public int Sort { get; set; }

        public string Location { get; set; }

        public string Address { get; set; }

        private List<HotelDTO> _Hotels = new List<HotelDTO>();

        public List<HotelDTO> Hotels
        {
            get { return _Hotels; }
            set { _Hotels = value; }
        }

        private List<RoomDTO> _Rooms = new List<RoomDTO>();

        public List<RoomDTO> Rooms
        {
            get { return _Rooms; }
            set { _Rooms = value; }
        }
    }
}
