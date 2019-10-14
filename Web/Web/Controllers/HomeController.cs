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
using Web.Models;

namespace Web.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            //HttpClient httpClient = new HttpClient();
            //httpClient.BaseAddress = new Uri("http://207.154.196.92:5002/");    
            //httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6IjU0ODYyMzU1OTUyIiwibmJmIjoxNTcwODg3Njk3LCJleHAiOjE1NzA4ODc3NTcsImlhdCI6MTU3MDg4NzY5N30.7FAegeFDZPLowt_IZfdSB3wyBfbv3TOMk8gF73zls_s");

            //HttpResponseMessage response = httpClient.GetAsync("api/transaction").Result;

            //string responseBody = response.Content.ReadAsStringAsync().Result;

            //TransactionsModel model = JsonConvert.DeserializeObject<TransactionsModel>(responseBody);

            return View(/*model*/);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
