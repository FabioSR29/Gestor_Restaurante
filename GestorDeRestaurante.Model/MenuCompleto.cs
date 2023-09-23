using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestorDeRestaurante.Model
{
    public class MenuCompleto
    {
        public List<Menu>? Entradas { get; set; }

        public List<Menu>? PequeñasBotanas { get; set; }
        public List<Menu>? Aperitivos { get; set; }
        public List<Menu>? SopasYEnsaladas { get; set; }
        public List<Menu>? PlatosPrincipales { get; set; }
        public List<Menu>? Postres { get; set; }
        public List<Menu>? Bebidas { get; set; }

    }
}
