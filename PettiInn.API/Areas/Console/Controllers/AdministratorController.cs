using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PettiInn.DAL.Manager.Core.Managers;
using PettiInn.SOA.DTO;

namespace PettiInn.API.Areas.Console.Controllers
{
    public class AdministratorController : ControllerBase
    {
        [System.Web.Mvc.HttpPost]
        public JsonResult GetAdministrator(int adminId, IEnumerable<string> query)
        {
            var queries = new Query[] { };

            if (query != null)
            {
                queries = query.Select(q => new Query { Name = q }).ToArray();
            }

            var manager = this.GetManagerFor<IAdministratorManager>();
            var result = manager.GetById(adminId);
            var resultDTO = new AdministratorDTO(queries, result);

            return Json(resultDTO);
        }

        [System.Web.Mvc.HttpPost]
        public JsonResult GetAdministrators()
        {
            var manager = this.GetManagerFor<IAdministratorManager>();
            var result = manager.GetAll().ToList();
            var resultDTOs = result.Select(t => new AdministratorDTO(t));

            return Json(resultDTOs);
        }

        [System.Web.Mvc.HttpPost]
        public JsonResult UpdatePassword(AdministratorDTO dto)
        {
            var manager = this.GetManagerFor<IAdministratorManager>();
            var result = manager.UpdatePassword(dto);
            var resultDTO = new AdministratorDTO(result);

            return Json(resultDTO);
        }

        [System.Web.Mvc.HttpPost]
        public JsonResult UpdateMyPassword(AdministratorDTO dto, string oldPassword)
        {
            var manager = this.GetManagerFor<IAdministratorManager>();
            var result = manager.UpdateMyPassword(dto, oldPassword);
            var resultDTO = new AdministratorDTO(result);

            return Json(resultDTO);
        }

        [System.Web.Mvc.HttpPost]
        public JsonResult CreateAdministrator(AdministratorDTO dto)
        {
            var manager = this.GetManagerFor<IAdministratorManager>();
            var result = manager.Create(dto);
            var resultDTO = new AdministratorDTO(result);

            return Json(resultDTO);
        }

        [System.Web.Mvc.HttpPost]
        public JsonResult UpdateAdministrator(AdministratorDTO dto)
        {
            var manager = this.GetManagerFor<IAdministratorManager>();
            var result = manager.Update(dto);
            var resultDTO = new AdministratorDTO(result);

            return Json(resultDTO);
        }
    }
}
