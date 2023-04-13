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
using API_MEI.Models;

namespace FrontEnd.Controllers
{
    public class EspecialistasController : Controller
    {

        private readonly ILogger<EspecialistasController> _logger;
        private readonly string _APIserver;
        private readonly HttpClient _InternalClient;
        private List<Especialistas> list;


        public EspecialistasController(ILogger<EspecialistasController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _APIserver = configuration.GetSection("WebAPIServers").GetSection("DashboardAPI").Value;
            _InternalClient = new HttpClient();
        }

        // GET: Especialistas
        public async Task<IActionResult> Index()
        {
            try
            {
                HttpResponseMessage message = await _InternalClient.GetAsync(_APIserver + "/Especialistas/Index");

                string body = await message.Content.ReadAsStringAsync();

                list = JsonConvert.DeserializeObject<List<Especialistas>>(body);

                if (list == null)
                {
                    list = new List<Especialistas>();
                }
                return View(list);
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, "Error fetching data from API");

                // handle the exception by returning the Especialistas Index view without making the API call
                List<Especialistas> list = new List<Especialistas>();
                return View(list);
            }
        }

        // GET: Especialistas/Create
        public async Task<IActionResult> Create_Especialista()
        {
            HttpResponseMessage message = await _InternalClient.GetAsync(_APIserver + "/Empresas");
            string body = await message.Content.ReadAsStringAsync();

            ViewBag.Empresa_ID = new SelectList(
                Newtonsoft.Json.JsonConvert.DeserializeObject<List<Empresas>>(body),
                "Id",
                "Nome");

            return View();
        }


        // POST: Especialistas/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create_Especialista(Especialistas especialistas)
        {

            try
            {
                // Serialize the Especialistas object to JSON
                string json = JsonConvert.SerializeObject(especialistas);

                // Send a POST request to the API to create a new Especialistas object
                var response = await _InternalClient.PostAsync(_APIserver + "/Especialistas/Create",
                    new StringContent(json, Encoding.UTF8, "application/json"));

                response.EnsureSuccessStatusCode();

                // Redirect to the Index action
                return RedirectToAction(nameof(Index));
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, "Error creating new Especialistas object");
                ViewData["ErrorMessage"] += ex.Message;

                // Handle the exception by returning the Especialistas Create view with the current object
                return View(especialistas);
            }


            return View(especialistas);
        }

        // GET: Especialistas/Edit/5
        public async Task<IActionResult> Edit_Especialista(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            try
            {
                // Send a GET request to the API to retrieve the Especialistas object with the specified ID
                HttpResponseMessage message = await _InternalClient.GetAsync(_APIserver + $"/Especialistas/{id}");

                string body = await message.Content.ReadAsStringAsync();

                Especialistas especialistas = JsonConvert.DeserializeObject<Especialistas>(body);

                if (especialistas == null)
                {
                    return NotFound();
                }

                return View(especialistas);
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, "Error fetching data from API");

                // Handle the exception by returning the Especialistas Index view
                return RedirectToAction(nameof(Index));
            }
        }

        // POST: Especialistas/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit_Especialista(int id, Especialistas especialistas)
        {
            if (id != especialistas.Id)
            {
                return NotFound();
            }

            try
            {
                // Serialize the Especialistas object to JSON
                string json = JsonConvert.SerializeObject(especialistas);

                // Send a PUT request to the API to update the Especialistas object with the specified ID
                var response = await _InternalClient.PutAsync(_APIserver + $"/Especialistas/{id}",
                    new StringContent(json, Encoding.UTF8, "application/json"));

                response.EnsureSuccessStatusCode();

                // Redirect to the Index action
                return RedirectToAction(nameof(Index));
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, "Error updating Especialistas object");

                // Handle the exception by returning the Especialistas Edit view with the current object
                return View(especialistas);
            }


            return View(especialistas);
        }


        // GET: Especialistas/Delete/5
        public async Task<IActionResult> Delete_Especialista(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            try
            {
                // Send a GET request to the API to retrieve the Especialistas object with the specified ID
                HttpResponseMessage message = await _InternalClient.GetAsync(_APIserver + $"/Especialistas/{id}");

                string body = await message.Content.ReadAsStringAsync();

                Especialistas especialistas = JsonConvert.DeserializeObject<Especialistas>(body);

                if (especialistas == null)
                {
                    return NotFound();
                }

                return View(especialistas);
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, "Error fetching data from API");

                // Handle the exception by returning the Especialistas Index view
                return RedirectToAction(nameof(Index));
            }
        }

        // POST: Especialistas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed_Especialista(int id)
        {
            try
            {
                // Send a DELETE request to the API to delete the Especialistas object with the specified ID
                var response = await _InternalClient.DeleteAsync(_APIserver + $"/Especialistas/{id}");

                response.EnsureSuccessStatusCode();

                // Redirect to the Index action
                return RedirectToAction(nameof(Index));
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, "Error deleting Especialistas object");

                // Handle the exception by returning the Especialistas Delete view with the current object
                HttpResponseMessage message = await _InternalClient.GetAsync(_APIserver + $"/Especialistas/{id}");

                string body = await message.Content.ReadAsStringAsync();

                Especialistas especialistas = JsonConvert.DeserializeObject<Especialistas>(body);

                return View(especialistas);
            }
        }


        // POST: Especialistas/DeleteMultiple
        [HttpPost]
        public async Task<IActionResult> DeleteMultiple(List<int> ids)
        {


            try
            {
                // Serialize the list of IDs to JSON
                string json = JsonConvert.SerializeObject(ids);

                // Send a DELETE request to the API to delete the Especialistas objects with the specified IDs
                var response = await _InternalClient.PostAsync(_APIserver + "/Especialistas/DeleteMultiple",
                    new StringContent(json, Encoding.UTF8, "application/json"));

                response.EnsureSuccessStatusCode();

                // Redirect to the Index action
                return RedirectToAction(nameof(Index));
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, "Error deleting Especialistas objects");
                ViewData["ErrorMessage"] += ex.Message;

                // Handle the exception by returning the Especialistas Index view
                return RedirectToAction(nameof(Index));
            }

        }

    }
}
