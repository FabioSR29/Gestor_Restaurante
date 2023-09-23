using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestorDeRestaurante.Model
{
    public class MenuIngredientes
    {
        public int Id { get; set; }

        public int Id_Menu { get; set; }

        public int Id_Ingredientes { get; set; }

        [Required(ErrorMessage = "El campo cantidad es requerido")]
        public double Cantidad { get; set; }

        public int Id_Medidas { get; set; }

        [Display(Name = "Valor aproximado")]
        [Required(ErrorMessage = "El campo Valor aproximado es requerido")]
        public int ValorAproximado { get; set; }

        [NotMapped]
        public string? Nombre { get; set; }

        [NotMapped]
        [Display(Name = "Medida")]
        public string? NombreDeLaMedida { get; set; }

    }
}
