﻿using System;
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
    public class RoomTypeController : ControllerBase
    {
        [System.Web.Mvc.HttpPost]
        public JsonResult RoomTypes()
        {
            var manager = this.GetManagerFor<IRoomTypeManager>();
            var result = manager.GetAll().ToList();
            var resultDTOs = result.Select(t => new RoomTypeDTO(t));

            return Json(resultDTOs);
        }

        [System.Web.Mvc.HttpPost]
        public JsonResult GetRoomType(int id, IEnumerable<string> query)
        {
            var queries = new Query[] { };

            if (query != null)
            {
                queries = query.Select(q => new Query { Name = q }).ToArray();
            }

            var manager = this.GetManagerFor<IRoomTypeManager>();
            var result = manager.GetById(id);
            var resultDTO = new RoomTypeDTO(queries, result);

            return Json(resultDTO);
        }
    }
}
