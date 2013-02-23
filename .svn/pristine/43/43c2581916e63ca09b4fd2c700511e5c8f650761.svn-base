using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using PettiInn.Console.Web.Filters;
using PettiInn.SOA.DTO;

namespace PettiInn.Console.Web.Controllers
{
    [RequireSuperAdmin]
    public class ModuleController : ControllerBase
    {
        public async Task<JsonResult> Tree()
        {
            var modules = await this.PostAsync<IEnumerable<ModuleDTO>>(this.GetAPIAddress("/api/console/Module/GetAll"));
            var tree = modules.ToTree();

            return Json(tree);
        }
    }
}
