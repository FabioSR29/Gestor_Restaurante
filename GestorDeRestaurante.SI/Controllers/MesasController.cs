using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GestorDeRestaurante.SI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MesasController : ControllerBase
    {
        private readonly BS.IRepositorioDelRestaurante ElRepositorio;

        public MesasController(BS.IRepositorioDelRestaurante repositorio)
        {
            ElRepositorio = repositorio;
        }

        // GET: api/<MesasController>
        [HttpGet("ObtengaLaListaDeMesas")]
        public IEnumerable<GestorDeRestaurante.Model.Mesas> ObtengaLaListaDeMesas()
        {
            List<Model.Mesas> elResultado;
            elResultado = ElRepositorio.ObtengaLaListaDeMesas();
            return elResultado;
        }


        // POST api/<MesasController>
        [HttpPost]
        public IActionResult Post([FromBody] GestorDeRestaurante.Model.Mesas lasMesas)
        {

            if (ModelState.IsValid)
            {
                ElRepositorio.AgregueLasMesas(lasMesas);
                return Ok(lasMesas);
            }
            else
            {
                return BadRequest(ModelState);
            }

        }

        // GET: api/<MesasController>
        [HttpGet("ObtenerMesasPorId")]
        public GestorDeRestaurante.Model.Mesas ObtenerMesasPorId(int id)
        {
            Model.Mesas elResultado;
            elResultado = ElRepositorio.ObtenerMesasPorId(id);
            return elResultado;
        }

    

        [HttpPut("Editar")]
        public IActionResult Put([FromBody] GestorDeRestaurante.Model.Mesas lasMesas)
        {
            if (ModelState.IsValid)
            {
                ElRepositorio.EditarLasMesas(lasMesas);
                return Ok(lasMesas);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        // PUT api/<ValuesController>/5
        [HttpPut("Deshabilitar")]
        public IActionResult Deshabilitar([FromBody] GestorDeRestaurante.Model.Mesas lasMesas)
        {

            lasMesas = ElRepositorio.ObtenerMesasPorId(lasMesas.Id);
            lasMesas.Estado = Model.Estado.NoDisponible;
            ElRepositorio.EditarLasMesas(lasMesas);
            return Ok(lasMesas);
        }
        [HttpPut("Habilitar")]
        public IActionResult Habilitar([FromBody] GestorDeRestaurante.Model.Mesas lasMesas)
        {

            lasMesas = ElRepositorio.ObtenerMesasPorId(lasMesas.Id);
            lasMesas.Estado = Model.Estado.Disponible;
            ElRepositorio.EditarLasMesas(lasMesas);
            return Ok(lasMesas);
        }
    }
}
