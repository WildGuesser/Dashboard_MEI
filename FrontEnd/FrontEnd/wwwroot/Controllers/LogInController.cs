using FrontEnd.Data.Paging_Models;
using FrontEnd.Models;
using FrontEnd.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Net.Http.Headers;
using System.Text;

namespace FrontEnd.Controllers
{
    public class LogInController : Controller
    {
        private readonly ILogger<LogInController> _logger;
        private readonly string _APIserver;
        private readonly HttpClient _InternalClient;
        private List<Trabalhos> Trabalhos_list;

        private List<Alunos> Alunos_list;
        private List<Empresas> Empresas_list;
        private List<Membros> Membros_list;

        private PagingModel pageEmpresas;

        public LogInController(ILogger<LogInController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _APIserver = configuration.GetSection("WebAPIServers").GetSection("DashboardAPI").Value;
            _InternalClient = new HttpClient();
        }

        public async Task<IActionResult> Index()
        { 
                return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LogIn(LogInViewModel login)
        {
            string json = JsonConvert.SerializeObject(login);

            HttpResponseMessage response = await _InternalClient.PostAsync(_APIserver + "/Auth/login",
                  new StringContent(json, Encoding.UTF8, "application/json"));

            if (response.IsSuccessStatusCode)
            {
                string token = await response.Content.ReadAsStringAsync();
                // Store the token in session or set a cookie for subsequent API calls
                HttpContext.Session.SetString("AuthToken", token);
                _InternalClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                return RedirectToAction("Index", "Home");
            }
            else
            {
                // Handle the error case
                ModelState.AddModelError(string.Empty, "Failed to authenticate. Please try again.");
                return View("Index");
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