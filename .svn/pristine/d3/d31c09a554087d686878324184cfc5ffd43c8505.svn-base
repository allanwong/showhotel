using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Itenso.TimePeriod;
using PettiInn.DAL.Manager.Core.Managers;
using PettiInn.SOA.DTO;
using PettiInn.Utilities;

namespace PettiInn.API.Areas.Services.Controllers
{
    public class BookingsController : ControllerBase
    {
        [HttpPost]
        public JsonResult Availabilities(int hotelId, IEnumerable<int> roomTypeIds, DateTime start, DateTime end, DateTime? startTime, DateTime? endTime, IEnumerable<Query> queries)
        {
            var startDT = start.MergeTime(startTime);
            var endDT = end.MergeTime(endTime);
            var manager = this.GetManagerFor<IRoomBookingManager>();
            var results = manager.Search(hotelId, roomTypeIds, startDT, endDT).ToList();
            
            var periods = new TimeRange(startDT, endDT, true);
            var collector = new CalendarPeriodCollector(new CalendarPeriodCollectorFilter(), periods);
            collector.CollectDays();
            var days = collector.Periods.Select(d => d.Start).ToList();
            days.Add(endDT);
            days.Insert(0, startDT);

            var tManager = this.GetManagerFor<IRoomTypeManager>();
            var roomTypes = tManager.GetByIds(roomTypeIds).ToList();

            var resultDTOs = results.Select(t => new RoomBookingDTO(queries, t));

            var json = new AvailabilityResultDTO
            {
                DTSlots = days,
                RoomTypes = roomTypes.Select(t => new RoomTypeDTO
                {
                    Id = t.Id,
                    Name = t.Name,
                    Rooms = t.Rooms.Where(r => r.HotelId == hotelId).Select(r => new RoomDTO(r)).ToList()
                }).ToList(),
                Bookings = resultDTOs
            };

            return Json(json);
        }
    }
}