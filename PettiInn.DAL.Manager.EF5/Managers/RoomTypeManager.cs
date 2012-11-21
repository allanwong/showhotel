using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PettiInn.DAL.Entities.EF5;
using PettiInn.DAL.Manager.Core.Managers;
using PettiInn.SOA.DTO;

namespace PettiInn.DAL.Manager.EF5.Managers
{
    internal class RoomTypeManager : ManagerBase<RoomType>, IRoomTypeManager
    {
        public NHResult<RoomType> Create(RoomTypeDTO dto)
        {
            var roomType = new RoomType
            {
                Name = dto.Name,
                Sort = dto.Sort
            };

            var result = base.SaveOrUpdate(roomType);

            return result;
        }

        public NHResult<RoomType> Update(RoomTypeDTO dto)
        {
            var roomType = base.GetById(dto.Id);
            roomType.Name = dto.Name;
            roomType.Sort = dto.Sort;

            var result = base.SaveOrUpdate(roomType);

            return result;
        }

        public override NHResult<RoomType> Delete(int id)
        {
            var result = new NHResult<RoomType>();
            var obj = this.GetById(id);

            if (obj.Rooms.Count > 0)
            {
                result.Errors.Add("还有该房型的房间，无法删除");
            }

            if (result.IsValid)
            {
                obj.Hotels.Clear();
                obj.Prices.Clear();
                obj.RoomTypeImages.Clear();
                result = base.Delete(id);
            }

            return result;
        }
    }
}
