using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using API_MEI.Models;
using FrontEnd.Data;
using FrontEnd.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;


namespace FrontEnd.Controllers
{
    public class JuriController : Controller
    {

        private readonly ILogger<JuriController> _logger;
        private readonly string _APIserver;
        private readonly HttpClient _InternalClient;

        public JuriController(ILogger<JuriController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _APIserver = configuration.GetSection("WebAPIServers").GetSection("DashboardAPI").Value;
            _InternalClient = new HttpClient();
        }

        // GET: Juri
        public async Task<IActionResult> Index()
        {
            try
            {
                HttpResponseMessage message = await _InternalClient.GetAsync(_APIserver + "/Juri/Index");

                string body = await message.Content.ReadAsStringAsync();

                List<Juri> list = JsonConvert.DeserializeObject<List<Juri>>(body);

                if (list == null)
                {
                    list = new List<Juri>();
                }
                return View(list);
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, "Error fetching data from API");

                // handle the exception by returning the Juri Index view without making the API call
                List<Juri> list = new List<Juri>();
                return View(list);
            }
        }

        // GET: Juri/Create
        public IActionResult Create_Juri()
        {
            return View();
        }

        // POST: Juri/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create_Juri(Juri juri)
        {

            try
            {
                // Serialize the Juri object to JSON
                string json = JsonConvert.SerializeObject(juri);

                // Send a POST request to the API to create a new Juri object
                var response = await _InternalClient.PostAsync(_APIserver + "/Juri/Create",
                    new StringContent(json, Encoding.UTF8, "application/json"));

                response.EnsureSuccessStatusCode();

                // Redirect to the Index action
                return RedirectToAction(nameof(Index));
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, "Error creating new Juri object");

                TempData["NoSuccess"] = "An error occurred while creating the Juri object: " + ex.Message;

                // Handle the exception by returning the Juri Create view with the current object
                return View(juri);
            }
        }

        // GET: Alunos/Edit/5
        public async Task<IActionResult> Edit_Juri(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            try
            {
                // Send a GET request to the API to retrieve the Juri object with the specified ID
                HttpResponseMessage message = await _InternalClient.GetAsync(_APIserver + $"/Juri/{id}");

                string body = await message.Content.ReadAsStringAsync();

                Juri juri = JsonConvert.DeserializeObject<Juri>(body);

                if (juri == null)
                {
                    return NotFound();
                }

                return View(juri);
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, "Error fetching data from API");

                // Handle the exception by returning the Alunos Index view
                return RedirectToAction(nameof(Index));
            }
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit_Juri(int id, Juri juris)
        {
            if (id != juris.Id)
            {
                return NotFound();
            }

            try
            {
                // Serialize the Juris object to JSON
                string json = JsonConvert.SerializeObject(juris);

                // Send a PUT request to the API to update the Juris object with the specified ID
                var response = await _InternalClient.PutAsync(_APIserver + $"/Juri/{id}",
                    new StringContent(json, Encoding.UTF8, "application/json"));

                response.EnsureSuccessStatusCode();

                // Redirect to the Index action
                return RedirectToAction(nameof(Index));
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, "Error updating Juris object");

                // Handle the exception by returning the Juris Edit view with the current object
                return View(juris);
            }

        }



        // GET: Juris/Delete/5
        public async Task<IActionResult> Delete_Juri(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            try
            {
                // Send a GET request to the API to retrieve the Juris object with the specified ID
                HttpResponseMessage message = await _InternalClient.GetAsync(_APIserver + $"/Juri/{id}");

                string body = await message.Content.ReadAsStringAsync();

                Juri juris = JsonConvert.DeserializeObject<Juri>(body);

                if (juris == null)
                {
                    return NotFound();
                }

                return View(juris);
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, "Error fetching data from API");

                // Handle the exception by returning the Juris Index view
                return RedirectToAction(nameof(Index));
            }
        }

        // POST: Juris/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed_Juri(int id)
        {
            try
            {
                // Send a DELETE request to the API to delete the Juris object with the specified ID
                var response = await _InternalClient.DeleteAsync(_APIserver + $"/Juri/{id}");

                response.EnsureSuccessStatusCode();

                // Redirect to the Index action
                return RedirectToAction(nameof(Index));
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, "Error deleting Juri object");

                // Handle the exception by returning the Juris Delete view with the current object
                HttpResponseMessage message = await _InternalClient.GetAsync(_APIserver + $"/Juri/{id}");

                string body = await message.Content.ReadAsStringAsync();

                Juri juris = JsonConvert.DeserializeObject<Juri>(body);

                return View(juris);
            }
        }


        // POST: Juris/DeleteMultiple
        [HttpPost]
        public async Task<IActionResult> DeleteMultiple(List<int> ids)
        {


            try
            {
                // Serialize the list of IDs to JSON
                string json = JsonConvert.SerializeObject(ids);

                // Send a DELETE request to the API to delete the Juris objects with the specified IDs
                var response = await _InternalClient.PostAsync(_APIserver + "/Juri/DeleteMultiple",
                    new StringContent(json, Encoding.UTF8, "application/json"));

                response.EnsureSuccessStatusCode();

                // Redirect to the Index action
                return RedirectToAction(nameof(Index));
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, "Error deleting Juris objects");
                ViewData["ErrorMessage"] += ex.Message;

                // Handle the exception by returning the Juris Index view
                return RedirectToAction(nameof(Index));
            }

        }
    }
}