using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestorDeRestaurante.Model
{
    public class Menu
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "El campo nombre es requerido")]
        public String Nombre { get; set; }
        [Display(Name = "Categoría")]
        public Categoria Categoria { get; set; }
        [Required(ErrorMessage = "El campo precio es requerido")]
        public double Precio { get; set; }
        [Display(Name = "Imágen")]
        [Required(ErrorMessage = "El campo imágen es requerido")]
        public byte[] Imagen { get; set; }
        [NotMapped]
        [Display(Name = "Ganancia aproximada")]
        public double? GananciaAproximada { get; set; }
        [NotMapped]
        public int Id_Orden { get; set; }

        [NotMapped]
        public byte[] ImagenVieja { get; set; }
        [Display(Name = "Imágen")]

        [NotMapped]
        public byte[]? ImagenNueva { get; set; }
    }
}
