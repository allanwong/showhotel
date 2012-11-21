using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PettiInn.DAL.Entities;
using PettiInn.DAL.Entities.EF5;
using PettiInn.SOA.DTO;

namespace PettiInn.DAL.Manager.Core.Managers
{
    public interface IRoomTypeManager : IManagerBase<RoomType>
    {
        NHResult<RoomType> Create(RoomTypeDTO dto);

        NHResult<RoomType> Update(RoomTypeDTO dto);
    }
}
