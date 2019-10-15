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
    public class BankAccountController : Controller
    {
 
        public IActionResult List()
        {
            HttpClient httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("http://207.154.196.92:5002/");
            httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("token"));

            HttpResponseMessage response = httpClient.GetAsync("api/BankAccount").Result;

            string responseBody = response.Content.ReadAsStringAsync().Result;

            JObject responseJson = JsonConvert.DeserializeObject(responseBody) as JObject;


            if ((responseJson["status"].ToString() == "success") && (Convert.ToInt32(response.StatusCode) == 200))
            {
                BankAccountsModel bankAccounts = JsonConvert.DeserializeObject<BankAccountsModel>(responseBody);                

                return View(bankAccounts);
            }

            else if ((responseJson["status"].ToString() == "failed") && (Convert.ToInt32(response.StatusCode) == 200))
            {
                //hata döndür
            }

            else
            {
                // hata döndür
            }
            return View();
        }

        [HttpPost]
        public IActionResult Open()
        {
            HttpClient httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("http://207.154.196.92:5002/");
            httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("token"));

            var content = new StringContent("", Encoding.UTF8, "application/json");
            HttpResponseMessage response = httpClient.PostAsync("/api/BankAccount", content).Result;

            string responseBody = response.Content.ReadAsStringAsync().Result;

            JObject responseJson = JsonConvert.DeserializeObject(responseBody) as JObject;


            if ((responseJson["status"].ToString() == "success") && (Convert.ToInt32(response.StatusCode) == 200))
            {
                //Hesap Oluşturuldu
            }

            else if ((responseJson["status"].ToString() == "failed") && (Convert.ToInt32(response.StatusCode) == 200))
            {
                //hata döndür
            }

            else
            {
                // hata döndür
            }
            return View();
        }
    }
}