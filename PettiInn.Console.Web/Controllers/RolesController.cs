using PettiInn.Console.Web.Filters;
using PettiInn.SOA.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using PettiInn.SOA.DTO.Shared;
using PettiInn.DAL.Entities.EF5;
using PettiInn.Console.Web.Models;

namespace PettiInn.Console.Web.Controllers
{
    [RequireSuperAdmin]
    public class RolesController : ControllerBase
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<JsonResult> List(SearchCriterias criterias, string name)
        {
            var result = await this.PostAsync<PagingResult<Role, RoleDTO>>(this.GetAPIAddress("/api/console/Role/Roles"), new
                {
                    criterias = criterias,
                    name = name
                });
            return Json(new
                {
                    aaData = result.PageOfDTOs.Select(r => new
                    {
                        Id = r.Id,
                        Name = r.Name,
                        Modules = r.Modules.Select(m => new
                        {
                            Name = m.Name
                        })
                    }),
                    iTotalDisplayRecords = result.TotalDisplay,
                    iTotalRecords = result.Total,
                    //sEcho= "3"
                });
        }

        public async Task<ActionResult> AddEdit(int? id)
        {
            var model = new RoleAddEditModel();
            this.ViewBag.Title = "添加角色";

            if (id != null)
            {
                var role = await this.PostAsync<RoleDTO>(this.GetAPIAddress("/api/console/Role/GetRole"), new
                {
                    id = id.Value,
                    query = new string[] { "Modules" }
                });

                this.ViewBag.Title = string.Format("编辑角色 - {0}", role.Name);

                model.Id = role.Id;
                model.Name = role.Name;
                model.ModuleIds = role.Modules.Select(m => m.Id);
            }

            return View(model);
        }

        [HttpPost]
        public JsonResult AddEdit(RoleAddEditModel model)
        {
            return Json(null);
        }
    }
}
