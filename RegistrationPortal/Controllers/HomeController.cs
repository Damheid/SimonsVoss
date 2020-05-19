using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RegistrationLibrary;
using RegistrationPortal.Models;

namespace RegistrationPortal.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IConfiguration configuration;

        public HomeController(ILogger<HomeController> logger,
                              IConfiguration configuration)
        {
            this.configuration = configuration;
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
                var apiAddress = configuration.GetValue<string>("ApiAddress");
                var registrationEndpoint = CombineUrl(apiAddress, "Registration");
                var stringContent = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
                using (var response = await httpClient.PostAsync(registrationEndpoint, stringContent))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<RegistrationResult>(apiResponse);
                }
            }
        }

        private static string CombineUrl(string uri1, string uri2)
        {
            uri1 = uri1.TrimEnd('/');
            uri2 = uri2.TrimStart('/');
            return string.Format("{0}/{1}", uri1, uri2);
        }

        public IActionResult Result()
        {
            ResultModel resultModel = null;
            if (TempData["resultModel"] is string s)
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
