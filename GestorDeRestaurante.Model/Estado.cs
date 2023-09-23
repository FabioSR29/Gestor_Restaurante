using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestorDeRestaurante.Model
{
    public enum Estado
    {
        Disponible = 1,
        [Display(Name = "No disponible")]
        NoDisponible = 2
    }
}
