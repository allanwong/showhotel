using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using PettiInn.SOA.DTO;
using PettiInn.Utilities;

namespace PettiInn.Console.Web.Filters
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    internal class RequireSuperAdmin : AuthorizeAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            var user = AsyncHelpers.RunSync<AdministratorDTO>(() =>
                {
                    return httpContext.GetAdministrator();
                }); 

            return user.IsSuper && base.AuthorizeCore(httpContext);
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            throw new Exception("只有超级管理员才能访问该模块");
        }
    }
}