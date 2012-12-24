using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PettiInn.DAL.Entities.EF5;

namespace PettiInn.SOA.DTO
{
    public class CurrencyUnitDTO : DTOBase<CurrencyUnit>
    {
        public CurrencyUnitDTO() { }

        public CurrencyUnitDTO(CurrencyUnit entity)
            : base(entity)
        {

        }

        public CurrencyUnitDTO(NHResult<CurrencyUnit> result)
            : base(result)
        {

        }

        public CurrencyUnitDTO(IEnumerable<Query> query, CurrencyUnit entity)
            : base(query, entity)
        {
            
        }

        public string Name { get; set; }
        public string Symbol { get; set; }
        public string EName { get; set; }

        private List<RoomBookingDTO> _RoomBookings = new List<RoomBookingDTO>();

        public List<RoomBookingDTO> RoomBookings
        {
            get { return _RoomBookings; }
            set { _RoomBookings = value; }
        }
    }
}
