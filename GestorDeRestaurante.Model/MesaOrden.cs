using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestorDeRestaurante.Model
{
    public  class MesaOrden
    {
        public int Id { get; set; }

        public int Id_Mesa { get; set; }

        public int Id_Menu { get; set; }
        [RegularExpression("(^[0-9]+$)", ErrorMessage = "Solo se permiten números ")]
        [Required(ErrorMessage = "Este campo es requerido")]
        public int Cantidad { get; set; }
        [NotMapped]
        public List<Mesas>? lasMesas { get; set; }
        [NotMapped]

        public List<Menu>? losPlatillos { get; set; }

        public EstadoDeOrdenes Estado { get; set; }

  

    }
}
