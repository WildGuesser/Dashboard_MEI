using FrontEnd.Data.Paging_Models;
using FrontEnd.Models;
using FrontEnd.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;

namespace FrontEnd.Controllers
{
	public class ChartsController : Controller
	{
		private readonly ILogger<ChartsController> _logger;
		private readonly string _APIserver;
		private readonly HttpClient _InternalClient;
		private List<Trabalhos> Trabalhos_list;

		private List<Alunos> Alunos_list;
		private List<Empresas> Empresas_list;
		private List<Membros> Membros_list;

		private PagingModel pageEmpresas;

		public ChartsController(ILogger<ChartsController> logger, IConfiguration configuration)
		{
			_logger = logger;
			_APIserver = configuration.GetSection("WebAPIServers").GetSection("DashboardAPI").Value;
			_InternalClient = new HttpClient();
		}

		public async Task<IActionResult> Index()
		{
			ChartsViewModel model = new ChartsViewModel();

			HttpResponseMessage message = await _InternalClient.GetAsync(_APIserver + "/Alunos/List_Active");
			string body = await message.Content.ReadAsStringAsync();
			List<Alunos> alunosList = JsonConvert.DeserializeObject<List<Alunos>>(body);

			if (alunosList != null && alunosList.Count > 0)
			{


				// Count Alunos per Edicao
				Dictionary<int, int> AnoCount = alunosList
					.GroupBy(t => t.Edicao)
					.ToDictionary(g => g.Key, g => g.Count());

				//Count Alunos per Instituição
				Dictionary<string, int> InstituicaoCount = alunosList
					.GroupBy(t => t.Instituicao)
					.ToDictionary(g => g.Key, g => g.Count());


				model.Anos = AnoCount.Keys.ToArray();
				model.AnosN = AnoCount.Values.ToArray();

				model.Intituicao = InstituicaoCount.Keys.ToArray();
				model.IntituicaoN = InstituicaoCount.Values.ToArray();


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
            model.Nalunos = responseData["nalunos"];
            model.NTrabalhos = responseData["nTrabalhos"];
            model.Nmenbros = responseData["nmenbros"];
            model.Nempresas = responseData["nempresas"];



            message = await _InternalClient.GetAsync(_APIserver + "/Trabalhos/Index");
			body = await message.Content.ReadAsStringAsync();
			Trabalhos_list = JsonConvert.DeserializeObject<List<Trabalhos>>(body);

			if (Trabalhos_list != null && Trabalhos_list.Count > 0)
			{

				// Count the number of Trabalhos for each Tipo
				Dictionary<string, int> tipoCount = Trabalhos_list
					.GroupBy(t => t.Tipo)
					.ToDictionary(g => g.Key, g => g.Count());

				// Assign the dictionary values to the model properties
				model.Tipos = tipoCount.Keys.ToArray();
				model.TipoN = tipoCount.Values.ToArray();


				
                // Calculate the mean Nota per Ano Letivo
                Dictionary<int, double> meanNotaPerAno = Trabalhos_list
                    .GroupBy(t => t.Ano_Letivo)
                    .ToDictionary(g => g.Key, g => CalculateMeanNota(g));

                model.meanNotaPerAno = meanNotaPerAno.Values.ToArray();
                model.meanNotaAnos = meanNotaPerAno.Keys.ToArray();

                // Calculate  the percentage of defended trabalhos 
                Dictionary<int, double> percentage = Trabalhos_list
				 .GroupBy(t => t.Ano_Letivo)
				 .ToDictionary(g => g.Key, g => CalculatePDefendidos(g));

				model.Pdefendidos = percentage.Values.ToArray();


                // Calculate the number of defended trabalhos 
                Dictionary<int, int> tdefendidos = Trabalhos_list
                 .GroupBy(t => t.Ano_Letivo)
                 .ToDictionary(g => g.Key, g => CalcutateNDefendidos(g));

                model.Ndefendidos = tdefendidos.Values.ToArray();
            }
            else
			{
				model.Trabalhos = new List<Trabalhos>();
				model.Tipos = new string[0];
				model.TipoN = new int[0];
			}

			return View(model);

		}

        private double CalculateMeanNota(IEnumerable<Trabalhos> trabalhos)
        {
            List<int> notas = trabalhos
                .Where(t => !string.IsNullOrEmpty(t.Nota))
                .Select(t => int.Parse(t.Nota))
                .ToList();

            return notas.Any() ? notas.Average() : 0;
        }

        private double CalculatePDefendidos(IEnumerable<Trabalhos> trabalhos)
        {
				double defendidos = trabalhos
                .Where(t => !string.IsNullOrEmpty(t.Nota))
                .Count();

				double total = trabalhos.Count();	
				
				double percentage = (defendidos * 100) / total;

				return percentage;
        }

        private int CalcutateNDefendidos(IEnumerable<Trabalhos> trabalhos)
        {
            int defendidos = trabalhos
            .Where(t => !string.IsNullOrEmpty(t.Nota))
            .Count();


            return defendidos;
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