using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GestorDeRestaurante.SI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IngredientesController : ControllerBase
    {
        private readonly BS.IRepositorioDelRestaurante ElRepositorio;

        public IngredientesController(BS.IRepositorioDelRestaurante repositorio)
        {
            ElRepositorio = repositorio;
        }

        // GET: api/<IngredientesController>
        [HttpGet("ObtengaLaLista")]
        public IEnumerable<GestorDeRestaurante.Model.Ingredientes> ObtengaLaListaDeIngredientes()
        {
            List<Model.Ingredientes> elResultado;
            elResultado = ElRepositorio.ObtengaLaListaDeIngredientes();
            return elResultado;
        }

     

        // POST api/<IngredientesController>
        [HttpPost]
        public IActionResult Post([FromBody] GestorDeRestaurante.Model.Ingredientes ingredientes)
        {

            if (ModelState.IsValid)
            {
                ElRepositorio.AgregueIngredientes(ingredientes);
                return Ok(ingredientes);
            }
            else
            {
                return BadRequest(ModelState);
            }

        }

        // GET: api/<EstudiantesController>
        [HttpGet("ObtenerIngredientePorId")]
        public GestorDeRestaurante.Model.Ingredientes ObtenerIngredientePorId(int id)
        {
            Model.Ingredientes elResultado;
            elResultado = ElRepositorio.ObtenerIngredientePorId(id);
            return elResultado;
        }

        // PUT api/<IngredientesController>/5
        [HttpPut]
        public IActionResult Put([FromBody] GestorDeRestaurante.Model.Ingredientes ingredientes)
        {

            if (ModelState.IsValid)
            {
                ElRepositorio.EditarIngredientes(ingredientes);
                return Ok(ingredientes);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }
        // GET api/<IngredientesController>/5
        [HttpGet("ListarPlatillosPorIngrediente/{id}")]
        public IEnumerable<Model.IngredientesDelPlatillo> ListarPlatillosPorIngrediente(int id)
        {
            List<Model.IngredientesDelPlatillo> elResultado;
            elResultado = ElRepositorio.ObtengaLaListaDePlatillosPorIngrediente(id);
            return elResultado;
        }
    }
}
