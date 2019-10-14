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
            HttpClient httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("http://207.154.196.92:5002/");
            httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("token"));

            HttpResponseMessage response = httpClient.GetAsync("api/transaction").Result;

            string responseBody = response.Content.ReadAsStringAsync().Result;

            JObject responseJson = JsonConvert.DeserializeObject(responseBody) as JObject;

            TransactionsModel transactionsModel = JsonConvert.DeserializeObject<TransactionsModel>(responseBody);
            UserModel userModel = new UserModel();

            HomePageModel homePageModel = new HomePageModel();

            userModel.Token = HttpContext.Session.GetString("token");
            userModel.TC = HttpContext.Session.GetString("tc");
            userModel.FirstName = HttpContext.Session.GetString("firstName");
            userModel.LastName = HttpContext.Session.GetString("lastName");
            userModel.PhoneNumber = HttpContext.Session.GetString("phoneNumber");
            userModel.CustomerNo = Convert.ToInt32(HttpContext.Session.GetString("no"));

            homePageModel.TransactionsModel = transactionsModel;
            homePageModel.UserModel = userModel;

            return View(homePageModel);
        }

    }
}