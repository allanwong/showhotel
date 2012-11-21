using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PettiInn.DAL.Entities.EF5;
using PettiInn.DAL.Manager.Core.Managers;
using PettiInn.SOA.DTO;

namespace PettiInn.DAL.Manager.EF5.Managers
{
    internal class HotelManager : ManagerBase<Hotel>, IHotelManager
    {
        public NHResult<Hotel> Create(HotelDTO dto)
        {
            throw new NotImplementedException();
        }

        public NHResult<Hotel> Update(HotelDTO dto)
        {
            throw new NotImplementedException();
        }
    }
}
