﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using PettiInn.SOA.DTO;
using PettiInn.Console.Web.Filters;
using PettiInn.Utilities;
using System.Threading.Tasks;

namespace PettiInn.Console.Web.Controllers
{
    public class AppController : ControllerBase
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            this.Session.Clear();
            this.Session.Abandon();

            return RedirectToAction("index", "home");
        }

        [HttpPost]
        public async Task<JsonResult> GetRootModules()
        {
            var admin = await this.GetAdministrator();

            var result = await this.PostAsync<IEnumerable<ModuleDTO>>(this.GetAPIAddress("/api/console/module/GetRootModules"), new
                {
                    adminId = admin.Id
                });
            return Json(result);
        }

        [HttpPost]
        [RequireSuperAdmin]
        public async Task<JsonResult> GetAdministrators()
        {
            var result = await this.PostAsync<IEnumerable<AdministratorDTO>>(this.GetAPIAddress("/api/console/Administrator/GetAdministrators"));

            return Json(result);
        }

        [HttpPost]
        [RequireSuperAdmin]
        public async Task<JsonResult> GetAdministrator(int adminId)
        {
            var result = await this.PostAsync<AdministratorDTO>(this.GetAPIAddress("/api/console/Administrator/GetAdministrator"), new
                {
                    adminId = adminId,
                    query = new string[] { "Roles" }
                });
            return Json(result);
        }

        [HttpPost]
        [RequireSuperAdmin]
        public async Task<JsonResult> CreateAdministrator(AdministratorDTO dto)
        {
            var result = await this.PostAsync<AdministratorDTO>(this.GetAPIAddress("/api/console/Administrator/CreateAdministrator"), dto);

            return Json(new
            {
                success = result.Info.IsValid,
                error = result.Info.Errors
            });
        }

        [HttpPost]
        [RequireSuperAdmin]
        public async Task<JsonResult> UpdateAdministrator(AdministratorDTO dto)
        {
            var result = await this.PostAsync<AdministratorDTO>(this.GetAPIAddress("/api/console/Administrator/UpdateAdministrator"), dto);

            return Json(new
            {
                success = result.Info.IsValid,
                error = result.Info.Errors
            });
        }

        [HttpPost]
        [RequireSuperAdmin]
        public async Task<JsonResult> UpdatePassword(AdministratorDTO dto)
        {
            var result = await this.PostAsync<AdministratorDTO>(this.GetAPIAddress("/api/console/Administrator/UpdatePassword"), dto);

            return Json(new
            {
                success = result.Info.IsValid,
                error = result.Info.Errors
            });
        }

        [HttpPost]
        public async Task<JsonResult> UpdateMyPassword(AdministratorDTO dto, string OldPassword)
        {
            var user = await this.GetAdministrator();
            dto.Id = user.Id;

            var result = await this.PostAsync<AdministratorDTO>(this.GetAPIAddress("/api/console/Administrator/UpdateMyPassword"), new
                {
                    dto = dto,
                    oldPassword = OldPassword
                });

            return Json(new
            {
                success = result.Info.IsValid,
                error = result.Info.Errors
            });
        }

        [HttpPost]
        [RequireSuperAdmin]
        public async Task<JsonResult> Roles()
        {
            var result = await this.PostAsync <IEnumerable<RoleDTO>>(this.GetAPIAddress("/api/console/Role/Roles"));
            return Json(result);
        }

        [HttpPost]
        [RequireSuperAdmin]
        public async Task<JsonResult> CreateRole(RoleDTO dto)
        {
            var result = await this.PostAsync<RoleDTO>(this.GetAPIAddress("/api/console/Role/Create"), new
                {
                    dto = dto
                });

            return Json(new
            {
                success = result.Info.IsValid,
                error = result.Info.Errors
            });
        }

        [HttpPost]
        [RequireSuperAdmin]
        public async Task<JsonResult> UpdateRole(RoleDTO dto)
        {
            var result = await this.PostAsync<RoleDTO>(this.GetAPIAddress("/api/console/Role/Update"), new
                {
                    dto = dto
                });

            return Json(new
            {
                success = result.Info.IsValid,
                error = result.Info.Errors
            });
        }

        [HttpPost]
        [RequireSuperAdmin]
        public async Task<JsonResult> DeleteRole(RoleDTO dto)
        {
            var result = await this.PostAsync<RoleDTO>(this.GetAPIAddress("/api/console/Role/DeleteRole"), new
                {
                    dto = dto
                });

            return Json(new
            {
                success = result.Info.Extra["count"].As<int>() > 0,
                error = result.Info.Errors
            });
        }

        [HttpPost]
        [RequireSuperAdmin]
        public async Task<JsonResult> GetRole(int roleId)
        {
            var result = await this.PostAsync<RoleDTO>(this.GetAPIAddress("/api/console/Role/GetRole"), new
                {
                    id = roleId,
                    query = new string[] { "Modules" }
                });
            return Json(result);
        }

        [HttpPost]
        [RequireSuperAdmin]
        public async Task<JsonResult> Modules()
        {
            var result = await this.PostAsync <IEnumerable<ModuleDTO>>(this.GetAPIAddress("/api/console/Module/GetModules"));
            var modules = result.Select(m => new
                {
                    Id = m.Id,
                    Name = m.Name
                });
            return Json(result);
        }

        [HttpPost]
        public async Task<JsonResult> Hotels()
        {
            var result = await this.PostAsync < IEnumerable<HotelDTO>>(this.GetAPIAddress("/api/console/Hotel/Hotels"));
            return Json(result);
        }

        [HttpPost]
        public async Task<JsonResult> GetHotel(int hotelId)
        {
            var result = await this.PostAsync<HotelDTO>(this.GetAPIAddress("/api/console/Hotel/GetHotel"), new
                {
                    id = hotelId,
                    query = new string[] { "RoomTypes" }
                });
            return Json(result);
        }

        [HttpPost]
        [RequireSuperAdmin]
        public JsonResult CreateHotel(HotelDTO dto)
        {
            //var result = ConsoleService.CreateHotel(dto);
            //return Json(new
            //{
            //    success = result.Info.IsValid,
            //    error = result.Info.Errors
            //});
            return null;
        }

        [HttpPost]
        [RequireSuperAdmin]
        public JsonResult UpdateHotel(HotelDTO dto)
        {
            //var result = ConsoleService.UpdateHotel(dto);
            //return Json(new
            //{
            //    success = result.Info.IsValid,
            //    error = result.Info.Errors
            //});
            return null;
        }

        [HttpPost]
        [RequireSuperAdmin]
        public JsonResult DeleteHotel(HotelDTO dto)
        {
            //var result = ConsoleService.DeleteHotel(dto);
            //return Json(new
            //{
            //    success = result.Info.Extra["count"].As<int>() > 0,
            //    error = result.Info.Errors
            //});
            return null;
        }

        [HttpPost]
        public async Task<JsonResult> RoomTypes()
        {
            var result = await this.PostAsync < IEnumerable<RoomTypeDTO>>(this.GetAPIAddress("/api/console/RoomType/RoomTypes"));
            return Json(result);
        }

        [HttpPost]
        public async Task<JsonResult> GetRoomType(int roomTypeId)
        {
            var result = await this.PostAsync<RoomTypeDTO>(this.GetAPIAddress("/api/console/RoomType/GetRoomType"), new
                {
                    id = roomTypeId
                });
            return Json(result);
        }

        [HttpPost]
        [RequireSuperAdmin]
        public JsonResult CreateRoomType(RoomTypeDTO dto)
        {
            //var result = ConsoleService.CreateRoomType(dto);
            //return Json(new
            //{
            //    success = result.Info.IsValid,
            //    error = result.Info.Errors
            //});
            return null;
        }

        [HttpPost]
        [RequireSuperAdmin]
        public JsonResult UpdateRoomType(RoomTypeDTO dto)
        {
            //var result = ConsoleService.UpdateRoomType(dto);
            //return Json(new
            //{
            //    success = result.Info.IsValid,
            //    error = result.Info.Errors
            //});
            return null;
        }

        [HttpPost]
        [RequireSuperAdmin]
        public JsonResult DeleteRoomType(RoomTypeDTO dto)
        {
            //var result = ConsoleService.DeleteRoomType(dto);
            //return Json(new
            //{
            //    success = result.Info.Extra["count"].As<int>() > 0,
            //    error = result.Info.Errors
            //});
            return null;
        }
    }
}
