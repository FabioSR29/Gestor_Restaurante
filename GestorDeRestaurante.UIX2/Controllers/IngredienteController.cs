using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace GestorDeRestaurante.UI.Controllers
{
    [Authorize]
    public class IngredienteController : Controller
    {
        // GET: IngredienteController
        public async Task<IActionResult> Index()
        {
            List<Model.Ingredientes> laLista;
            try
            {
                var httpClient = new HttpClient();

                var response = await httpClient.GetAsync("https://gestorderestaurante--si.azurewebsites.net/api/Ingredientes/ObtengaLaLista");
                string apiResponse = await response.Content.ReadAsStringAsync();
                laLista = JsonConvert.DeserializeObject<List<GestorDeRestaurante.Model.Ingredientes>>(apiResponse);

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return View(laLista);

        
        }

        // GET: IngredienteController/Details/5
        public async Task<IActionResult> Details(int id)
        {Model.Ingredientes elIngrediente;
            try
            {
                var httpClient = new HttpClient();

                var response = await httpClient.GetAsync("https://gestorderestaurante--si.azurewebsites.net/api/Ordenes/" + id.ToString());

                string apiResponse = await response.Content.ReadAsStringAsync();

                elIngrediente = JsonConvert.DeserializeObject<Model.Ingredientes>(apiResponse);
            }
            catch (Exception)
            {

                throw;
            }
            return View(elIngrediente) ;
        }

        // GET: IngredienteController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: IngredienteController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Model.Ingredientes ingrediente)
        {
            try
            {

                GestorDeRestaurante.Model.Ingredientes elIngrediente = new Model.Ingredientes();

                
                elIngrediente.Nombre = ingrediente.Nombre;


                var httpClient = new HttpClient();

                string json = JsonConvert.SerializeObject(elIngrediente);

                var buffer = System.Text.Encoding.UTF8.GetBytes(json);

                var byteContent = new ByteArrayContent(buffer);

                byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                await httpClient.PostAsync("https://gestorderestaurante--si.azurewebsites.net/api/Ingredientes", byteContent);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: IngredienteController/Edit/5
        public async Task<IActionResult> Edit(int id)
        {

            Model.Ingredientes elIngrediente;

            try

            {
                var httpClient = new HttpClient();

                var query = new Dictionary<string, string>()
                {

                    ["id"] = id.ToString()
                };

                var uri = QueryHelpers.AddQueryString("https://gestorderestaurante--si.azurewebsites.net/api/Ingredientes/ObtenerIngredientePorId", query);
                var response = await httpClient.GetAsync(uri);
                string apiResponse = await response.Content.ReadAsStringAsync();

                elIngrediente = JsonConvert.DeserializeObject<GestorDeRestaurante.Model.Ingredientes>(apiResponse);

            }
            catch (Exception ex)
            {
                throw ex;

            }
            return View(elIngrediente);
        }

        // POST: IngredienteController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Model.Ingredientes ingredientes)
        {
            try
            {

                var httpClient = new HttpClient();

                string json = JsonConvert.SerializeObject(ingredientes);

                var buffer = System.Text.Encoding.UTF8.GetBytes(json);

                var byteContent = new ByteArrayContent(buffer);

                byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                await httpClient.PutAsync("https://gestorderestaurante--si.azurewebsites.net/api/Ingredientes", byteContent);


                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }


        // POST: IngredienteController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
        public async Task<IActionResult> Detalles(int Id, string nombre)
        {
            List<Model.IngredientesDelPlatillo> laLista;

            try
            {
                var httpClient = new HttpClient();


                var response = await httpClient.GetAsync("https://gestorderestaurante--si.azurewebsites.net/api/Ingredientes/ListarPlatillosPorIngrediente/" + Id);
                string apiResponse = await response.Content.ReadAsStringAsync();
                laLista = JsonConvert.DeserializeObject<List<Model.IngredientesDelPlatillo>>(apiResponse);

            }
            catch (Exception ex)
            {
                throw ex;
            }



            return View(Tuple.Create(laLista, nombre));
        }
    }
}
