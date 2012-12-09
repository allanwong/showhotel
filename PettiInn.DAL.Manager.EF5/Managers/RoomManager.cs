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
    internal class RoomManager : ManagerBase<Room>, IRoomManager
    {
        public override NHResult<Room> Delete(int id)
        {
            var result = new NHResult<Room>();
            var obj = this.GetById(id);

            if (obj.RoomBookings.Count > 0)
            {
                result.Errors.Add("该房间已经存在预订，无法删除");
            }

            if (result.IsValid)
            {
                result = base.Delete(id);
            }

            return result;
        }

        public NHResult<Room> Create(RoomDTO dto)
        {
            var room = new Room
            {
                Name = dto.Name,
                HasWindow = dto.HasWindow,
                HotelId = dto.HotelId,
                RoomTypeId = dto.RoomTypeId,
                Size = dto.Size,
                Sort = dto.Sort
            };

            var result = base.SaveOrUpdate(room);

            return result;
        }

        public NHResult<Room> Update(RoomDTO dto)
        {
            var room = base.GetById(dto.Id);
            room.Name = dto.Name;
            room.Sort = dto.Sort;
            room.HasWindow = dto.HasWindow;
            room.HotelId = dto.HotelId;
            room.RoomTypeId = dto.RoomTypeId;
            room.Size = dto.Size;

            var result = base.SaveOrUpdate(room);

            return result;
        }
    }
}
