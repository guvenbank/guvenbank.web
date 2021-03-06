﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Web.Models;

namespace Web.Controllers
{
    public class AuthController : Controller
    {
        public IActionResult Login()
        {
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginModel loginModel)
        {

            HttpClient httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("http://207.154.196.92:5002");

            string jsonData = JsonConvert.SerializeObject(loginModel);

            var content = new StringContent(jsonData.ToString(), Encoding.UTF8, "application/json");
            HttpResponseMessage response = httpClient.PostAsync("/api/auth/login", content).Result;

            string responseBody = response.Content.ReadAsStringAsync().Result;

            JObject responseJson = JsonConvert.DeserializeObject(responseBody) as JObject;

            if ((Convert.ToInt32(response.StatusCode) == 200) && (responseJson["status"].ToString() == "success"))
            {
                HttpContext.Session.SetString("token", responseJson["token"].ToString());
                HttpContext.Session.SetString("tc", responseJson["tc"].ToString());
                HttpContext.Session.SetString("firstName", responseJson["firstName"].ToString());
                HttpContext.Session.SetString("lastName", responseJson["lastName"].ToString());
                HttpContext.Session.SetString("phoneNumber", responseJson["phoneNumber"].ToString());
                HttpContext.Session.SetString("no", responseJson["no"].ToString());
                
                return RedirectToAction("Index", "Home");
            }
            else if ((Convert.ToInt32(response.StatusCode) == 200) && (responseJson["status"].ToString() == "failed"))
            {
                ViewBag.Error = responseJson["message"].ToString();
            }
            else
            {
                ViewBag.ErrorMessage = "Bir hata oluştu.";
            }

            return View();
        }

        [HttpPost]
        public IActionResult Register(RegisterModel registerModel)
        {

            HttpClient httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("http://207.154.196.92:5002");

            string jsonData = JsonConvert.SerializeObject(registerModel);

            var content = new StringContent(jsonData.ToString(), Encoding.UTF8, "application/json");
            HttpResponseMessage response = httpClient.PostAsync("/api/auth/register", content).Result;

            string responseBody = response.Content.ReadAsStringAsync().Result;

            JObject responseJson = JsonConvert.DeserializeObject(responseBody) as JObject;

            if ((Convert.ToInt32(response.StatusCode) == 200) && (responseJson["status"].ToString() == "success"))
            {
                HttpContext.Session.SetString("token", responseJson["token"].ToString());
                HttpContext.Session.SetString("tc", responseJson["tc"].ToString());
                HttpContext.Session.SetString("firstName", responseJson["firstName"].ToString());
                HttpContext.Session.SetString("lastName", responseJson["lastName"].ToString());
                HttpContext.Session.SetString("phoneNumber", responseJson["phoneNumber"].ToString());
                HttpContext.Session.SetString("no", responseJson["no"].ToString());
                return RedirectToAction("Index", "Account");
            }
            else if ((Convert.ToInt32(response.StatusCode) == 200) && (responseJson["status"].ToString() == "failed"))
            {
                ViewBag.Error = responseJson["message"].ToString();
            }
            else
            {
                ViewBag.ErrorMessage = "Bir hata oluştu.";
            }

            return View();
        }

        public IActionResult Logout()
        {
            HttpContext.Session.SetString("token", "");
            return RedirectToAction("Index", "Home");
        }
    }
}
