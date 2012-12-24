using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;
using PettiInn.DAL.Manager.Core.Managers;
using PettiInn.SOA.DTO;

namespace PettiInn.API.Areas.Console.Controllers
{
    public class CurrencyUnitsController : ControllerBase
    {
        [System.Web.Mvc.HttpPost]
        public JsonResult CurrencyUnits(IEnumerable<Query> queries)
        {
            var manager = this.GetManagerFor<ICurrencyUnitManager>();
            var result = manager.GetAll().ToList();
            var resultDTOs = result.Select(t => new CurrencyUnitDTO(queries, t));

            return Json(resultDTOs);
        }
    }
}
