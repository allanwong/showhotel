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
    public class AgentTypeController : ControllerBase
    {
        [System.Web.Mvc.HttpPost]
        public JsonResult List()
        {
            var manager = this.GetManagerFor<IAgentTypeManager>();
            var result = manager.GetAll().ToList();
            var resultDTOs = result.Select(t => new AgentTypeDTO(t));

            return Json(resultDTOs);
        }
    }
}
