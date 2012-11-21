using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using System.Drawing.Imaging;
using PettiInn.SOA.DTO;
using PettiInn.Utilities;
using System.Configuration;
using System.Threading.Tasks;
using System.Net.Http;

namespace PettiInn.Console.Web
{
    public static class Extensions
    {
        public static byte[] GetCaptcha(this Controller controller, string sessionKey)
        {
            using (var ms = new MemoryStream())
            {
                var image = new CaptchaImage(5, 18, false);

                controller.Session[sessionKey] = image;

                image.CaptchaFrame.Save(ms, ImageFormat.Jpeg);

                return ms.GetBuffer();
            }
        }

        public static bool ValidateCaptcha(this Controller controller, string captcha, string key)
        {
            bool result = false;

            if (!string.IsNullOrWhiteSpace(captcha))
            {
                var capt = controller.Session[key] as CaptchaImage;

                if (capt != null && !string.IsNullOrWhiteSpace(capt.CaptchaCode))
                {
                    result = captcha.Equals(capt.CaptchaCode, StringComparison.OrdinalIgnoreCase);
                }
            }

            return result;
        }

        internal const string KEY_LOGGEDIN_ADMINI = "PettiInn.Logged.Administrator";
        public async static Task<AdministratorDTO> GetAdministrator(this Controller controller)
        {
            return await controller.HttpContext.GetAdministrator();
        }

        public static async Task<AdministratorDTO> GetAdministrator(this HttpContextBase context)
        {
            var user = default(AdministratorDTO);

            if (HttpContext.Current.Request.IsAuthenticated)
            {
                user = context.Session[KEY_LOGGEDIN_ADMINI] as AdministratorDTO;

                if (user == null)
                {
                    user = await context.PostAsync<AdministratorDTO>(context.GetAPIAddress("/api/console/Administrator/GetAdministrator"), new
                        {
                            adminId = HttpContext.Current.User.Identity.Name.As<int>()
                        });
                    context.Session[KEY_LOGGEDIN_ADMINI] = user;
                }
            }

            return user;
        }

        public static T GetWebConfigAs<T>(this object obj, string key)
        {
            return ConfigurationManager.AppSettings[key].As<T>();
        }

        public static string GetAPIAddress(this object controller, string address)
        {
            var baseAddr = controller.GetWebConfigAs<string>("SOA.ServiceBase");
            var addr = string.Concat(baseAddr, address.TrimStart('/'));

            return addr;
        }

        public async static Task<TResult> PostAsync<TResult>(this object controller, string url, object data = default(object))
        {
            using (var client = new HttpClient())
            {
                var response = await client.PostAsJsonAsync(url, data);
                response.EnsureSuccessStatusCode();
                var content = await response.Content.ReadAsAsync<TResult>();

                return content;
            }
        }
    }
}