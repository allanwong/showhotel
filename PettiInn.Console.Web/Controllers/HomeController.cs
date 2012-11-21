using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using PettiInn.Console.Web.Models;
using PettiInn.SOA.DTO;
using System.Web.Security;
using System.Net.Http;
using System.Threading.Tasks;

namespace PettiInn.Console.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        private const string SESSION_CAPTCHA_LOGIN = "captcha_login";

        [HttpPost]
        public async Task<JsonResult> Login(LoginModel model)
        {
            if (!this.ValidateCaptcha(model.Captcha, SESSION_CAPTCHA_LOGIN))
            {
                return Json(new
                    {
                        success = false,
                        error = "验证码不正确"
                    });
            }

            var result = await this.PostAsync<AdministratorDTO>(this.GetAPIAddress("/api/console/account/Authenticate"), new
            {
                userName = model.UserName,
                password = model.Password
            });

            if (result.Info.IsValid)
            {
                var markResult = await this.PostAsync<AdministratorDTO>(this.GetAPIAddress("/api/console/account/MarkLogin"), new
                    {
                        adminId = result.Id
                    });
                //FormsAuthentication.SetAuthCookie(result.Id.ToString(), model.RememberMe);
                var ticket = new FormsAuthenticationTicket(
                    1, // 版本
                    result.Id.ToString(), // 用户名
                    DateTime.Now, // 票据的签发时间
                    DateTime.Now.Add(FormsAuthentication.Timeout), // 票据的过期时间
                    model.RememberMe, // 是否为永久Cookie
                    result.Id.ToString() // 附加的用户数据，此处用来存放用户角色
                   );
                var encryptedTicket = FormsAuthentication.Encrypt(ticket);
                var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
                Response.Cookies.Add(cookie);
            }

            return Json(new
                {
                    success = result.Info.IsValid,
                    error = result.Info.Errors,
                    entity = result,
                    returnUrl = Url.Action("index", "app")
                });
        }

        public FileContentResult CaptchaLogin()
        {
            var bytes = this.GetCaptcha(SESSION_CAPTCHA_LOGIN);

            return File(bytes, "image/JPEG");
        }
    }
}
