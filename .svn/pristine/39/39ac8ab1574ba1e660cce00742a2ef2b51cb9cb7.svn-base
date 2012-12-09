using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using PettiInn.DAL.Manager.Core.Managers;
using PettiInn.SOA.DTO;

namespace PettiInn.API.Areas.Console.Controllers
{
    public class AccountController : ControllerBase
    {
        [System.Web.Mvc.HttpPost]
        public JsonResult Authenticate(string userName, string password)
        {
            var manager = this.GetManagerFor<IAdministratorManager>();
            var result = manager.Authenticate(userName, password);
            var resultDTO = new AdministratorDTO(result);

            return Json(resultDTO);
        }

        [System.Web.Mvc.HttpPost]
        public JsonResult MarkLogin(int adminId)
        {
            var manager = this.GetManagerFor<IAdministratorManager>();
            var result = manager.MarkLogin(adminId);
            var resultDTO = new AdministratorDTO(result);

            return Json(resultDTO);
        }
    }
}