using FrontEnd.Data.Paging_Models;
using FrontEnd.Models;
using FrontEnd.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using NuGet.Common;
using System.Diagnostics;
using System.Globalization;
using System.Net.Http.Headers;

namespace FrontEnd.Controllers
{
    public class HomeController : Controller
    {
		private readonly ILogger<HomeController> _logger;
		private readonly string _APIserver;
		private readonly HttpClient _InternalClient;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private List<Trabalhos> Trabalhos_list;

		private List<Alunos> Alunos_list;
		private List<Empresas> Empresas_list;
		private List<Membros> Membros_list;

		private PagingModel pageEmpresas;

		public HomeController (ILogger<HomeController> logger, IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
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
            HomeViewModel model = new HomeViewModel();



            HttpResponseMessage message = await _InternalClient.GetAsync(_APIserver + "/Alunos/List_Active");
            string body = await message.Content.ReadAsStringAsync();
            List<Alunos> alunosList = JsonConvert.DeserializeObject<List<Alunos>>(body);

            if (alunosList != null && alunosList.Count > 0)
            {
                List<Alunos> sortedList = alunosList.OrderByDescending(a => a.Numero_Aluno).ToList();
                model.Alunos = sortedList.Take(5).ToList();

                // Count Alunos per Edicao
                Dictionary<int, int> tipoCount = alunosList
                    .GroupBy(t => t.Edicao)
                    .ToDictionary(g => g.Key, g => g.Count());

                model.Anos = tipoCount.Keys.ToArray();
                model.AnosN = tipoCount.Values.ToArray();
            }
            else
            {
                model.Alunos = new List<Alunos>();
            }

            message = await _InternalClient.GetAsync(_APIserver + "/Home/Index");
            body = await message.Content.ReadAsStringAsync();

            // Deserialize the response into a dynamic object
            dynamic responseData = JsonConvert.DeserializeObject(body);

            // Set the properties of the HomeViewModel object manually
            model.Nalunos = responseData != null ? responseData["nalunos"] : null;
            model.NTrabalhos = responseData != null ? responseData["nTrabalhos"] : null;
            model.Nmenbros = responseData != null ? responseData["nmenbros"] : null;

            // Convert the nearest date from string to DateOnly
            string nearestDateStr = responseData != null ? responseData["dataMaisProxima"] : null;
            if (DateTime.TryParseExact(nearestDateStr, "MM/dd/yyyy HH:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime nearestDate))
            {
                model.DataMaisProxima = nearestDate;
            }

            message = await _InternalClient.GetAsync(_APIserver + "/Trabalhos/Index");
            body = await message.Content.ReadAsStringAsync();
            Trabalhos_list = JsonConvert.DeserializeObject<List<Trabalhos>>(body);

            if (Trabalhos_list != null && Trabalhos_list.Count > 0)
            {
                List<Trabalhos> sortedList = Trabalhos_list.OrderBy(a => a.Data_Defesa).ToList();
                model.Trabalhos = sortedList.Take(5).ToList();

                // Count the number of Trabalhos for each Tipo
                Dictionary<string, int> tipoCount = Trabalhos_list
                    .GroupBy(t => t.Tipo)
                    .ToDictionary(g => g.Key, g => g.Count());

                // Assign the dictionary values to the model properties
                model.Tipos = tipoCount.Keys.ToArray();
                model.TipoN = tipoCount.Values.ToArray();
            }
            else
            {
                model.Trabalhos = new List<Trabalhos>();
                model.Tipos = new string[0];
                model.TipoN = new int[0];
            }

            return View(model);
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