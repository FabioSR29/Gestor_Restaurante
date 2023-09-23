using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace GestorDeRestaurante.UI.Controllers
{
    [Authorize]
    public class MedidaController : Controller
    {


        // GET: MedidasController
        public async Task<IActionResult> Index(string nombre)
        {
            List<Model.Medidas> laListaDeMedidas;
            try
            {
                var httpClient = new HttpClient();

                if (nombre is null)
                {
                    var response = await httpClient.GetAsync("https://localhost:7071/api/Medidas/ObtengaLaListaDeMedidas");
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    laListaDeMedidas = JsonConvert.DeserializeObject<List<GestorDeRestaurante.Model.Medidas>>(apiResponse);

                }
                else
                {
                    var query = new Dictionary<string, string>()
                    {

                        ["nombre"] = nombre
                    };

                    var uri = QueryHelpers.AddQueryString("https://localhost:7071/api/Medidas/ObTengaLasMedidasPorNombre", query);
                    var response = await httpClient.GetAsync(uri);
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    laListaDeMedidas = JsonConvert.DeserializeObject<List<GestorDeRestaurante.Model.Medidas>>(apiResponse);

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return View(laListaDeMedidas);
        }

        // GET: MedidasController/Details/5
        public async Task<IActionResult> Details(int id)
        {
            Model.Medidas laMedida;

            try

            {
                var httpClient = new HttpClient();
                var query = new Dictionary<string, string>()
                {

                    ["id"] = id.ToString()
                };

                var uri = QueryHelpers.AddQueryString("https://localhost:7071/api/Medidas/ObtenerPorIdLaMedida", query);
                var response = await httpClient.GetAsync(uri);
                string apiResponse = await response.Content.ReadAsStringAsync();
                laMedida = JsonConvert.DeserializeObject<GestorDeRestaurante.Model.Medidas>(apiResponse);
            }
            catch (Exception ex)
            {
                throw ex;

            }

            return View(laMedida);
        }

        // GET: MedidasController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: MedidasController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Model.Medidas medida)
        {
            try
            {

                GestorDeRestaurante.Model.Medidas laMedida = new Model.Medidas();

                laMedida.Nombre = medida.Nombre;



                var httpClient = new HttpClient();

                string json = JsonConvert.SerializeObject(medida);

                var buffer = System.Text.Encoding.UTF8.GetBytes(json);

                var byteContent = new ByteArrayContent(buffer);

                byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                await httpClient.PostAsync("https://localhost:7071/api/Medidas", byteContent);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: MedidasController/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            Model.Medidas laMedida;

            try

            {
                var httpClient = new HttpClient();

                var query = new Dictionary<string, string>()
                {

                    ["id"] = id.ToString()
                };

                var uri = QueryHelpers.AddQueryString("https://localhost:7071/api/Medidas/ObtenerPorIdLaMedida", query);
                var response = await httpClient.GetAsync(uri);
                string apiResponse = await response.Content.ReadAsStringAsync();

                laMedida = JsonConvert.DeserializeObject<GestorDeRestaurante.Model.Medidas>(apiResponse);

            }
            catch (Exception ex)
            {
                throw ex;

            }

            return View(laMedida);
        }

        // POST: MedidasController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Model.Medidas medida)
        {
            try
            {

                var httpClient = new HttpClient();

                string json = JsonConvert.SerializeObject(medida);

                var buffer = System.Text.Encoding.UTF8.GetBytes(json);

                var byteContent = new ByteArrayContent(buffer);

                byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                await httpClient.PutAsync("https://localhost:7071/api/Medidas/Editar", byteContent);


                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }


      
    }

}
