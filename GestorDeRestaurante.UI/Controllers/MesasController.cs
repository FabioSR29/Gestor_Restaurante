using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace GestorDeRestaurante.UI.Controllers
{
    public class MesasController : Controller
    {
        // GET: MesaController
        public async Task<IActionResult> Index()
        {
            List<Model.Mesas> laLista;
            try
            {
                var httpClient = new HttpClient();

                var response = await httpClient.GetAsync("https://localhost:7071/api/Mesas/ObtengaLaListaDeMesas");
                string apiResponse = await response.Content.ReadAsStringAsync();
                laLista = JsonConvert.DeserializeObject<List<GestorDeRestaurante.Model.Mesas>>(apiResponse);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return View(laLista);
        }

        // GET: MesaController/Details/5
        public async Task<IActionResult> Details(int Id)
        {
            Model.Mesas lasMesas;

            try

            {
                var httpClient = new HttpClient();
                var query = new Dictionary<string, string>()
                {

                    ["id"] = Id.ToString()
                };

                var uri = QueryHelpers.AddQueryString("https://localhost:7071/api/Mesas/ObtenerMesasPorId", query);
                var response = await httpClient.GetAsync(uri);
                string apiResponse = await response.Content.ReadAsStringAsync();
                lasMesas = JsonConvert.DeserializeObject<GestorDeRestaurante.Model.Mesas>(apiResponse);
            }
            catch (Exception ex)
            {
                throw ex;

            }

            return View(lasMesas);
        }

        // GET: MesaController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: MesaController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Model.Mesas mesas)
        {
            try
            {

                GestorDeRestaurante.Model.Mesas lasMesas = new Model.Mesas();


                lasMesas.Nombre = mesas.Nombre;


                var httpClient = new HttpClient();

                string json = JsonConvert.SerializeObject(lasMesas);

                var buffer = System.Text.Encoding.UTF8.GetBytes(json);

                var byteContent = new ByteArrayContent(buffer);

                byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                await httpClient.PostAsync("https://localhost:7071/api/Mesas", byteContent);

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
            Model.Mesas laMesa;

            try

            {
                var httpClient = new HttpClient();

                var query = new Dictionary<string, string>()
                {

                    ["id"] = id.ToString()
                };

                var uri = QueryHelpers.AddQueryString("https://localhost:7071/api/Mesas/ObtenerMesasPorId", query);
                var response = await httpClient.GetAsync(uri);
                string apiResponse = await response.Content.ReadAsStringAsync();

                laMesa = JsonConvert.DeserializeObject<GestorDeRestaurante.Model.Mesas>(apiResponse);

            }
            catch (Exception ex)
            {
                throw ex;

            }

            return View(laMesa);
        }



        // POST: MedidasController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Model.Mesas mesas)
        {
            try
            {

                var httpClient = new HttpClient();

                string json = JsonConvert.SerializeObject(mesas);

                var buffer = System.Text.Encoding.UTF8.GetBytes(json);

                var byteContent = new ByteArrayContent(buffer);

                byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                await httpClient.PutAsync("https://localhost:7071/api/Mesas/Editar", byteContent);


                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }


        public async Task<ActionResult> Deshabilitar(int id)
            {
            Model.Mesas lasMesas = new Model.Mesas();
            lasMesas.Id = id;
            lasMesas.Nombre = "";
            lasMesas.Estado = Model.Estado.NoDisponible;
                try
                {


                    var httpClient = new HttpClient();
                    string json = JsonConvert.SerializeObject(lasMesas);

                    var buffer = System.Text.Encoding.UTF8.GetBytes(json);

                    var byteContent = new ByteArrayContent(buffer);

                    byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                    var response = await httpClient.PutAsync("https://localhost:7071/api/Mesas/Deshabilitar" ,byteContent);

                }


                catch (Exception ex)
                {
                    throw ex;
                }


                return RedirectToAction(nameof(Index));

            }
        public async Task<ActionResult> Habilitar(int id)
        {
            Model.Mesas lasMesas = new Model.Mesas();
            lasMesas.Id = id;
            lasMesas.Nombre = "";
            lasMesas.Estado = Model.Estado.Disponible;
            try
            {


                var httpClient = new HttpClient();
                string json = JsonConvert.SerializeObject(lasMesas);

                var buffer = System.Text.Encoding.UTF8.GetBytes(json);

                var byteContent = new ByteArrayContent(buffer);

                byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                var response = await httpClient.PutAsync("https://localhost:7071/api/Mesas/Habilitar", byteContent);

            }


            catch (Exception ex)
            {
                throw ex;
            }


            return RedirectToAction(nameof(Index));
        }

    }
}
