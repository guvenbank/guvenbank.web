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
    public class TransactionController : Controller
    {
        public IActionResult Index()
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

            return View(homePageModel);
        }

        [HttpPost]
        public IActionResult External(TransactionModel transactionModel)
        {
            transactionModel.date = DateTime.Now;
            HttpClient httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("http://207.154.196.92:5002/");
            httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("token"));

            string jsonData = JsonConvert.SerializeObject(transactionModel);

            var content = new StringContent(jsonData.ToString(), Encoding.UTF8, "application/json");
            HttpResponseMessage response = httpClient.PostAsync("/api/Transaction/external", content).Result;

            string responseBody = response.Content.ReadAsStringAsync().Result;

            JObject responseJson = JsonConvert.DeserializeObject(responseBody) as JObject;


            if ((Convert.ToInt32(response.StatusCode) == 200) && (responseJson["status"].ToString() == "failed"))
            {
                return BadRequest(new { status = "failed", message = responseJson["message"].ToString() });
            }
            else if ((Convert.ToInt32(response.StatusCode) != 200))
            {
                return BadRequest(new { status = "failed", message = "Bir hata oluştu." });
            }

            return Ok();
        }

        [HttpPost]
        public IActionResult Internal(TransactionModel transactionModel)
        {
            transactionModel.date = DateTime.Now;
            HttpClient httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("http://207.154.196.92:5002/");
            httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("token"));

            string jsonData = JsonConvert.SerializeObject(transactionModel);

            var content = new StringContent(jsonData.ToString(), Encoding.UTF8, "application/json");
            HttpResponseMessage response = httpClient.PostAsync("/api/Transaction/Internal", content).Result;

            string responseBody = response.Content.ReadAsStringAsync().Result;

            JObject responseJson = JsonConvert.DeserializeObject(responseBody) as JObject;


            if ((Convert.ToInt32(response.StatusCode) == 200) && (responseJson["status"].ToString() == "failed"))
            {
                return BadRequest(new { status = "failed", message = responseJson["message"].ToString() });
            }
            else if ((Convert.ToInt32(response.StatusCode) != 200))
            {
                return BadRequest(new { status = "failed", message = "Bir hata oluştu." });
            }

            return Ok();
        }
    }
}