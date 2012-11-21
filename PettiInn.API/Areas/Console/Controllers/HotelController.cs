using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;
using Newtonsoft.Json.Linq;
using PettiInn.DAL.Manager.Core.Managers;
using PettiInn.SOA.DTO;

namespace PettiInn.API.Areas.Console.Controllers
{
    public class HotelController : ControllerBase
    {
        [System.Web.Mvc.HttpPost]
        public JsonResult Hotels()
        {
            var manager = this.GetManagerFor<IHotelManager>();
            var result = manager.GetAll().ToList();
            var resultDTOs = result.Select(t => new HotelDTO(t));

            return Json(resultDTOs);
        }

        [System.Web.Mvc.HttpPost]
        public JsonResult GetHotel(int id, IEnumerable<string> query)
        {
            var queries = new Query[] { };

            if (query != null)
            {
                queries = query.Select(q => new Query { Name = q }).ToArray();
            }

            var manager = this.GetManagerFor<IHotelManager>();
            var result = manager.GetById(id);
            var resultDTO = new HotelDTO(queries, result);

            return Json(resultDTO);
        }
    }
}
