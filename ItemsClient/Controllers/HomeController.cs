using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ItemsClient.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace ItemsClient.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IConfiguration _config;
        private readonly HttpClient _httpClient;
        public HomeController(ILogger<HomeController> logger, IConfiguration configuration, HttpClient httpClient)
        {
            _logger = logger;
            _config = configuration;
            _httpClient = httpClient;
        }

        public  IActionResult Index()
        {
            
            return View();
           
        }

        [HttpPost]
        public async Task<JsonResult> CreateItem(string ItemName, double ItemPrice)
        {
            ItemsViewModel ReceivedItem = new ItemsViewModel();
            ReceivedItem.ItemName = ItemName;
            ReceivedItem.Price = ItemPrice;
            var apiCreateUri = _config["APIcallurl:create"]; 
           
               StringContent content = new StringContent(JsonConvert.SerializeObject(ReceivedItem), Encoding.UTF8, "application/json");

          
            HttpResponseMessage response = await _httpClient.PostAsync(apiCreateUri, content);
            if (response.IsSuccessStatusCode)
            {
                return Json(new { success = true, responseText = "Saved" });
            }
            else
            {
                return Json(new { success = false, responseText = response.ReasonPhrase });
            }
        }


        [HttpPost]
        public async Task<JsonResult> UpdateItem(int ItemId, string ItemName, double ItemPrice)
        {
            ItemsViewModel ReceivedItem = new ItemsViewModel();
            ReceivedItem.ItemName = ItemName;
            ReceivedItem.Price = ItemPrice;

            ReceivedItem.ItemId = ItemId;
            var apiUpdateUri = _config["APIcallurl:update"];
           
                StringContent content = new StringContent(JsonConvert.SerializeObject(ReceivedItem), Encoding.UTF8, "application/json");

                HttpResponseMessage response = await _httpClient.PostAsync(apiUpdateUri, content);
                
                    if (response.IsSuccessStatusCode)
                    {
                        return Json(new { success = true, responseText = "Saved" });
                    }
                    else
                    {
                        return Json(new { success = false, responseText = response.ReasonPhrase });
                    }
                
            

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
