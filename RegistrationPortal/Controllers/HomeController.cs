﻿using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RegistrationLibrary;
using RegistrationPortal.Models;

namespace RegistrationPortal.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(RegistrationModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await PerformRequest(model.ToRequest());

                var resultModel = ResultModel.FromResult(result);

                TempData["resultModel"] = Newtonsoft.Json.JsonConvert.SerializeObject(resultModel);

                return RedirectToAction("Result");
            }

            return View(model);
        }

        private async Task<RegistrationResult> PerformRequest(RegistrationRequest request)
        {
            using (var httpClient = new HttpClient())
            {
                var stringContent = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
                using (var response = await httpClient.PostAsync("https://localhost:5001/Registration", stringContent))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<RegistrationResult>(apiResponse);
                }
            }
        }

        public IActionResult Result()
        {
            ResultModel resultModel = null;
            if(TempData["resultModel"] is string s)
            {
                resultModel = JsonConvert.DeserializeObject<ResultModel>(s);
            }

            if (resultModel == null)
                return RedirectToAction("Index");
                
            return View(resultModel);
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
