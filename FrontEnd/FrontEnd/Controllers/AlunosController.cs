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

    }
}
