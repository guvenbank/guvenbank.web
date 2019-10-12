using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Web.Models;

namespace Web.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Index()
        {
           
           return View();
        }

        public IActionResult Login()
        {
            //LoginModel loginModel = new LoginModel();
            //loginModel.TC = "54862355952";
            //loginModel.Password = "123456";

           
            return View();

        }

        public IActionResult Register()
        {

            return View();
        }

        //[HttpPost]
        //public IActionResult Login(LoginModel loginModel)
        //{

        //    HttpClient httpClient = new HttpClient();
        //    httpClient.BaseAddress = new Uri("http://192.168.1.34:5002/api/auth/login");

        //    string jsonData = JsonConvert.SerializeObject(loginModel);

        //    var content = new StringContent(jsonData.ToString(), Encoding.UTF8, "application/json");
        //    HttpResponseMessage response = httpClient.PostAsync("/api/auth/login", content).Result;

        //    string responseBody = response.Content.ReadAsStringAsync().Result;

        //    JObject responseJson = JsonConvert.DeserializeObject(responseBody) as JObject;


        //    if ((responseJson["status"].ToString() == "success") && (Convert.ToInt32(response.StatusCode) == 200))
        //    {
        //        //HttpContext.Response.Cookies.Append("token", responseJson["token"].ToString());
        //        return RedirectToAction("Index", "Account");
                
        //    }

        //    else if ((responseJson["status"].ToString() == "failed") && (Convert.ToInt32(response.StatusCode) == 200))
        //    {
        //        return RedirectToAction("Login","Account");
        //        //databag ile şifre yada tc yanlış hatası döndür.
        //    }

        //    else
        //    {
        //        // login ekranına dön hata oluştu yazısı yazdır.
        //    }
        //    return View();
        //}

        [HttpPost]
        public IActionResult Register(RegisterModel registerModel)
        {

            HttpClient httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("http://192.168.1.34:5002/api/auth/register");

            string jsonData = JsonConvert.SerializeObject(registerModel);

            var content = new StringContent(jsonData.ToString(), Encoding.UTF8, "application/json");
            HttpResponseMessage response = httpClient.PostAsync("/api/auth/register", content).Result;

            string responseBody = response.Content.ReadAsStringAsync().Result;

            JObject responseJson = JsonConvert.DeserializeObject(responseBody) as JObject;


            if ((responseJson["status"].ToString() == "success") && (Convert.ToInt32(response.StatusCode) == 200))
            {
                return RedirectToAction("Index", "Account");
            }

            else if ((responseJson["status"].ToString() == "failed") && (Convert.ToInt32(response.StatusCode) == 200))
            {
                return RedirectToAction("Register", "Account");
                //databag ile hatayı döndür register sayfasına ve sayfada göster
            }

            else
            {
                // register ekranına dön hata oluştu yazısı yazdır.
            }
            return View();
        }

        //public IActionResult Accounts()
        //{
        //    return View();
        //}

        //[HttpPost]
        //public IActionResult Accounts()
        //{
        //    return View();
        //}
    }
}