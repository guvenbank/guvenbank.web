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
    public class CreditController : Controller
    {
        public IActionResult Index()
        {
            HttpClient httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("http://207.154.196.92:5002/");
            httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("token"));

            HttpResponseMessage response = httpClient.GetAsync("api/transaction").Result;

            string responseBody = response.Content.ReadAsStringAsync().Result;

            if (string.IsNullOrEmpty(responseBody))
            {
                HttpContext.Session.SetString("token", "");
                return RedirectToAction("Index", "Home");
            }

            JObject responseJson = JsonConvert.DeserializeObject(responseBody) as JObject;

            TransactionsModel transactionsModel = JsonConvert.DeserializeObject<TransactionsModel>(responseBody);
            UserModel userModel = new UserModel();

            response = httpClient.GetAsync("api/BankAccount").Result;

            responseBody = response.Content.ReadAsStringAsync().Result;

            responseJson = JsonConvert.DeserializeObject(responseBody) as JObject;
            BankAccountsModel bankAccounts = JsonConvert.DeserializeObject<BankAccountsModel>(responseBody);

            HomePageModel homePageModel = new HomePageModel();

            userModel.Token = HttpContext.Session.GetString("token");
            userModel.TC = HttpContext.Session.GetString("tc");
            userModel.FirstName = HttpContext.Session.GetString("firstName");
            userModel.LastName = HttpContext.Session.GetString("lastName");
            userModel.PhoneNumber = HttpContext.Session.GetString("phoneNumber");
            userModel.CustomerNo = Convert.ToInt32(HttpContext.Session.GetString("no"));

            transactionsModel.transactions = transactionsModel.transactions.OrderBy(x => x.date).ToList();
            bankAccounts.BankAccounts = bankAccounts.BankAccounts.OrderBy(x => x.No).ToList();

            homePageModel.TransactionsModel = transactionsModel;
            homePageModel.UserModel = userModel;
            homePageModel.BankAccountsModel = bankAccounts;

            return View(homePageModel); 
        }

        [HttpPost]
        public IActionResult Post([FromBody] CreditModel creditModel)
        {
            HttpClient httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("http://207.154.196.92:5002/");
            httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("token"));

            HttpResponseMessage response = httpClient.GetAsync("api/customer").Result;

            string responseBody = response.Content.ReadAsStringAsync().Result;

            creditModel.aldigi_kredi_sayi = Convert.ToInt32(responseBody.Remove(0, responseBody.IndexOf("creditNumber") + 14).Split('}')[0]);
            string jsonData = JsonConvert.SerializeObject(creditModel);

            httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("http://ml-python.herokuapp.com/");
            var content = new StringContent(jsonData.ToString(), Encoding.UTF8, "application/json");
            response = httpClient.PostAsync("/api", content).Result;

            responseBody = response.Content.ReadAsStringAsync().Result;

            if (responseBody.Contains("0"))
            {
                httpClient = new HttpClient();
                httpClient.BaseAddress = new Uri("http://207.154.196.92:5002/");
                httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("token"));
                var res = httpClient.PostAsync("api/customer", null).Result;

                return Ok(new { status = "success", message = "Kredi başvurunuz olumlu sonuçlanmıştır." });
            }
            else
            {
                return BadRequest(new { status = "failed", message = "Maalesef size kredi veremiyoruz." });
            }
        }
    }
}