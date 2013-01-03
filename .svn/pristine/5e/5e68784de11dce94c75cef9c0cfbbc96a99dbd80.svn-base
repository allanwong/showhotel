using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PettiInn.DAL.Entities.EF5;

namespace PettiInn.DAL.Manager.Core.Managers
{
    public interface IRoomBookingManager : IManagerBase<RoomBooking>
    {
        IQueryable<RoomBooking> Search(int? hotelId, IEnumerable<int> roomTypeIds, DateTime? start, DateTime? end, bool tracking = false);
    }
}
