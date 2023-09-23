using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace GestorDeRestaurante.UI.Controllers
{
    public class PlatilloDelMenuController : Controller
    {
        // GET: PlatilloController
        public async Task<IActionResult> Index()
        {
            List<Model.Menu> laLista;
            try
            {
                var httpClient = new HttpClient();

                var response = await httpClient.GetAsync("https://localhost:7071/api/PlatillosDelMenu/ObtengaLaListaDePlatillos");
                string apiResponse = await response.Content.ReadAsStringAsync();
                laLista = JsonConvert.DeserializeObject<List<GestorDeRestaurante.Model.Menu>>(apiResponse);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return View(laLista);
        }

        // GET: PlatilloController/Details/5
        public async Task<IActionResult> Details(int id)
        {
            Model.Menu ELPlatillo;

            try

            {
                var httpClient = new HttpClient();
                var query = new Dictionary<string, string>()
                {

                    ["id"] = id.ToString()
                };

                var uri = QueryHelpers.AddQueryString("https://localhost:7071/api/PlatillosDelMenu/ObtengaPlatillosPorId", query);
                var response = await httpClient.GetAsync(uri);
                string apiResponse = await response.Content.ReadAsStringAsync();
                ELPlatillo = JsonConvert.DeserializeObject<GestorDeRestaurante.Model.Menu>(apiResponse);
            }
            catch (Exception ex)
            {
                throw ex;

            }
            return View(ELPlatillo);
        }

        // GET: PlatilloController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PlatilloController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Model.Menu Platillo)
        {    
            try
            {
                GestorDeRestaurante.Model.Menu elPlatillo = new Model.Menu();
                var files = HttpContext.Request.Form.Files;
                if (files.Count>0)
                {
                    byte[] p1 = null;
                    using (var fsl = files[0].OpenReadStream())
                    {
                        using (var msl = new MemoryStream()) 
                        {
                            fsl.CopyTo(msl);
                            p1 = msl.ToArray();
                        
                        }
                    
                    }
                elPlatillo.Nombre = Platillo.Nombre;
                elPlatillo.Precio = Platillo.Precio;
                elPlatillo.Categoria = Platillo.Categoria;
                elPlatillo.Imagen = p1;
                       
                }

                

                var httpClient = new HttpClient();

                string json = JsonConvert.SerializeObject(elPlatillo);

                var buffer = System.Text.Encoding.UTF8.GetBytes(json);

                var byteContent = new ByteArrayContent(buffer);

                byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                await httpClient.PostAsync("https://localhost:7071/api/PlatillosDelMenu/IngresePlatillo", byteContent);


                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: PlatilloController/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            Model.Menu ELPlatillo;

            try

            {
                var httpClient = new HttpClient();
                var query = new Dictionary<string, string>()

                {

                    ["id"] = id.ToString()
                };

                var uri = QueryHelpers.AddQueryString("https://localhost:7071/api/PlatillosDelMenu/ObtengaPlatillosPorId", query);
                var response = await httpClient.GetAsync(uri);
                string apiResponse = await response.Content.ReadAsStringAsync();
                ELPlatillo = JsonConvert.DeserializeObject<GestorDeRestaurante.Model.Menu>(apiResponse);
            }
            catch (Exception ex)
            {
                throw ex;

            }
            return View(ELPlatillo);
        }

        // POST: PlatilloController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Model.Menu Platillo)
        {
            try
            {
                GestorDeRestaurante.Model.Menu elPlatillo = new Model.Menu();
                var files = HttpContext.Request.Form.Files;
                if (files.Count > 0)
                {
                    byte[] p1 = null;
                    using (var fsl = files[0].OpenReadStream())
                    {
                        using (var msl = new MemoryStream())
                        {
                            fsl.CopyTo(msl);
                            p1 = msl.ToArray();

                        }

                    }
                    elPlatillo.Nombre = Platillo.Nombre;
                    elPlatillo.Id = Platillo.Id;
                    elPlatillo.Precio = Platillo.Precio;
                    elPlatillo.Categoria = Platillo.Categoria;
                    elPlatillo.Imagen = p1;

                }


                var httpClient = new HttpClient();

                string json = JsonConvert.SerializeObject(elPlatillo);

                var buffer = System.Text.Encoding.UTF8.GetBytes(json);

                var byteContent = new ByteArrayContent(buffer);

                byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                await httpClient.PutAsync("https://localhost:7071/api/PlatillosDelMenu/EditarPlatillo", byteContent);




                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        public async Task<IActionResult> MenuCompleto()
        {
            Model.MenuCompleto ElMenuCompleto;

            try

            {
                var httpClient = new HttpClient();

                var response = await httpClient.GetAsync("https://localhost:7071/api/PlatillosDelMenu/ObtengaElMenuCompleto");
                string apiResponse = await response.Content.ReadAsStringAsync();
                ElMenuCompleto = JsonConvert.DeserializeObject<Model.MenuCompleto>(apiResponse);
            }
            catch (Exception ex)
            {
                throw ex;

            }

            return View(ElMenuCompleto);
        }

        // GET: PlatilloController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: PlatilloController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
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
    }
}
