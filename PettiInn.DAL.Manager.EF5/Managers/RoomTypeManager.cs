using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PettiInn.DAL.Entities.EF5;
using PettiInn.DAL.Manager.Core.Managers;

namespace PettiInn.DAL.Manager.EF5.Managers
{
    internal class RoomTypeManager : ManagerBase<RoomType>, IRoomTypeManager
    {
        public NHResult<RoomType> Create(SOA.DTO.RoomTypeDTO dto)
        {
            throw new NotImplementedException();
        }

        public NHResult<RoomType> Update(SOA.DTO.RoomTypeDTO dto)
        {
            throw new NotImplementedException();
        }
    }
}
