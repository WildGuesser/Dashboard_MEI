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
    public class EmpresasController : Controller
    {

        private readonly ILogger<EmpresasController> _logger;
        private readonly string _APIserver;
        private readonly HttpClient _InternalClient;
        private List<Empresas> list;
        private PagingModel pageEmpresas;


        public EmpresasController(ILogger<EmpresasController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _APIserver = configuration.GetSection("WebAPIServers").GetSection("DashboardAPI").Value;
            _InternalClient = new HttpClient();
        }

        // GET: Empresas
        public async Task<IActionResult> Index(
            [FromQuery] int p = 1,
            [FromQuery] int s = 10 )
        {
            try
            {
                HttpResponseMessage message = await _InternalClient.GetAsync(_APIserver + "/Empresas/Index");

                string body = await message.Content.ReadAsStringAsync();

                list = JsonConvert.DeserializeObject<List<Empresas>>(body);

                if (list == null)
                {
                    list = new List<Empresas>();
                }
                
                EmpresaPagingModel model = new()
                {
                    P = p,
                    S = s
                };

                //count records that returns after the search
                model.TotalRecords = list.Count;

                model.EmpresaList =  list
                                        .Skip((p - 1) * s)
                                        .Take(s)
                                        .ToList();

                return View(model);
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, "Error fetching data from API");

                // handle the exception by returning the Empresas Index view without making the API call
                EmpresaPagingModel model = new()
                {
                    P = p,
                    S = s
                };
                return View(model);
            }
        }

        // GET: Empresas/Create
        public IActionResult Create_Empresa()
        {
            return View();
        }

        // POST: Empresas/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create_Empresa(Empresas empresas)
        {

            try
            {
                // Serialize the Empresas object to JSON
                string json = JsonConvert.SerializeObject(empresas);

                // Send a POST request to the API to create a new Empresas object
                var response = await _InternalClient.PostAsync(_APIserver + "/Empresas/Create",
                    new StringContent(json, Encoding.UTF8, "application/json"));

                response.EnsureSuccessStatusCode();

                // Redirect to the Index action
                return RedirectToAction(nameof(Index));
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, "Error creating new Empresas object");
                ViewData["ErrorMessage"] += ex.Message;

                // Handle the exception by returning the Empresas Create view with the current object
                return View(empresas);
            }

        }

        // GET: Empresas/Edit/5
        public async Task<IActionResult> Edit_Empresa(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            try
            {
                // Send a GET request to the API to retrieve the Empresas object with the specified ID
                HttpResponseMessage message = await _InternalClient.GetAsync(_APIserver + $"/Empresas/{id}");

                string body = await message.Content.ReadAsStringAsync();

                Empresas empresas = JsonConvert.DeserializeObject<Empresas>(body);

                if (empresas == null)
                {
                    return NotFound();
                }

                return PartialView("Edit_Empresa", empresas);
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, "Error fetching data from API");

                // Handle the exception by returning the Empresas Index view
                return RedirectToAction(nameof(Index));
            }
        }

        // POST: Empresas/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit_Empresa(int id, Empresas empresas)
        {
            if (id != empresas.Id)
            {
                return NotFound();
            }

            try
            {
                // Serialize the Empresas object to JSON
                string json = JsonConvert.SerializeObject(empresas);

                // Send a PUT request to the API to update the Empresas object with the specified ID
                var response = await _InternalClient.PutAsync(_APIserver + $"/Empresas/{id}",
                    new StringContent(json, Encoding.UTF8, "application/json"));

                response.EnsureSuccessStatusCode();

                // Redirect to the Index action
                return RedirectToAction(nameof(Index));
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, "Error updating Empresas object");

                // Handle the exception by returning the Empresas Edit view with the current object
                return View(empresas);
            }

        }


        // GET: Empresas/Delete/5
        public async Task<IActionResult> Delete_Empresa(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            try
            {
                // Send a GET request to the API to retrieve the Empresas object with the specified ID
                HttpResponseMessage message = await _InternalClient.GetAsync(_APIserver + $"/Empresas/{id}");

                string body = await message.Content.ReadAsStringAsync();

                Empresas empresas = JsonConvert.DeserializeObject<Empresas>(body);

                if (empresas == null)
                {
                    return NotFound();
                }

                return View(empresas);
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, "Error fetching data from API");

                // Handle the exception by returning the Empresas Index view
                return RedirectToAction(nameof(Index));
            }
        }

        // POST: Empresas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed_Empresa(int id)
        {
            try
            {
                // Send a DELETE request to the API to delete the Empresas object with the specified ID
                var response = await _InternalClient.DeleteAsync(_APIserver + $"/Empresas/{id}");

                response.EnsureSuccessStatusCode();

                // Redirect to the Index action
                return RedirectToAction(nameof(Index));
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, "Error deleting Empresas object");

                // Handle the exception by returning the Empresas Delete view with the current object
                HttpResponseMessage message = await _InternalClient.GetAsync(_APIserver + $"/Empresas/{id}");

                string body = await message.Content.ReadAsStringAsync();

                Empresas empresas = JsonConvert.DeserializeObject<Empresas>(body);

                return View(empresas);
            }
        }


        // POST: Empresas/DeleteMultiple
        [HttpPost]
        public async Task<IActionResult> DeleteMultiple(List<int> ids)
        {


            try
            {
                // Serialize the list of IDs to JSON
                string json = JsonConvert.SerializeObject(ids);

                // Send a DELETE request to the API to delete the Empresas objects with the specified IDs
                var response = await _InternalClient.PostAsync(_APIserver + "/Empresas/DeleteMultiple",
                    new StringContent(json, Encoding.UTF8, "application/json"));

                response.EnsureSuccessStatusCode();

                // Redirect to the Index action
                return RedirectToAction(nameof(Index));
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, "Error deleting Empresas objects");
                ViewData["ErrorMessage"] += ex.Message;

                // Handle the exception by returning the Empresas Index view
                return RedirectToAction(nameof(Index));
            }

        }

        public async Task<IActionResult> Info_Membro(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            try
            {
                // Send a GET request to the API to retrieve the Trabalhos object with the specified ID
                HttpResponseMessage message = await _InternalClient.GetAsync(_APIserver + $"/Trabalhos/Membros/{id}");

                string body = await message.Content.ReadAsStringAsync();

                Membros especialista = JsonConvert.DeserializeObject<Membros>(body);

                if (especialista == null)
                {
                    return NotFound();
                }

                return PartialView("Info_Especialista", especialista);
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, "Error fetching data from API");

                // Handle the exception by returning the Trabalhos Index view
                return RedirectToAction(nameof(Index));
            }
        }

    }
}
