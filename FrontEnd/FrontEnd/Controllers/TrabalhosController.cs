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
using Newtonsoft.Json.Linq;
using FrontEnd.Models.ViewModel;
using System.Diagnostics.Metrics;

namespace FrontEnd.Controllers
{
    public class TrabalhosController : Controller
    {

        private readonly ILogger<TrabalhosController> _logger;
        private readonly string _APIserver;
        private readonly HttpClient _InternalClient;
        private List<Trabalhos> list;

        private List<Alunos> Alunos_list;
        private List<Empresas> Empresas_list;
        private List<Membros> Membros_list;

        private PagingModel pageEmpresas;

        public TrabalhosController(ILogger<TrabalhosController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _APIserver = configuration.GetSection("WebAPIServers").GetSection("DashboardAPI").Value;
            _InternalClient = new HttpClient();
        }

        // GET: Trabalhos
        public async Task<IActionResult> Index(
            [FromQuery] int p = 1,
            [FromQuery] int s = 10)
        {
            try
            {
                HttpResponseMessage message = await _InternalClient.GetAsync(_APIserver + "/Trabalhos/Index");

                string body = await message.Content.ReadAsStringAsync();

                list = JsonConvert.DeserializeObject<List<Trabalhos>>(body);

                if (list == null)
                {
                    list = new List<Trabalhos>();
                }

                TrabalhosPagingModel model = new()
                {
                    P = p,
                    S = s
                };

                //count records that returns after the search
                model.TotalRecords = list.Count;

                model.TrabalhosList = list
                                        .Skip((p - 1) * s)
                                        .Take(s)
                                        .ToList();

                return View(model);
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, "Error fetching data from API");

                // handle the exception by returning the Trabalhos Index view without making the API call
                TrabalhosPagingModel model = new()
                {
                    P = p,
                    S = s
                };
                return View(model);
            }
        }

        // GET: Trabalhos/Create
        public IActionResult Create_Trabalho()
        {
            return View();
        }

        // POST: Trabalhos/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create_Trabalho(TrabalhosViewModel input)
        {
            HttpResponseMessage response = null;
            string apiResponse = null;


            bool hasErrors = false;

            if (input.Aluno_Id == 0)
            {
                TempData["Aluno_IdError"] = "Por favor, forneça um Aluno.";
                hasErrors = true;
            }

            if (input.Orientador_1.Id == 0)
            {
                TempData["Orientador_1_IdError"] = "Por favor, forneça um Orientador 1.";
                hasErrors = true;
            }

            if (input.Orientador_2.Id == 0)
            {
                TempData["Orientador_2_IdError"] = "Por favor, forneça um Orientador 2.";
                hasErrors = true;
            }

            if (input.Data_Defesa != null)
            {
                if (input.Presidente.Id == 0)
                {
                    TempData["Presidente_IdError"] = "Por favor, forneça um Presidente.";
                    hasErrors = true;
                }

                if (input.Arguente_1.Id == 0)
                {
                    TempData["Arguente_1_IdError"] = "Por favor, forneça um Arguente 1.";
                    hasErrors = true;
                }

                if (input.Vogal.Id == 0)
                {
                    TempData["Vogal_IdError"] = "Por favor, forneça um Vogal.";
                    hasErrors = true;
                }
            }

            if (hasErrors)
            {
                return View(input);
            }




            try
            {
                Trabalhos trabalho = new Trabalhos()
                {
                    Titulo=input.Titulo,
                    ReferenciaInfo = input.ReferenciaInfo,  
                    Tipo= input.Tipo,
                    AdendaProtocolo = input.AdendaProtocolo,
                    Aluno_Id=input.Aluno_Id,
                    Nota = input.Nota,
                    Empresa_Id=input.Empresa_Id,   
                    Ano_Letivo = input.Ano_Letivo

                };

                // Serialize the Trabalhos object to JSON
                string json = JsonConvert.SerializeObject(trabalho);

                // Send a POST request to the API to create a new Trabalhos object
                response = await _InternalClient.PostAsync(_APIserver + "/Trabalhos/Create",
                    new StringContent(json, Encoding.UTF8, "application/json"));

                response.EnsureSuccessStatusCode();

                // Deserialize the JSON response to get the created object's ID
                apiResponse = await response.Content.ReadAsStringAsync();

                // Use JObject to parse the JSON response
                var createdTrabalho = JObject.Parse(apiResponse);

                // Get the Id value as an integer
                int createdTrabalhoId = createdTrabalho.Value<int>("id");

                if (input.Data_Defesa != null)
                {

                    List<JuriMembros> juriMembrosList = new List<JuriMembros>();

                    Dictionary<string, Membros> roleToMembro = new Dictionary<string, Membros>();

                    if (input.Presidente != null)
                    {
                        roleToMembro.Add("Presidente", input.Presidente);
                    }

                    if (input.Arguente_1 != null)
                    {
                        roleToMembro.Add("Arguente 1", input.Arguente_1);
                    }

                    if (input.Arguente_2 != null)
                    {
                        roleToMembro.Add("Arguente 2", input.Arguente_2);
                    }

                    if (input.Vogal != null)
                    {
                        roleToMembro.Add("Vogal", input.Vogal);
                    }

                    Juri juri = new Juri()
                    {
                        Data_Defesa = input.Data_Defesa,
                        Trabalho_Id = createdTrabalhoId,

                    };

                    // Serialize the Trabalhos object to JSON
                    string Jurijson = JsonConvert.SerializeObject(juri);

                    // Send a POST request to the API to create a new Trabalhos object
                    response = await _InternalClient.PostAsync(_APIserver + "/Juri/Create",
                        new StringContent(Jurijson, Encoding.UTF8, "application/json"));

                    response.EnsureSuccessStatusCode();

                    apiResponse = await response.Content.ReadAsStringAsync();
                    // Use JObject to parse the JSON response
                    var createdJuri = JObject.Parse(apiResponse);
                    // Get the Id value as an integer
                    int createdJuriId = createdJuri.Value<int>("id");



                    foreach (var pair in roleToMembro)
                    {
                        JuriMembros juriMembros = new JuriMembros()
                        {
                            Juri_Id = createdJuriId,
                            Membro_Id = pair.Value.Id,
                            Funcao = pair.Key

                        };
                        // Serialize the Trabalhos object to JSON
                        string JuriMembrosjson = JsonConvert.SerializeObject(juriMembros);

                        // Send a POST request to the API to create a new Trabalhos object
                        response = await _InternalClient.PostAsync(_APIserver + "/JuriMembros/Create",
                            new StringContent(JuriMembrosjson, Encoding.UTF8, "application/json"));

                        response.EnsureSuccessStatusCode();

                    }



                }

                //Orientadores Input

                Orientadores orientador_1 = new Orientadores()
                {
                    Trabalho_Id = createdTrabalhoId,
                    Membro_Id = input.Orientador_1.Id,
                    Funcao = "Orientador 1"
                };

                Orientadores orientador_2 = new Orientadores()
                {
                    Trabalho_Id = createdTrabalhoId,
                    Membro_Id = input.Orientador_2.Id,
                    Funcao = "Orientador 2"
                };

                // Serialize the Trabalhos object to JSON
                string orientador1json = JsonConvert.SerializeObject(orientador_1);

                // Send a POST request to the API to create a new Trabalhos object
                response = await _InternalClient.PostAsync(_APIserver + "/Orientadores/Create",
                    new StringContent(orientador1json, Encoding.UTF8, "application/json"));

                response.EnsureSuccessStatusCode();

                // Serialize the Trabalhos object to JSON
                string orientador2json = JsonConvert.SerializeObject(orientador_2);

                // Send a POST request to the API to create a new Trabalhos object
                response = await _InternalClient.PostAsync(_APIserver + "/Orientadores/Create",
                    new StringContent(orientador2json, Encoding.UTF8, "application/json"));

                response.EnsureSuccessStatusCode();
              
                

                // Redirect to the Index action
                return RedirectToAction(nameof(Index));
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, $"Error creating new Trabalhos object: {ex.Message}");

                if (response != null)
                {
                    apiResponse = await response.Content.ReadAsStringAsync();
                    ViewData["ErrorMessage"] += $"API Response: {apiResponse}";
                }
                else
                {
                    ViewData["ErrorMessage"] += "Unable to retrieve API response.";
                }

                // Log the serialized JSON data for debugging purposes
                _logger.LogError($"Serialized JSON data: {JsonConvert.SerializeObject(input)}");

                return View(input);
            }
        }


        // GET: Trabalhos/Edit/5
        public async Task<IActionResult> Edit_Trabalho(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            try
            {
                // Send a GET request to the API to retrieve the Trabalhos object with the specified ID
                HttpResponseMessage message = await _InternalClient.GetAsync(_APIserver + $"/Trabalhos/{id}");

                string body = await message.Content.ReadAsStringAsync();

                Trabalhos trabalhos = JsonConvert.DeserializeObject<Trabalhos>(body);

                if (trabalhos == null)
                {
                    return NotFound();
                }

                TrabalhosViewModel trabalhosVM = new TrabalhosViewModel()
                {
                    Id = trabalhos.Id,
                    Titulo = trabalhos.Titulo,
                    ReferenciaInfo = trabalhos.ReferenciaInfo,
                    Tipo = trabalhos.Tipo,
                    Ano_Letivo = trabalhos.Ano_Letivo,
                    AdendaProtocolo = trabalhos.AdendaProtocolo,
                    Aluno_Id = trabalhos.Aluno_Id,
                    Nota = trabalhos.Nota,
                    Alunos = trabalhos.Alunos,
                    Empresa_Id = trabalhos.Empresa_Id,
                    Empresas = trabalhos.Empresas,
                    Data_Defesa = (DateTime)trabalhos.Juri.Data_Defesa

                };

                trabalhosVM.Juri.Id = trabalhos.Juri.Id;

                foreach (var trabalho in trabalhos.Juri.JuriMembros)
                {
                    if (trabalho.Funcao == "Presidente")
                    {
                        trabalhosVM.Presidente = (trabalho.Membros);
                        trabalhosVM.Presidente.OldId = (trabalho.Membros.Id);
                    }
                    else if (trabalho.Funcao == "Arguente 1")
                    {
                        trabalhosVM.Arguente_1 = (trabalho.Membros);
                        trabalhosVM.Arguente_1.OldId = (trabalho.Membros.Id);
                    }
                    else if (trabalho.Funcao == "Arguente 2")
                    {
                        trabalhosVM.Arguente_2 = (trabalho.Membros);
                        trabalhosVM.Arguente_2.OldId = (trabalho.Membros.Id);
                    }
                    else if (trabalho.Funcao == "Vogal")
                    {
                        trabalhosVM.Vogal = (trabalho.Membros);
                        trabalhosVM.Vogal.OldId = (trabalho.Membros.Id);
                    }
                }


                {

                    foreach (var trabalho in trabalhos.Orientadores)
                    {
                        if (trabalho.Funcao == "Orientador 1")
                        { 
                            trabalhosVM.Orientador_1 = (trabalho.Membros);
                            trabalhosVM.Orientador_1.OldId = (trabalho.Membros.Id);
                        }
                        else if (trabalho.Funcao == "Orientador 2")
                        { 
                            trabalhosVM.Orientador_2 = (trabalho.Membros);
                            trabalhosVM.Orientador_2.OldId = (trabalho.Membros.Id);

                        }
                    }


                    return View(trabalhosVM);
                }
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, "Error fetching data from API");

                // Handle the exception by returning the Trabalhos Index view
                return RedirectToAction(nameof(Index));
            }
        }

        // POST: Trabalhos/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit_Trabalho(int id, TrabalhosViewModel trabalhosVM)
        {
            if (id != trabalhosVM.Id)
            {
                return NotFound();
            }

            bool hasErrors = false;

            if (trabalhosVM.Aluno_Id == 0)
            {
                TempData["Aluno_IdError"] = "Por favor, forneça um Aluno.";
                hasErrors = true;
            }

            if (trabalhosVM.Orientador_1.Id == 0)
            {
                TempData["Orientador_1_IdError"] = "Por favor, forneça um Orientador 1.";
                hasErrors = true;
            }

            if (trabalhosVM.Orientador_2.Id == 0)
            {
                TempData["Orientador_2_IdError"] = "Por favor, forneça um Orientador 2.";
                hasErrors = true;
            }

            if (trabalhosVM.Data_Defesa != null)
            {
                if (trabalhosVM.Presidente.Id == 0)
                {
                    TempData["Presidente_IdError"] = "Por favor, forneça um Presidente.";
                    hasErrors = true;
                }

                if (trabalhosVM.Arguente_1.Id == 0)
                {
                    TempData["Arguente_1_IdError"] = "Por favor, forneça um Arguente 1.";
                    hasErrors = true;
                }

                if (trabalhosVM.Vogal.Id == 0)
                {
                    TempData["Vogal_IdError"] = "Por favor, forneça um Vogal.";
                    hasErrors = true;
                }
            }

            if (hasErrors)
            {
                return View(trabalhosVM);
            }



            try
            {
                HttpResponseMessage response = null;
                string apiResponse = null;

                Trabalhos trabalhos = new Trabalhos()
                {
                    Id = id,
                    Titulo = trabalhosVM.Titulo,
                    ReferenciaInfo = trabalhosVM.ReferenciaInfo,
                    Tipo = trabalhosVM.Tipo,
                    Ano_Letivo = trabalhosVM.Ano_Letivo,
                    AdendaProtocolo = trabalhosVM.AdendaProtocolo,
                    Aluno_Id = trabalhosVM.Aluno_Id,
                    Nota = trabalhosVM.Nota,
                    Empresa_Id = trabalhosVM.Empresa_Id

                };

                // Serialize the Trabalhos object to JSON
                string json = JsonConvert.SerializeObject(trabalhos);

                // Send a PUT request to the API to update the Trabalhos object with the specified ID
                response = await _InternalClient.PutAsync(_APIserver + $"/Trabalhos/{id}",
                    new StringContent(json, Encoding.UTF8, "application/json"));

                response.EnsureSuccessStatusCode();

                //.....................................................//

                //Juri and Juri Menbros
                if(trabalhosVM.Data_Defesa != null)
                {

                    List <JuriMembros> juriMembrosList = new List<JuriMembros>();

                    Dictionary<string, Membros> roleToMembro = new Dictionary<string, Membros>();

                    if (trabalhosVM.Presidente.Id != 0)
                    {
                        roleToMembro.Add("Presidente", trabalhosVM.Presidente);
                    }

                    if (trabalhosVM.Arguente_1.Id != 0)
                    {
                        roleToMembro.Add("Arguente 1", trabalhosVM.Arguente_1);
                    }

                    if (trabalhosVM.Arguente_2.Id != 0)
                    {
                        if(trabalhosVM.Arguente_2.OldId == null && trabalhosVM.Juri.Id != 0)
                        {
                            JuriMembros juriMembros = new JuriMembros()
                            {
                                Juri_Id = trabalhosVM.Juri.Id,
                                Membro_Id = trabalhosVM.Arguente_2.Id,
                                Funcao = "Arguente 2"

                            };
                            string JuriMembrosjson = JsonConvert.SerializeObject(juriMembros);

                            // Send a POST request to the API to create a new Trabalhos object
                            response = await _InternalClient.PostAsync(_APIserver + "/JuriMembros/Create",
                                new StringContent(JuriMembrosjson, Encoding.UTF8, "application/json"));

                            response.EnsureSuccessStatusCode();
                        }
                        else
                        roleToMembro.Add("Arguente 2", trabalhosVM.Arguente_2);
                    }
                    else
                    {
                        if(trabalhosVM.Arguente_2.OldId != 0)
                        { 
                            JuriMembros juriMembros = new JuriMembros()
                            {
                                Juri_Id = trabalhosVM.Juri.Id,
                                Membro_Id = (int)trabalhosVM.Arguente_2.OldId,
                                Funcao = "Arguente 2"

                            };

                            // Send a PUT request to the API to update the Juri object with the specified ID
                            response = await _InternalClient.DeleteAsync(_APIserver + $"/JuriMembros/{juriMembros.Juri_Id}/{juriMembros.Membro_Id}");

                            response.EnsureSuccessStatusCode();
                        }
                    }

                    if (trabalhosVM.Vogal.Id != 0)
                    {
                        roleToMembro.Add("Vogal", trabalhosVM.Vogal);
                    }


                    if (trabalhosVM.Juri.Id != 0) //If Juri created

                    {

                        Juri juri = new Juri()
                        {
                            Id = trabalhosVM.Juri.Id,
                            Trabalho_Id = id,
                            Data_Defesa = trabalhosVM.Data_Defesa,
                        };

                            // Serialize the Trabalhos object to JSON
                            string jsonJuri = JsonConvert.SerializeObject(juri);

                        // Send a PUT request to the API to update the Juri object with the specified ID
                        var responseJuri = await _InternalClient.PutAsync(_APIserver + $"/Juri/{juri.Id}",
                            new StringContent(jsonJuri, Encoding.UTF8, "application/json"));

                        responseJuri.EnsureSuccessStatusCode();


                        foreach (var pair in roleToMembro)
                        {
                            JuriMembros juriMembros = new JuriMembros()
                            {
                                Juri_Id = trabalhosVM.Juri.Id,
                                Membro_Id = pair.Value.Id,
                                Funcao = pair.Key

                            };
                            // Serialize the Trabalhos object to JSON
                            string JuriMembrosjson = JsonConvert.SerializeObject(juriMembros);

                            // Send a PUT request to the API to update the Juri object with the specified ID
                            response = await _InternalClient.PutAsync(_APIserver + $"/JuriMembros/{juri.Id}/{pair.Value.OldId}",
                                new StringContent(JuriMembrosjson, Encoding.UTF8, "application/json"));

                            response.EnsureSuccessStatusCode();
           
                        }


                    }
                    else    //If Juri need to be created
                    {
                        Juri juri = new Juri()
                        {
                            Trabalho_Id = id,
                            Data_Defesa = trabalhosVM.Data_Defesa,
                        };

                        // Serialize the Trabalhos object to JSON
                        string Jurijson = JsonConvert.SerializeObject(juri);

                        // Send a POST request to the API to create a new Trabalhos object
                        response = await _InternalClient.PostAsync(_APIserver + "/Juri/Create",
                            new StringContent(Jurijson, Encoding.UTF8, "application/json"));

                        response.EnsureSuccessStatusCode();

                        apiResponse = await response.Content.ReadAsStringAsync();
                        // Use JObject to parse the JSON response
                        var createdJuri = JObject.Parse(apiResponse);
                        // Get the Id value as an integer
                        int createdJuriId = createdJuri.Value<int>("id");


                        foreach (var pair in roleToMembro)
                        {
                            JuriMembros juriMembros = new JuriMembros()
                            {
                                Juri_Id = createdJuriId,
                                Membro_Id = pair.Value.Id,
                                Funcao = pair.Key

                            };
                            // Serialize the Trabalhos object to JSON
                            string JuriMembrosjson = JsonConvert.SerializeObject(juriMembros);

                            // Send a POST request to the API to create a new Trabalhos object
                            response = await _InternalClient.PostAsync(_APIserver + "/JuriMembros/Create",
                                new StringContent(JuriMembrosjson, Encoding.UTF8, "application/json"));

                            response.EnsureSuccessStatusCode();

                        }
                    }

                
                }
                //Orientadores Input
                Orientadores orientador_1 = new Orientadores()
                {
                    Trabalho_Id = id,
                    Membro_Id = trabalhosVM.Orientador_1.Id,
                    Funcao = "Orientador 1"
                };

                Orientadores orientador_2 = new Orientadores()
                {
                    Trabalho_Id = id,
                    Membro_Id = trabalhosVM.Orientador_2.Id,
                    Funcao = "Orientador 2"
                };

                // Serialize the Trabalhos object to JSON
                string orientador1json = JsonConvert.SerializeObject(orientador_1);

                // Send a PUT request to the API to update the Juri object with the specified ID
                response = await _InternalClient.PutAsync(_APIserver + $"/Orientadores/{id}/{trabalhosVM.Orientador_1.OldId}",
                    new StringContent(orientador1json, Encoding.UTF8, "application/json"));

                response.EnsureSuccessStatusCode();

                // Serialize the Trabalhos object to JSON
                string orientador2json = JsonConvert.SerializeObject(orientador_2);

                // Send a PUT request to the API to update the Juri object with the specified ID
                response = await _InternalClient.PutAsync(_APIserver + $"/Orientadores/{id}/{trabalhosVM.Orientador_2.OldId}",
                    new StringContent(orientador2json, Encoding.UTF8, "application/json"));

                response.EnsureSuccessStatusCode();



                // Redirect to the Index action
                return RedirectToAction(nameof(Index));
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, "Error updating Trabalhos object");

                // Handle the exception by returning the Trabalhos Edit view with the current object
                return View(trabalhosVM);
            }


        }


        // GET: Trabalhos/Delete/5
        public async Task<IActionResult> Delete_Trabalho(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            try
            {
                // Send a GET request to the API to retrieve the Trabalhos object with the specified ID
                HttpResponseMessage message = await _InternalClient.GetAsync(_APIserver + $"/Trabalhos/{id}");

                string body = await message.Content.ReadAsStringAsync();

                Trabalhos trabalhos = JsonConvert.DeserializeObject<Trabalhos>(body);

                if (trabalhos == null)
                {
                    return NotFound();
                }

                return View(trabalhos);
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, "Error fetching data from API");

                // Handle the exception by returning the Trabalhos Index view
                return RedirectToAction(nameof(Index));
            }
        }

        // POST: Trabalhos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed_Trabalho(int id)
        {
            try
            {
                // Send a DELETE request to the API to delete the Trabalhos object with the specified ID
                var response = await _InternalClient.DeleteAsync(_APIserver + $"/Trabalhos/{id}");

                response.EnsureSuccessStatusCode();

                // Redirect to the Index action
                return RedirectToAction(nameof(Index));
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, "Error deleting Trabalhos object");

                // Handle the exception by returning the Trabalhos Delete view with the current object
                HttpResponseMessage message = await _InternalClient.GetAsync(_APIserver + $"/Trabalhos/{id}");

                string body = await message.Content.ReadAsStringAsync();

                Trabalhos trabalhos = JsonConvert.DeserializeObject<Trabalhos>(body);

                return View(trabalhos);
            }
        }


        // POST: Trabalhos/DeleteMultiple
        [HttpPost]
        public async Task<IActionResult> DeleteMultiple(List<int> ids)
        {


            try
            {
                // Serialize the list of IDs to JSON
                string json = JsonConvert.SerializeObject(ids);

                // Send a DELETE request to the API to delete the Trabalhos objects with the specified IDs
                var response = await _InternalClient.PostAsync(_APIserver + "/Trabalhos/DeleteMultiple",
                    new StringContent(json, Encoding.UTF8, "application/json"));

                response.EnsureSuccessStatusCode();

                // Redirect to the Index action
                return RedirectToAction(nameof(Index));
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, "Error deleting Trabalhos objects");
                ViewData["ErrorMessage"] += ex.Message;

                // Handle the exception by returning the Trabalhos Index view
                return RedirectToAction(nameof(Index));
            }

        }

    

        //----------------------------------------Orientadores-----------------------------------------------------------------------------------------------

        // GET: Trabalhos/Membros/5
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

                Membros orientador = JsonConvert.DeserializeObject<Membros>(body);

                if (orientador == null)
                {
                    return NotFound();
                }

                return PartialView("Info_Orientador", orientador);
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, "Error fetching data from API");

                // Handle the exception by returning the Trabalhos Index view
                return RedirectToAction(nameof(Index));
            }
        }
        

        // POST: Trabalhos/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit_Orientador(int id, Trabalhos trabalhos)
        {
            if (id != trabalhos.Id)
            {
                return NotFound();
            }

            try
            {
                // Serialize the Trabalhos object to JSON
                string json = JsonConvert.SerializeObject(trabalhos);

                // Send a PUT request to the API to update the Trabalhos object with the specified ID
                var response = await _InternalClient.PutAsync(_APIserver + $"/Trabalhos/Membros/{id}",
                    new StringContent(json, Encoding.UTF8, "application/json"));

                response.EnsureSuccessStatusCode();

                // Redirect to the Index action
                return RedirectToAction(nameof(Index));
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, "Error updating Trabalhos object");

                // Handle the exception by returning the Trabalhos Edit view with the current object
                return View(trabalhos);
            }


            return View(trabalhos);
        }

        // GET: Alunos
        public async Task<IActionResult> Alunos_Index()
        {
            try
            {
                HttpResponseMessage message = await _InternalClient.GetAsync(_APIserver + "/Alunos/List_Active");

                string body = await message.Content.ReadAsStringAsync();

                Alunos_list = JsonConvert.DeserializeObject<List<Alunos>>(body);

                if (Alunos_list == null)
                {
                    Alunos_list = new List<Alunos>();
                }


                return PartialView("Select_Aluno", Alunos_list);
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, "Error fetching data from API");

                // handle the exception by returning the Alunos Index view without making the API call

                return View(Alunos_list);
            }
        }

        // GET: Empresas
        public async Task<IActionResult> Empresas_Index()
        {
            try
            {
                HttpResponseMessage message = await _InternalClient.GetAsync(_APIserver + "/Empresas/Index");

                string body = await message.Content.ReadAsStringAsync();

                Empresas_list = JsonConvert.DeserializeObject<List<Empresas>>(body);

                if (Empresas_list == null)
                {
                    Empresas_list = new List<Empresas>();
                }


                return PartialView("Select_Empresa", Empresas_list);
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, "Error fetching data from API");

                return View(Empresas_list);
            }
        }

        public async Task<IActionResult> Membros_Index(string role)
        {
            try
            {
                HttpResponseMessage message = await _InternalClient.GetAsync(_APIserver + "/Trabalhos/Membros/List");

                string body = await message.Content.ReadAsStringAsync();

                Membros_list = JsonConvert.DeserializeObject<List<Membros>>(body);

                if (Membros_list == null)
                {
                    Membros_list = new List<Membros>();
                }

                ViewBag.Role = role;
                return PartialView("Select_Membro", Membros_list);
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, "Error fetching data from API");

                // handle the exception by returning the Alunos Index view without making the API call

                return View(Membros_list);
            }
        }
    }//End Of Class
}//End of Namespace

