using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PettiInn.DAL.Entities;
using PettiInn.DAL.Entities.EF5;

namespace PettiInn.SOA.DTO
{
    public class RoomDTO : DTOBase<Room>
    {
        public RoomDTO() { }

        public RoomDTO(Room entity)
            : base(entity)
        {

        }

        public RoomDTO(NHResult<Room> result)
            : base(result)
        {

        }

        public RoomDTO(IEnumerable<Query> query, Room entity)
            : base(query, entity)
        {
            
        }

        public string Name { get; set; }

        public int Sort { get; set; }

        public int? Size { get; set; }

        public bool HasWindow { get; set; }

        public string Facilities { get; set; }

        public int HotelId { get; set; }

        public HotelDTO Hotel { get; set; }

        public int RoomTypeId { get; set; }

        public RoomTypeDTO RoomType { get; set; }
    }
}
