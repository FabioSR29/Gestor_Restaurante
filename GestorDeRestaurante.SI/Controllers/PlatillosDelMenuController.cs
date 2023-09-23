using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GestorDeRestaurante.SI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlatillosDelMenuController : ControllerBase
    {

        private readonly BS.IRepositorioDelRestaurante ElRepositorio;

        public PlatillosDelMenuController(BS.IRepositorioDelRestaurante repositorio)
        {
            ElRepositorio = repositorio;
        }
        // GET: api/<PlatillosDelMenusController>

        [HttpGet("ObtengaLaListaDePlatillos")]
        public IEnumerable<GestorDeRestaurante.Model.Menu> ObtengaLaListaDePlatillos()
        {
            List<Model.Menu> elResultado;
            elResultado = ElRepositorio.ObtengaLaListaDePlatillos();
            return elResultado;
        }

        // GET api/<PlatillosDelMenusController>/5
        [HttpGet("ObtengaElMenuCompleto")]
        public Model.MenuCompleto ObtengaElMenuCompleto()
        {
            Model.MenuCompleto ElMenuCompleto;
            ElMenuCompleto = ElRepositorio.ObtengaElMenuCompleto();

            return ElMenuCompleto;
        }

        // POST api/<PlatillosDelMenusController>
        [HttpPost("IngresePlatillo")]
        public IActionResult Post([FromBody] GestorDeRestaurante.Model.Menu platillos)
        {
            if (ModelState.IsValid)
            {
                ElRepositorio.AgregueElPlatillo(platillos);
                return Ok(platillos);
            }
            else
            {
                return BadRequest(ModelState);
            }

        }

        // PUT api/<PlatillosDelMenusController>/5
        [HttpGet("ObtengaPlatillosPorId")]
        public GestorDeRestaurante.Model.Menu ObtengaPlatillosPorId(int id)
        {
            Model.Menu elResultado;
            elResultado = ElRepositorio.ObtenerPlatillosPorId(id);
            return elResultado;
        }


        [HttpPut("EditarPlatillo")]
        public IActionResult Put([FromBody] GestorDeRestaurante.Model.Menu ElPlatillo)
        {

            if (ModelState.IsValid)
            {
                ElRepositorio.EditarLosPlatilos(ElPlatillo);
                return Ok(ElPlatillo);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }
        // DELETE api/<PlatillosDelMenusController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
