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
    public class RoleController : ControllerBase
    {
        [System.Web.Mvc.HttpPost]
        public JsonResult Roles()
        {
            var manager = this.GetManagerFor<IRoleManager>();
            var result = manager.GetAll().ToList();
            var resultDTOs = result.Select(t => new RoleDTO(t));

            return Json(resultDTOs);
        }

        [System.Web.Mvc.HttpPost]
        public JsonResult GetRole(int id, IEnumerable<string> query)
        {
            var queries = new Query[] { };

            if (query != null)
            {
                queries = query.Select(q => new Query { Name = q }).ToArray();
            }

            var manager = this.GetManagerFor<IRoleManager>();
            var result = manager.GetById(id);
            var resultDTO = new RoleDTO(queries, result);

            return Json(resultDTO);
        }

        [System.Web.Mvc.HttpPost]
        public JsonResult Create(RoleDTO dto)
        {
            var manager = this.GetManagerFor<IRoleManager>();
            var result = manager.Create(dto);
            var resultDTO = new RoleDTO(result);

            return Json(resultDTO);
        }

        [System.Web.Mvc.HttpPost]
        public JsonResult Update(RoleDTO dto)
        {
            var manager = this.GetManagerFor<IRoleManager>();
            var result = manager.Update(dto);
            var resultDTO = new RoleDTO(result);

            return Json(resultDTO);
        }

        [System.Web.Mvc.HttpPost]
        public JsonResult DeleteRole(RoleDTO dto)
        {
            var manager = this.GetManagerFor<IRoleManager>();
            var role = manager.Delete(dto.Id);
            var result = new RoleDTO(role);

            return Json(result);
        }
    }
}
