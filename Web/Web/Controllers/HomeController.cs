using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Web.Models;

namespace Web.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("token")))
            {
                HttpClient httpClient = new HttpClient();
                httpClient.BaseAddress = new Uri("http://207.154.196.92:5002/");
                httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("token"));

                HttpResponseMessage response = httpClient.GetAsync("api/transaction").Result;

                string responseBody = response.Content.ReadAsStringAsync().Result;

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

                return View("dashboard", homePageModel);
            }

            HttpContext.Session.Clear();
            return View();
        }
    }
}
