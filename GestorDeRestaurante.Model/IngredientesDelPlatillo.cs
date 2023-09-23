using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestorDeRestaurante.Model
{
    public class IngredientesDelPlatillo
    {
        [Display(Name = "Nombre del ingrediente")]
        public  string NombreDelIngrediente { get; set; }
        public double Cantidad { get; set; }
        [Display(Name = "Nombre de la medida")]
        public string NombreDeLaMedida { get; set; }
        public int ValorAproximado { get; set; }
        public int Id { get; set; }
        public string Nombre { get; set; }
    }
}
