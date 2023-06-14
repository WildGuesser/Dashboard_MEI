using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FrontEnd.Data;
using FrontEnd.Models;
using Microsoft.Extensions.Logging;
using System.Configuration;
using Newtonsoft.Json;
using System.Text;
using FrontEnd.Data.Paging_Models;
using CsvHelper;
using CsvHelper.Configuration;
using System.Globalization;
using System.ComponentModel.DataAnnotations;

namespace FrontEnd.Controllers
{
    public class AlunosController : Controller
    {

        private readonly ILogger<AlunosController> _logger;
        private readonly string _APIserver;
        private readonly HttpClient _InternalClient;
        private List<Alunos> list;
        private PagingModel pageEmpresas;

        public AlunosController(ILogger<AlunosController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _APIserver = configuration.GetSection("WebAPIServers").GetSection("DashboardAPI").Value;
            _InternalClient = new HttpClient();
        }

        // GET: Alunos
        public async Task<IActionResult> Index(
            [FromQuery] int p = 1,
            [FromQuery] int s = 10)
        {
            try
            {
                HttpResponseMessage message = await _InternalClient.GetAsync(_APIserver + "/Alunos/Index");

                string body = await message.Content.ReadAsStringAsync();

                list = JsonConvert.DeserializeObject<List<Alunos>>(body);

                if (list == null)
                {
                    list = new List<Alunos>();
                }
                
                AlunosPagingModel model = new()
                {
                    P = p,
                    S = s
                };

                //count records that returns after the search
                model.TotalRecords = list.Count;

                model.AlunosList = list
                                        .Skip((p - 1) * s)
                                        .Take(s)
                                        .ToList();




                return View(model);
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, "Error fetching data from API");

                // handle the exception by returning the Alunos Index view without making the API call
                AlunosPagingModel model = new()
                {
                    P = p,
                    S = s
                };
                return View(model);
            }
        }

        // GET: Alunos/Create
        public IActionResult Create_Aluno()
        {
            return View();
        }

        // POST: Alunos/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create_Aluno(Alunos alunos)
        {

                try
                {
                    // Serialize the Alunos object to JSON
                    string json = JsonConvert.SerializeObject(alunos);

                    // Send a POST request to the API to create a new Alunos object
                    var response = await _InternalClient.PostAsync(_APIserver + "/Alunos/Create",
                        new StringContent(json, Encoding.UTF8, "application/json"));

                    response.EnsureSuccessStatusCode();

                    // Redirect to the Index action
                    return RedirectToAction(nameof(Index));
                }
                catch (HttpRequestException ex)
                {
                    _logger.LogError(ex, "Error creating new Alunos object");
                    ViewData["ErrorMessage"] += ex.Message;

                    // Handle the exception by returning the Alunos Create view with the current object
                    return View(alunos);
                }
     

            return View(alunos);
        }

        // GET: Alunos/Edit/5
        public async Task<IActionResult> Edit_Aluno(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            try
            {
                // Send a GET request to the API to retrieve the Alunos object with the specified ID
                HttpResponseMessage message = await _InternalClient.GetAsync(_APIserver + $"/Alunos/{id}");

                string body = await message.Content.ReadAsStringAsync();

                Alunos alunos = JsonConvert.DeserializeObject<Alunos>(body);

                if (alunos == null)
                {
                    return NotFound();
                }

                return PartialView("Edit_Aluno", alunos);
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, "Error fetching data from API");

                // Handle the exception by returning the Alunos Index view
                return RedirectToAction(nameof(Index));
            }
        }

        // POST: Alunos/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit_Aluno(int id, Alunos alunos)
        {
            if (id != alunos.Id)
            {
                return NotFound();
            }

            try
            {
                // Serialize the Alunos object to JSON
                string json = JsonConvert.SerializeObject(alunos);

                // Send a PUT request to the API to update the Alunos object with the specified ID
                var response = await _InternalClient.PutAsync(_APIserver + $"/Alunos/{id}",
                    new StringContent(json, Encoding.UTF8, "application/json"));

                response.EnsureSuccessStatusCode();

                // Redirect to the Index action
                return RedirectToAction(nameof(Index));
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, "Error updating Alunos object");

                // Handle the exception by returning the Alunos Edit view with the current object
                return View(alunos);
            }
            

            return View(alunos);
        }


        // GET: Alunos/Delete/5
        public async Task<IActionResult> Delete_Aluno(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            try
            {
                // Send a GET request to the API to retrieve the Alunos object with the specified ID
                HttpResponseMessage message = await _InternalClient.GetAsync(_APIserver + $"/Alunos/{id}");

                string body = await message.Content.ReadAsStringAsync();

                Alunos alunos = JsonConvert.DeserializeObject<Alunos>(body);

                if (alunos == null)
                {
                    return NotFound();
                }

                return View(alunos);
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, "Error fetching data from API");

                // Handle the exception by returning the Alunos Index view
                return RedirectToAction(nameof(Index));
            }
        }

        // POST: Alunos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed_Aluno(int id)
        {
            try
            {
                // Send a DELETE request to the API to delete the Alunos object with the specified ID
                var response = await _InternalClient.DeleteAsync(_APIserver + $"/Alunos/{id}");

                response.EnsureSuccessStatusCode();

                // Redirect to the Index action
                return RedirectToAction(nameof(Index));
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, "Error deleting Alunos object");

                // Handle the exception by returning the Alunos Delete view with the current object
                HttpResponseMessage message = await _InternalClient.GetAsync(_APIserver + $"/Alunos/{id}");

                string body = await message.Content.ReadAsStringAsync();

                Alunos alunos = JsonConvert.DeserializeObject<Alunos>(body);

                return View(alunos);
            }
        }


        // POST: Alunos/DeleteMultiple
        [HttpPost]
        public async Task<IActionResult> DeleteMultiple(List<int> ids)
        {


                try
                {
                    // Serialize the list of IDs to JSON
                    string json = JsonConvert.SerializeObject(ids);

                    // Send a DELETE request to the API to delete the Alunos objects with the specified IDs
                    var response = await _InternalClient.PostAsync(_APIserver + "/Alunos/DeleteMultiple",
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

        // GET: Alunos/ExportCSV
        public async Task<IActionResult> ExportCSV()
        {
            try
            {
                HttpResponseMessage message = await _InternalClient.GetAsync(_APIserver + "/Alunos/Index");

                string body = await message.Content.ReadAsStringAsync();

                list = JsonConvert.DeserializeObject<List<Alunos>>(body);

                if (list == null)
                {
                    list = new List<Alunos>();
                }

                // Generate the CSV content
                var csvContent = new StringBuilder();
                csvContent.AppendLine("Numero_Aluno,Nome,Curso,Email,Contacto,Instituicao,Edicao,Estado");
                foreach (var aluno in list)
                {
                    csvContent.AppendLine($"{aluno.Numero_Aluno},{aluno.Nome},{aluno.Curso},{aluno.Email},{aluno.Contacto},{aluno.Instituicao},{aluno.Edicao},{aluno.Estado}");
                }

                // Prepare the CSV file for download
                byte[] csvBytes = Encoding.GetEncoding("iso-8859-1").GetBytes(csvContent.ToString());
                var csvFileName = "alunos.csv";

                // Save the success message in TempData
                TempData["ExportSuccessMessage"] = "Alunos data exported successfully.";
                
                return File(csvBytes, "text/csv", csvFileName);

            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, "Error fetching data from API");

                // Save the error message in TempData
                TempData["ExportErrorMessage"] = "Error exporting Alunos data.";

                // Handle the exception by returning the Alunos Index view
                return RedirectToAction(nameof(Index));
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ImportCSV(IFormFile csvFile)
        {
            if (csvFile == null || csvFile.Length == 0)
            {
                // Handle the case when no file is selected
                TempData["ExportErrorMessage"] = "No file selected.";
                return RedirectToAction(nameof(Index));
            }

            var errorLines = new List<string>(); // Store the lines that could not be stored

            try
            {
                using (var reader = new StreamReader(csvFile.OpenReadStream(), Encoding.GetEncoding("ISO-8859-1")))
                using (var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)))
                {
                    csv.Read(); // Skip the header line

                    int lineNumber = 2; // Starting from line 2 (since the header is skipped)


                    while (csv.Read())
                    {
                      
                            // Create a new Alunos object and set its properties from CSV values
                            var alunos = new Alunos
                            {
                                Numero_Aluno = csv.GetField<int>(0),
                                Nome = csv.GetField<string>(1),
                                Curso = csv.GetField<string>(2),
                                Email = csv.GetField<string>(3),
                                Contacto = csv.GetField<string>(4),
                                Instituicao = csv.GetField<string>(5),
                                Edicao = csv.GetField<int>(6),
                                Estado = bool.Parse(csv.GetField<string>(7))
                            };

                            // Validate the Alunos object
                            var validationContext = new ValidationContext(alunos, serviceProvider: null, items: null);
                            var validationResults = new List<ValidationResult>();
                            bool isValid = Validator.TryValidateObject(alunos, validationContext, validationResults, validateAllProperties: true);
                        if (isValid)
                        {
                            // Serialize the Alunos object to JSON
                            string json = JsonConvert.SerializeObject(alunos);

                            // Send a POST request to the API to create a new Alunos object
                            var response = await _InternalClient.PostAsync(_APIserver + "/Alunos/Create",
                                new StringContent(json, Encoding.UTF8, "application/json"));

                            // Check if the response is a bad request and add the line number to the error message
                            if (!response.IsSuccessStatusCode)
                            {
                                errorLines.Add($"Line {lineNumber}: {response.ReasonPhrase}");
                            }
                        }
                        else
                        {
                            // Add the line number to the error message
                            var validationErrors = string.Join(", ", validationResults.Select(r => r.ErrorMessage));
                            errorLines.Add($"Line {lineNumber}: {validationErrors}");
                        }

                        lineNumber++;
                    }
                }

                if (errorLines.Any())
                {
                    TempData["ExportErrorMessage"] = $"Error importing CSV. The following lines could not be stored:\n{string.Join("\n", errorLines)}";
                }
                else
                {
                    TempData["ExportSuccessMessage"] = "All records imported successfully.";
                }

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                TempData["ExportErrorMessage"] = $"Error importing CSV: {ex.Message}";
                return RedirectToAction(nameof(Index));
            }
        }






    }
}
