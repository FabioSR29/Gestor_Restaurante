using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GestorDeRestaurante.SI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IngredientesDelMenuController : ControllerBase
    {
        private readonly BS.IRepositorioDelRestaurante ElRepositorio;

        public IngredientesDelMenuController(BS.IRepositorioDelRestaurante repositorio)
        {
            ElRepositorio = repositorio;
        }

        // GET: api/<IngredientesDelMenuController>
        [HttpGet("ObtengaElMenuParaAdministrarLosIngredientes")]
        public IEnumerable<GestorDeRestaurante.Model.Menu> ObtengaElMenuParaAdministrarLosIngredientes()
        {
            List<Model.Menu> elResultado;
            List<Model.Menu> laListaDelMenu;
            List<Model.MenuIngredientes> elMenuDeIngredientes;

            laListaDelMenu = ElRepositorio.ObtengaLaListaDePlatillos();
            elMenuDeIngredientes = ElRepositorio.ObtengaElMenuDeIngredientes();

            elResultado = ElRepositorio.ObtengaLaListaDelMenuParaIngredientes(laListaDelMenu, elMenuDeIngredientes);

            return elResultado;
        }

        // GET api/<IngredientesDelMenuController>/5
        [HttpGet("ObtengaLaListaDeIngredientesDeUnPlatilloPorId")]
        public IEnumerable<GestorDeRestaurante.Model.MenuIngredientes> ObtengaLaListaDeIngredientesDeUnPlatilloPorId(int id)
        {
            List<Model.MenuIngredientes> elResultado;

            elResultado = ElRepositorio.ObtengaLaListaDeIngredientesDeUnPlatilloPorId(id);

            return elResultado;
        }

        // GET api/<IngredientesDelMenuController>/5
        [HttpGet("ObtengaLaListaDeIngredientes")]
        public IEnumerable<GestorDeRestaurante.Model.Ingredientes> ObtengaLaListaDeIngredientes()
        {
            List<Model.Ingredientes> elResultado;

            elResultado = ElRepositorio.ObtengaLaListaDeIngredientes();

            return elResultado;
        }

        // GET api/<IngredientesDelMenuController>/5
        [HttpGet("ObtengaLaListaDeMedidas")]
        public IEnumerable<GestorDeRestaurante.Model.Medidas> ObtengaLaListaDeMedidas()
        {
            List<Model.Medidas> elResultado;

            elResultado = ElRepositorio.ObtengaLaListaDeMedidas();

            return elResultado;
        }

        // POST api/<IngredientesDelMenuController>
        [HttpPost("AsocieUnIngrediente")]
        public IActionResult Post([FromBody] GestorDeRestaurante.Model.MenuIngredientes elIngredienteAsociado)
        {
            if (ModelState.IsValid)
            {
                ElRepositorio.AgregueElIngredienteAsociado(elIngredienteAsociado);
                return Ok(elIngredienteAsociado);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }


        // DELETE api/<IngredientesDelMenuController>/5

        [HttpPut("DesasocieUnIngrediente")]
        public IActionResult DesasocieUnIngrediente([FromBody] int id)
        {
            Model.MenuIngredientes elIngredientePorDesasociar;
            elIngredientePorDesasociar = ElRepositorio.ObtenerIngredienteAsociadoPorId(id);
            ElRepositorio.DesasociarUnIngrediente(elIngredientePorDesasociar);
            return NoContent();
        }
    }
}
