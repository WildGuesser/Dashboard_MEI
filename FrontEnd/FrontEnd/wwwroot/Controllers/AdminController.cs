using FrontEnd.Data.Paging_Models;
using FrontEnd.Models;
using FrontEnd.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using NuGet.Common;
using System.Diagnostics;
using System.Globalization;
using System.Net.Http.Headers;
using System.Text;

namespace FrontEnd.Controllers
{
    public class AdminController : Controller
    {
		private readonly ILogger<AdminController> _logger;
		private readonly string _APIserver;
		private readonly HttpClient _InternalClient;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private List<Trabalhos> Trabalhos_list;

		private List<Alunos> Alunos_list;
		private List<Empresas> Empresas_list;
		private List<Membros> Membros_list;

		private PagingModel pageEmpresas;

		public AdminController (ILogger<AdminController> logger, IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
		{
			_logger = logger;
			_APIserver = configuration.GetSection("WebAPIServers").GetSection("DashboardAPI").Value;
			_InternalClient = new HttpClient();
            _httpContextAccessor = httpContextAccessor;

            // Retrieve the token from the session
            string token = httpContextAccessor.HttpContext.Session.GetString("AuthToken");

            // Set the token in the default request headers
            _InternalClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        }

        public async Task<IActionResult> Index()
        {
            UsersPagingModel model = new UsersPagingModel();

            HttpResponseMessage message = await _InternalClient.GetAsync(_APIserver + "/Auth/List_Users");
            string body = await message.Content.ReadAsStringAsync();
            model.UserList = JsonConvert.DeserializeObject<List<LogInViewModel>>(body);

            if (model.UserList == null)
            {
                model.UserList = new List<LogInViewModel>();
            }

            return View(model);
        }


        [HttpGet]
        public async Task<IActionResult> Delete(LogInViewModel user)
        {


            try
            {
                JustUserDTO justUserDTO = new JustUserDTO();    
                justUserDTO.Username = user.Username;   

                string json = JsonConvert.SerializeObject(justUserDTO);

                var response = await _InternalClient.PostAsync(_APIserver + "/Auth/delete",
                    new StringContent(json, Encoding.UTF8, "application/json"));

                response.EnsureSuccessStatusCode();

                // Redirect to the Index action
                return RedirectToAction(nameof(Index));
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, "Error deleting Alunos objects");
                ViewData["ErrorMessage"] += ex.Message;

                // Handle the exception by returning the Alunos Index view
                return RedirectToAction(nameof(Index));
            }

        }

        [HttpPost]
        public async Task<IActionResult> Register(LogInViewModel user)
        {


            try
            {
                string json = JsonConvert.SerializeObject(user);

                var response = await _InternalClient.PostAsync(_APIserver + "/Auth/register",
                    new StringContent(json, Encoding.UTF8, "application/json"));

                response.EnsureSuccessStatusCode();

                // Redirect to the Index action
                return RedirectToAction(nameof(Index));
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, "Error deleting Alunos objects");
                ViewData["ErrorMessage"] += ex.Message;

                // Handle the exception by returning the Alunos Index view
                return RedirectToAction(nameof(Index));
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