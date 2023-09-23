using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GestorDeRestaurante.Model
{
    public class Ingredientes
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "El campo nombre es requerido")]
        public string Nombre { get; set; }
        [NotMapped]

        public List<Menu>? losPlatillosAsociaados { get; set; }

    }
}