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
    public class DocentesController : Controller
    {

        private readonly ILogger<DocentesController> _logger;
        private readonly string _APIserver;
        private readonly HttpClient _InternalClient;
        private List<Docentes> list;


        public DocentesController(ILogger<DocentesController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _APIserver = configuration.GetSection("WebAPIServers").GetSection("DashboardAPI").Value;
            _InternalClient = new HttpClient();
        }

        // GET: Docentes
        public async Task<IActionResult> Index(
            [FromQuery] int p = 1,
            [FromQuery] int s = 10)
        {
            try
            {
                HttpResponseMessage message = await _InternalClient.GetAsync(_APIserver + "/Docentes/Index");

                string body = await message.Content.ReadAsStringAsync();

                list = JsonConvert.DeserializeObject<List<Docentes>>(body);

                if (list == null)
                {
                    list = new List<Docentes>();
                }
                DocentesPagingModel model = new()
                {
                    P = p,
                    S = s
                };

                //count records that returns after the search
                model.TotalRecords = list.Count;

                model.DocentesList = list
                                        .Skip((p - 1) * s)
                                        .Take(s)
                                        .ToList();

                return View(model);
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, "Error fetching data from API");

                // handle the exception by returning the Docentes Index view without making the API call
                DocentesPagingModel model = new()
                {
                    P = p,
                    S = s
                };

                return View(model);
            }
        }

        // GET: Docentes/Create
        public IActionResult Create_Docente()
        {
            return View();
        }

        // POST: Docentes/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create_Docente(Docentes docentes)
        {

            try
            {
                // Serialize the Docentes object to JSON
                string json = JsonConvert.SerializeObject(docentes);

                // Send a POST request to the API to create a new Docentes object
                var response = await _InternalClient.PostAsync(_APIserver + "/Docentes/Create",
                    new StringContent(json, Encoding.UTF8, "application/json"));

                response.EnsureSuccessStatusCode();

                // Redirect to the Index action
                return RedirectToAction(nameof(Index));
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, "Error creating new Docentes object");
                ViewData["ErrorMessage"] += ex.Message;

                // Handle the exception by returning the Docentes Create view with the current object
                return View(docentes);
            }


            return View(docentes);
        }

        // GET: Docentes/Edit/5
        public async Task<IActionResult> Edit_Docente(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            try
            {
                // Send a GET request to the API to retrieve the Docentes object with the specified ID
                HttpResponseMessage message = await _InternalClient.GetAsync(_APIserver + $"/Docentes/{id}");

                string body = await message.Content.ReadAsStringAsync();

                Docentes docentes = JsonConvert.DeserializeObject<Docentes>(body);

                if (docentes == null)
                {
                    return NotFound();
                }

                return PartialView("Edit_Docente",docentes);
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, "Error fetching data from API");

                // Handle the exception by returning the Docentes Index view
                return RedirectToAction(nameof(Index));
            }
        }

        // POST: Docentes/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit_Docente(int id, Docentes docentes)
        {
            if (id != docentes.Id)
            {
                return NotFound();
            }

            try
            {
                // Serialize the Docentes object to JSON
                string json = JsonConvert.SerializeObject(docentes);

                // Send a PUT request to the API to update the Docentes object with the specified ID
                var response = await _InternalClient.PutAsync(_APIserver + $"/Docentes/{id}",
                    new StringContent(json, Encoding.UTF8, "application/json"));

                response.EnsureSuccessStatusCode();

                // Redirect to the Index action
                return RedirectToAction(nameof(Index));
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, "Error updating Docentes object");

                // Handle the exception by returning the Docentes Edit view with the current object
                return View(docentes);
            }


            return View(docentes);
        }


        // GET: Docentes/Delete/5
        public async Task<IActionResult> Delete_Docente(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            try
            {
                // Send a GET request to the API to retrieve the Docentes object with the specified ID
                HttpResponseMessage message = await _InternalClient.GetAsync(_APIserver + $"/Docentes/{id}");

                string body = await message.Content.ReadAsStringAsync();

                Docentes docentes = JsonConvert.DeserializeObject<Docentes>(body);

                if (docentes == null)
                {
                    return NotFound();
                }

                return View(docentes);
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, "Error fetching data from API");

                // Handle the exception by returning the Docentes Index view
                return RedirectToAction(nameof(Index));
            }
        }

        // POST: Docentes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed_Docente(int id)
        {
            try
            {
                // Send a DELETE request to the API to delete the Docentes object with the specified ID
                var response = await _InternalClient.DeleteAsync(_APIserver + $"/Docentes/{id}");

                response.EnsureSuccessStatusCode();

                // Redirect to the Index action
                return RedirectToAction(nameof(Index));
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, "Error deleting Docentes object");

                // Handle the exception by returning the Docentes Delete view with the current object
                HttpResponseMessage message = await _InternalClient.GetAsync(_APIserver + $"/Docentes/{id}");

                string body = await message.Content.ReadAsStringAsync();

                Docentes docentes = JsonConvert.DeserializeObject<Docentes>(body);

                return View(docentes);
            }
        }


        // POST: Docentes/DeleteMultiple
        [HttpPost]
        public async Task<IActionResult> DeleteMultiple(List<int> ids)
        {


            try
            {
                // Serialize the list of IDs to JSON
                string json = JsonConvert.SerializeObject(ids);

                // Send a DELETE request to the API to delete the Docentes objects with the specified IDs
                var response = await _InternalClient.PostAsync(_APIserver + "/Docentes/DeleteMultiple",
                    new StringContent(json, Encoding.UTF8, "application/json"));

                response.EnsureSuccessStatusCode();

                // Redirect to the Index action
                return RedirectToAction(nameof(Index));
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, "Error deleting Docentes objects");
                ViewData["ErrorMessage"] += ex.Message;

                // Handle the exception by returning the Docentes Index view
                return RedirectToAction(nameof(Index));
            }

        }

    }
}
