using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PettiInn.DAL.Manager.Core;
using PettiInn.Utilities;

namespace PettiInn.API
{
    internal static class Extensions
    {
        public static TManager GetManagerFor<TManager>(this Controller controller)
        {
            var manager = WebApiApplication.DALFactory.Create<TManager>();

            return manager;
        }

        public static T GetWebConfigAs<T>(this object obj, string key)
        {
            return ConfigurationManager.AppSettings[key].As<T>();
        }
    }
}