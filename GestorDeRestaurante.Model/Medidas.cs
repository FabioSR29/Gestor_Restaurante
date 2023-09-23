using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestorDeRestaurante.Model
{
    public class Medidas
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "El campo nombre es requerido")]
        public string Nombre { get; set; }
    }
}
