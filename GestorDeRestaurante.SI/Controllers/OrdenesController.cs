
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GestorDeRestaurante.SI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdenesController : ControllerBase
    {
        private readonly BS.IRepositorioDelRestaurante ElRepositorio;

        public OrdenesController(BS.IRepositorioDelRestaurante repositorio)
        {
            ElRepositorio = repositorio;
        }


        // GET api/<OrdenesController>/5
        [HttpGet("ObtengaLasMesasDeOrdenes")]
        public Model.MesasOrdenes ObtengaLasMesasDeOrdenes()
        {
            Model.MesasOrdenes lasMesas;
            lasMesas = ElRepositorio.ObtengaLasMesasDeOrdenes();

            return lasMesas;
        }

        // GET api/<OrdenesController>/5
        [HttpGet("ObtengaLosPlatillos")]
        public IEnumerable<GestorDeRestaurante.Model.Menu> ObtengaLoslatillos()
        {
            List<Model.Menu> elResultado;
            elResultado = ElRepositorio.ObtengaLaListaDePlatillos();
            return elResultado;
        }

        // POST api/<MesasController>
        [HttpPost("IngreseLaOrden")]
        public IActionResult Post([FromBody] GestorDeRestaurante.Model.MesaOrden lasOrdenes)
        {

            if (ModelState.IsValid)
            {
                ElRepositorio.AgregarUnaOrden(lasOrdenes);
                return Ok(lasOrdenes);
            }
            else
            {
                return BadRequest(ModelState);
            }

        }

        // GET: api/<MesasController>
        [HttpGet("ObtenerUnaOrden")]
        public GestorDeRestaurante.Model.MesaOrden ObtenerUnaOrden(int id)
        {
            Model.MesaOrden elResultado;
            elResultado = ElRepositorio.ObtenerUnaOrden();
            return elResultado;
        }

        [HttpGet("{id}")]
        public GestorDeRestaurante.Model.Mesas Get(int id)
        {
            Model.Mesas lasMesa = new Model.Mesas();
            lasMesa.Id = id;
            lasMesa.PlatillosAsociadosALaMesa = ElRepositorio.ObtengaLosPlatillosAsociadosAUnaMesa(id);


            return lasMesa;
        }





        // POST api/<OrdenesController>
        [HttpPut("SeleccionarPlatillo")]
        public IActionResult SeleccionarPlatillo([FromBody] GestorDeRestaurante.Model.MesaOrden laOrden)
        {
            if (ModelState.IsValid)
            {
                ElRepositorio.CambieEstadoDeOrden(laOrden);
                return Ok(laOrden);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        // PUT api/<OrdenesController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<OrdenesController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}