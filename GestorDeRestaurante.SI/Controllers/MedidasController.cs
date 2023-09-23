using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GestorDeRestaurante.SI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MedidasController : ControllerBase
    {
        private readonly BS.IRepositorioDelRestaurante ElRepositorio;

        public MedidasController(BS.IRepositorioDelRestaurante repositorio)
        {
            ElRepositorio = repositorio;
        }


        // GET: api/<MedidasController>
        [HttpGet("ObtengaLaListaDeMedidas")]
        public IEnumerable<GestorDeRestaurante.Model.Medidas> OntengaLaListaDeMedidas()
        {
            List<Model.Medidas> elResultado;
            elResultado = ElRepositorio.ObtengaLaListaDeMedidas();
            return elResultado;
        }

        // GET api/<MedidasController>/5
        [HttpGet("ObTengaLasMedidasPorNombre")]
        public IEnumerable<GestorDeRestaurante.Model.Medidas> ObTengaLasMedidasPorNombre(string nombre)
        {
            List<Model.Medidas> elResultado;
            elResultado = ElRepositorio.ObTengaLasMedidasPorNombre(nombre);
            return elResultado;
        }

        // GET api/<MedidasController>/5
        [HttpGet("ObtenerPorIdLaMedida")]
        public GestorDeRestaurante.Model.Medidas ObtenerPorIdLaMedida(int id)
        {
            Model.Medidas elResultado;
            elResultado = ElRepositorio.ObtenerPorIdLaMedida(id);
            return elResultado;
        }

        // POST api/<MedidasController>
        [HttpPost]
        public IActionResult Post([FromBody] GestorDeRestaurante.Model.Medidas medida)
        {
            if (ModelState.IsValid)
            {
                ElRepositorio.AgregueLaMedida(medida);
                return Ok(medida);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        // PUT api/<MedidasController>/5
        [HttpPut("Editar")]
        public IActionResult Put([FromBody] GestorDeRestaurante.Model.Medidas medida)
        {
            if (ModelState.IsValid)
            {
                ElRepositorio.EditarLaMedida(medida);
                return Ok(medida);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        // DELETE api/<MedidasController>/5
        [HttpDelete("Deshabilitar")]
        public IActionResult Deshabilitar([FromBody] GestorDeRestaurante.Model.Medidas medida)
        {

            medida = ElRepositorio.ObtenerPorIdLaMedida(medida.Id);
            ElRepositorio.EditarLaMedida(medida);
            return Ok(medida);
        }
    }
}
