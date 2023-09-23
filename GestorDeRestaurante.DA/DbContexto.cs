using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestorDeRestaurante.DA
{
    public class DbContexto: DbContext
    {
        public DbContexto(DbContextOptions<DbContexto> opciones) : base(opciones)
        {

        }
        public DbSet<GestorDeRestaurante.Model.Ingredientes> Ingredientes { get; set; }

        public DbSet<GestorDeRestaurante.Model.Medidas>  Medidas { get; set; }

        public DbSet<GestorDeRestaurante.Model.Menu> Menu { get; set; }

        //    public DbSet<GestorDeRestaurante.Model.MenuIngredientes> MenuIngredientes { get; set; }

        //     public DbSet<GestorDeRestaurante.Model.MesaOrden> MesaOrden { get; set; }

        public DbSet<GestorDeRestaurante.Model.Mesas> Mesas { get; set; }

        public DbSet<GestorDeRestaurante.Model.MesaOrden> MesaOrden { get; set; }

        public DbSet<GestorDeRestaurante.Model.MenuIngredientes> MenuIngredientes { get; set; }



    }
}

