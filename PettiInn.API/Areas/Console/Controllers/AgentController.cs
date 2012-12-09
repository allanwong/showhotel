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
    public class AgentController : ControllerBase
    {
        [System.Web.Mvc.HttpPost]
        public JsonResult List(IEnumerable<Query> queries)
        {
            var manager = this.GetManagerFor<IAgentManager>();
            var result = manager.GetAll().ToList();
            var resultDTOs = result.Select(t => new AgentDTO(queries,t));

            return Json(resultDTOs);
        }

        [System.Web.Mvc.HttpPost]
        public JsonResult Delete(AgentDTO dto)
        {
            var manager = this.GetManagerFor<IAgentManager>();
            var result = manager.Delete(dto.Id);
            var resultDTO = new AgentDTO(result);

            return Json(resultDTO);
        }
    }
}
