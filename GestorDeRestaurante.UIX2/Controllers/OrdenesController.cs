using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using GestorDeRestaurante.Model;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;

namespace GestorDeRestaurante.UI.Controllers
{
    [Authorize]
    public class OrdenesController : Controller
    {


        // GET: OrdenController
        public async Task<IActionResult> Index()
        {
            Model.MesasOrdenes laLista;
            try
            {
                var httpClient = new HttpClient();

                var response = await httpClient.GetAsync("https://gestorderestaurante--si.azurewebsites.net/api/Ordenes/ObtengaLasMesasDeOrdenes");
                string apiResponse = await response.Content.ReadAsStringAsync();
                laLista = JsonConvert.DeserializeObject<Model.MesasOrdenes>(apiResponse);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return View(laLista);
        }

        // GET: OrdenController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: OrdenController/Create
        public async Task<IActionResult> Create(int id)
        {
            List<Menu> laLista = new List<Menu>();

            try
            {
                var httpClient = new HttpClient();

                var response = await httpClient.GetAsync("https://gestorderestaurante--si.azurewebsites.net/api/Ordenes/ObtengaLosPlatillos");

                string apiResponse = await response.Content.ReadAsStringAsync();

                laLista = JsonConvert.DeserializeObject<List<Menu>>(apiResponse);

            }
            catch (Exception)
            {

                throw;
            }

            ViewBag.laLista = laLista;

            return View();
        }

        // POST: OrdenController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(String Menu, MesaOrden Orden)
        {
            int idPlatillo = Int32.Parse(Menu);
            try
            {

                GestorDeRestaurante.Model.MesaOrden laOrden = new Model.MesaOrden();


                laOrden.Id_Mesa = Orden.Id;
                laOrden.Id_Menu = idPlatillo;
                laOrden.Cantidad = Orden.Cantidad;


                var httpClient = new HttpClient();

                string json = JsonConvert.SerializeObject(laOrden);

                var buffer = System.Text.Encoding.UTF8.GetBytes(json);

                var byteContent = new ByteArrayContent(buffer);

                byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                await httpClient.PostAsync("https://gestorderestaurante--si.azurewebsites.net/api/Ordenes/IngreseLaOrden", byteContent);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }

        }



        public async Task<IActionResult> SeleccionarPlatillos(int id, int id_Mesa, int id_orden)
        {

            Model.MesaOrden laOrden = new Model.MesaOrden();
            laOrden.Id_Menu = id;
            laOrden.Id_Mesa = id_Mesa;
            laOrden.Id = id_orden;


            try
            {

                var httpClient = new HttpClient();

                string json = JsonConvert.SerializeObject(laOrden);

                var buffer = System.Text.Encoding.UTF8.GetBytes(json);

                var byteContent = new ByteArrayContent(buffer);

                byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                await httpClient.PutAsync("https://gestorderestaurante--si.azurewebsites.net/api/Ordenes/SeleccionarPlatillo", byteContent);


                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }



        // GET: OrdenController/Edit/5
        public async Task<IActionResult> Servir(int id)
        {
            Model.Mesas laOrden;
            try
            {
                var httpClient = new HttpClient();

                var response = await httpClient.GetAsync("https://gestorderestaurante--si.azurewebsites.net/api/Ordenes/" + id.ToString());

                string apiResponse = await response.Content.ReadAsStringAsync();

                laOrden = JsonConvert.DeserializeObject<Model.Mesas>(apiResponse);
            }
            catch (Exception)
            {

                throw;
            }
            return View(laOrden);
        }



        // GET: OrdenController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: OrdenController/Delete/5
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

