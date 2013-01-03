using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Itenso.TimePeriod;
using PettiInn.DAL.Entities.EF5;
using PettiInn.DAL.Manager.Core.Managers;

namespace PettiInn.DAL.Manager.EF5.Managers
{
    internal class RoomBookingManager : ManagerBase<RoomBooking>, IRoomBookingManager
    {
        public IQueryable<RoomBooking> Search(int? hotelId, IEnumerable<int> roomTypeIds, DateTime? start, DateTime? end, bool tracking = false)
        {
            var q = base.Query();

            if (!tracking)
            {
                q = q.AsNoTracking();
            }

            if (hotelId != null)
            {
                q = q.Where(r => r.HotelId == hotelId.Value);
            }

            if (start == null || end == null)
            {
                if (start != null)
                {
                    q = q.Where(r => r.Start >= start.Value);
                }
                else if (end != null)
                {
                    q = q.Where(r => r.End <= end.Value);
                }
            }

            if (roomTypeIds != null && roomTypeIds.Count() > 0)
            {
                q = from rm in q join r in base.DBContext.Room on rm.RoomId equals r.Id join rt in base.DBContext.RoomType on r.RoomTypeId equals rt.Id
                    where roomTypeIds.Contains(r.RoomTypeId)
                    select rm;
            }

            q = q.OrderByDescending(r => r.BookingDate).Include(r => r.Room.RoomType);
            q = q.ToList().AsQueryable();// executes the query

            if (start != null && end != null)
            {
                var block = new TimeRange(start.Value, end.Value, true);
                q = q.Where(r => new TimeRange(r.Start, r.End, true).IntersectsWith(block));
            }

            return q;
        }

        internal override DbSet<RoomBooking> DBSet
        {
            get { return base.DBContext.RoomBooking; }
        }
    }
}
