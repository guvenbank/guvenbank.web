using System;
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
                return Ok();
            }

            else if ((responseJson["status"].ToString() == "failed") && (Convert.ToInt32(response.StatusCode) == 200))
            {
                return BadRequest();
            }

            else
            {
                return BadRequest();
            }
        }


        [HttpPost]
        public IActionResult External(TransactionModel transactionModel)
        {
            HttpClient httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("http://207.154.196.92:5002/");

            string jsonData = JsonConvert.SerializeObject(transactionModel);

            var content = new StringContent(jsonData.ToString(), Encoding.UTF8, "application/json");
            HttpResponseMessage response = httpClient.PostAsync("/api/Transaction/external", content).Result;

            string responseBody = response.Content.ReadAsStringAsync().Result;

            JObject responseJson = JsonConvert.DeserializeObject(responseBody) as JObject;


            if ((responseJson["status"].ToString() == "success") && (Convert.ToInt32(response.StatusCode) == 200))
            {
                return RedirectToAction("TransferSummary", "BankAccount", transactionModel);
            }

            else if ((responseJson["status"].ToString() == "failed") && (Convert.ToInt32(response.StatusCode) == 200))
            {
                return RedirectToAction("Register", "Auth");
                //databag ile hatayı döndür register sayfasına ve sayfada göster
            }

            else
            {
                // register ekranına dön hata oluştu yazısı yazdır.
            }
            return View();
        }


        [HttpPost]
        public IActionResult Internal(TransactionModel transactionModel)
        {
            HttpClient httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("http://207.154.196.92:5002/");

            string jsonData = JsonConvert.SerializeObject(transactionModel);

            var content = new StringContent(jsonData.ToString(), Encoding.UTF8, "application/json");
            HttpResponseMessage response = httpClient.PostAsync("/api/Transaction/internal", content).Result;

            string responseBody = response.Content.ReadAsStringAsync().Result;

            JObject responseJson = JsonConvert.DeserializeObject(responseBody) as JObject;


            if ((responseJson["status"].ToString() == "success") && (Convert.ToInt32(response.StatusCode) == 200))
            {
                return RedirectToAction("TransferSummary", "BankAccount", transactionModel);
            }

            else if ((responseJson["status"].ToString() == "failed") && (Convert.ToInt32(response.StatusCode) == 200))
            {
                return RedirectToAction("Register", "Auth");
                //databag ile hatayı döndür register sayfasına ve sayfada göster
            }

            else
            {
                // register ekranına dön hata oluştu yazısı yazdır.
            }
            return View();
        }




    }
}