using GestorDeRestaurante.Model;

namespace GestorDeRestaurante.BS
{
    public class RepositorioDelRestaurante : IRepositorioDelRestaurante
    {
        private GestorDeRestaurante.DA.DbContexto ElContextoBD;

        public RepositorioDelRestaurante(GestorDeRestaurante.DA.DbContexto contexto)
        {
            ElContextoBD = contexto;
        }

        public List<Ingredientes> ObtengaLaListaDeIngredientes()
        {
            var resultado = from c in ElContextoBD.Ingredientes
                            select c;
            return resultado.ToList();
        }

        public void AgregueIngredientes(Ingredientes elIngrediente)
        {

            ElContextoBD.Ingredientes.Add(elIngrediente);
            ElContextoBD.SaveChanges();
        }

        public void EditarIngredientes(Ingredientes elIngrediente)
        {
            Model.Ingredientes elIngredienteAModificar;

            elIngredienteAModificar = ObtenerIngredientePorId(elIngrediente.Id);

            elIngredienteAModificar.Nombre = elIngrediente.Nombre;

            ElContextoBD.Ingredientes.Update(elIngredienteAModificar);
            ElContextoBD.SaveChanges();
        }

        public Ingredientes ObtenerIngredientePorId(int Id)
        {
            Model.Ingredientes resultado;

            resultado = ElContextoBD.Ingredientes.Find(Id);

            return resultado;
        }
        public List<IngredientesDelPlatillo> ObtengaLaListaDePlatillosPorIngrediente(int id_Ingredientes)
        {
            var query = from c in ElContextoBD.MenuIngredientes
                        where c.Id_Ingredientes == id_Ingredientes
                        select c;

            List<MenuIngredientes> ingredientes = query.ToList();

            List<IngredientesDelPlatillo> ingredientDePlatillo = new List<IngredientesDelPlatillo>();

            foreach (MenuIngredientes item in ingredientes)
            {
                IngredientesDelPlatillo losingredientesDelPlatillo = new IngredientesDelPlatillo();

                losingredientesDelPlatillo.Id = item.Id;

                losingredientesDelPlatillo.NombreDeLaMedida = ObtenerPorIdLaMedida(item.Id_Medidas).Nombre;

                losingredientesDelPlatillo.NombreDelIngrediente = ObtenerIngredientePorId(item.Id_Ingredientes).Nombre;

                losingredientesDelPlatillo.Nombre =ObtenerPlatillosPorId( item.Id_Menu).Nombre;
                losingredientesDelPlatillo.Cantidad = item.Cantidad;

                losingredientesDelPlatillo.ValorAproximado = item.ValorAproximado;

                ingredientDePlatillo.Add(losingredientesDelPlatillo);
            }

            return ingredientDePlatillo;
        }

            //----------------------------------------------------------------------------------------------------------------------------------------

            public List<Medidas> ObtengaLaListaDeMedidas()
        {
            var resultado = from c in ElContextoBD.Medidas
                            select c;
            return resultado.ToList();
        }

        public Medidas ObTengalaMedida(string Nombre)
        {
            Model.Medidas resultado = null;
            List<Model.Medidas> laLista;

            laLista = ObtengaLaListaDeMedidas();

            foreach (Model.Medidas item in laLista)
            {
                if (item.Nombre == Nombre)
                    resultado = item;
            }

            return resultado;
        }

        public List<Medidas> ObTengaLasMedidasPorNombre(string nombre)
        {
            List<Model.Medidas> laLista;
            List<Model.Medidas> laListaFiltrada;

            laLista = ObtengaLaListaDeMedidas();

            laListaFiltrada = laLista.Where(x => x.Nombre.Contains(nombre)).ToList();
            return laListaFiltrada;
        }

        public Medidas ObtenerPorIdLaMedida(int Id)
        {
            Model.Medidas resultado;

            resultado = ElContextoBD.Medidas.Find(Id);

            return resultado;
        } 

        public void AgregueLaMedida(Medidas medida)
        {
            ElContextoBD.Medidas.Add(medida);
            ElContextoBD.SaveChanges();
        }

        public void EditarLaMedida(Medidas medida)
        {
            Model.Medidas laMedidaAModificar;

            laMedidaAModificar = ObtenerPorIdLaMedida(medida.Id);

            laMedidaAModificar.Nombre = medida.Nombre;

            ElContextoBD.Medidas.Update(laMedidaAModificar);
            ElContextoBD.SaveChanges();
        }

        //----------------------------------------------------------------------------------------------------------------------------------------

        public List<Mesas> ObtengaLaListaDeMesas()
        {
            var resultado = from c in ElContextoBD.Mesas
                            select c;
            return resultado.ToList();
        }

        public void AgregueLasMesas(Mesas lasMesas)
        {
            lasMesas.Estado = Estado.Disponible;
            ElContextoBD.Mesas.Add(lasMesas);
            ElContextoBD.SaveChanges();
        }

        public void EditarLasMesas(Mesas lasMesas)
        {
            Model.Mesas laMesaAModificar;

            laMesaAModificar = ObtenerMesasPorId(lasMesas.Id);

            laMesaAModificar.Nombre = lasMesas.Nombre;

            ElContextoBD.Mesas.Update(laMesaAModificar);
            ElContextoBD.SaveChanges();
        }

        public Mesas ObtenerMesasPorId(int Id)
        {
            Model.Mesas resultado;

            resultado = ElContextoBD.Mesas.Find(Id);

            return resultado;
        }

        //----------------------------------------------------------------------------------------------------------------------------------------

        public List<Menu> ObtengaLaListaDePlatillos()
        {
            var resultado = from c in ElContextoBD.Menu
                            select c;
            return resultado.ToList();
        }

        public void AgregueElPlatillo(Menu elPlatillo)
        {
            ElContextoBD.Menu.Add(elPlatillo);
            ElContextoBD.SaveChanges();
        }

        public MenuCompleto ObtengaElMenuCompleto()
        {
            List<Model.Menu> LaListaDePlatillos;
            LaListaDePlatillos = ObtengaLaListaDePlatillos();

            Model.MenuCompleto ElMenuCompleto = new Model.MenuCompleto();

            ElMenuCompleto.Entradas = ObtengaLasEntradas();
            ElMenuCompleto.PlatosPrincipales = ObtengaPlatosPrincipales();
            ElMenuCompleto.Postres = ObtengaLosPostres();
            ElMenuCompleto.PequeñasBotanas = ObtengaLasPequenasBotanas();
            ElMenuCompleto.Aperitivos = ObtengaLosAperitivos();
            ElMenuCompleto.Bebidas = ObtengasLasBebidas();
            ElMenuCompleto.SopasYEnsaladas = ObtengaLasSopas();


            return ElMenuCompleto;
        }

        public Menu ObtenerPlatillosPorId(int? Id)
        {
            Model.Menu resultado;

            resultado = ElContextoBD.Menu.Find(Id);

            return resultado;
        }

        public void EditarLosPlatilos(Model.Menu? ElPlatillo)
        {
            Model.Menu elPlatilloaModificar;

            elPlatilloaModificar = ObtenerPlatillosPorId(ElPlatillo.Id);

            elPlatilloaModificar.Nombre = ElPlatillo.Nombre;

            elPlatilloaModificar.Precio = ElPlatillo.Precio;
            elPlatilloaModificar.Categoria = ElPlatillo.Categoria;
            elPlatilloaModificar.Imagen = ElPlatillo.Imagen;

            ElContextoBD.Menu.Update(elPlatilloaModificar);
            ElContextoBD.SaveChanges();
        }

        public List<Menu> ObtengaLasEntradas()
        {
            List<Model.Menu>? LaListaDePlatillos;
            List<Model.Menu>? LaListaDeEntradas = new List<Model.Menu>();
            LaListaDePlatillos = ObtengaLaListaDePlatillos();

            LaListaDeEntradas = LaListaDePlatillos.Where(x => x.Categoria.ToString().Equals("Entrada")).ToList();
            return LaListaDeEntradas;
        }
        public List<Menu> ObtengaPlatosPrincipales()
        {
            List<Model.Menu>? LaListaDePlatillos;
            List<Model.Menu>? LaListaDeEntradas = new List<Model.Menu>();
            LaListaDePlatillos = ObtengaLaListaDePlatillos();

            LaListaDeEntradas = LaListaDePlatillos.Where(x => x.Categoria.ToString().Equals("PlatosPrincipales")).ToList();
            return LaListaDeEntradas;
        }
        public List<Menu> ObtengaLosPostres()
        {
            List<Model.Menu>? LaListaDePlatillos;
            List<Model.Menu>? LaListaDeEntradas = new List<Model.Menu>();
            LaListaDePlatillos = ObtengaLaListaDePlatillos();

            LaListaDeEntradas = LaListaDePlatillos.Where(x => x.Categoria.ToString().Equals("Postres")).ToList();
            return LaListaDeEntradas;
        }
        public List<Menu> ObtengaLasPequenasBotanas()
        {
            List<Model.Menu>? LaListaDePlatillos;
            List<Model.Menu>? LaListaDeEntradas = new List<Model.Menu>();
            LaListaDePlatillos = ObtengaLaListaDePlatillos();

            LaListaDeEntradas = LaListaDePlatillos.Where(x => x.Categoria.ToString().Equals("PequeñasBotanas")).ToList();
            return LaListaDeEntradas;
        }
        public List<Menu> ObtengasLasBebidas()
        {
            List<Model.Menu>? LaListaDePlatillos;
            List<Model.Menu>? LaListaDeEntradas = new List<Model.Menu>();
            LaListaDePlatillos = ObtengaLaListaDePlatillos();

            LaListaDeEntradas = LaListaDePlatillos.Where(x => x.Categoria.ToString().Equals("Bebidas")).ToList();
            return LaListaDeEntradas;
        }
        public List<Menu> ObtengaLosAperitivos()
        {
            List<Model.Menu>? LaListaDePlatillos;
            List<Model.Menu>? LaListaDeEntradas = new List<Model.Menu>();
            LaListaDePlatillos = ObtengaLaListaDePlatillos();

            LaListaDeEntradas = LaListaDePlatillos.Where(x => x.Categoria.ToString().Equals("Aperitivos")).ToList();
            return LaListaDeEntradas;
        }
        public List<Menu> ObtengaLasSopas()
        {
            List<Model.Menu>? LaListaDePlatillos;
            List<Model.Menu>? LaListaDeEntradas = new List<Model.Menu>();
            LaListaDePlatillos = ObtengaLaListaDePlatillos();

            LaListaDeEntradas = LaListaDePlatillos.Where(x => x.Categoria.ToString().Equals("SopasYEnsaladas")).ToList();
            return LaListaDeEntradas;
        }
        //----------------------------------------------------------------------------------------------------------------------------------------
        public List<Menu> ObtengaLaListaDelMenuParaIngredientes(List<Menu> losPlatillos, List<MenuIngredientes> elMenuDeIngredientes)
        {
            List<Model.Menu> elResultado = new List<Model.Menu>();

            foreach (Model.Menu platillo in losPlatillos)
            {
                Model.Menu elPlatilloParaIngredientes = new Model.Menu();

                elPlatilloParaIngredientes.Id = platillo.Id;
                elPlatilloParaIngredientes.Nombre = platillo.Nombre;
                elPlatilloParaIngredientes.Precio = platillo.Precio;

                elPlatilloParaIngredientes.GananciaAproximada = ObtengaLaGananciaAproximada(elMenuDeIngredientes, platillo);

                elResultado.Add(elPlatilloParaIngredientes);
            }

            return elResultado;
        }

        public List<MenuIngredientes> ObtengaLaListaDeIngredientesDeUnPlatilloPorId(int id)
        {
            List<Model.MenuIngredientes> elResultado;

            List<Model.MenuIngredientes> laLista;

            laLista = ObtengaLaListaDeIngredientesDelMenuPorId(id);

            elResultado = ObtengaElResultadoDeLosIngredientesDeUnPlatillo(laLista);

            return elResultado;
        }

        public List<MenuIngredientes> ObtengaElMenuDeIngredientes()
        {
            var resultado = from c in ElContextoBD.MenuIngredientes
                            select c;
            return resultado.ToList();
        }

        public void AgregueElIngredienteAsociado(MenuIngredientes elIngredienteAsociado)
        {
            ElContextoBD.MenuIngredientes.Add(elIngredienteAsociado);
            ElContextoBD.SaveChanges();
        }

        public MenuIngredientes ObtenerIngredienteAsociadoPorId(int Id)
        {
            Model.MenuIngredientes resultado;

            resultado = ElContextoBD.MenuIngredientes.Find(Id);

            return resultado;
        }

        public void DesasociarUnIngrediente(MenuIngredientes elIngrediente)
        {
            ElContextoBD.MenuIngredientes.Remove(elIngrediente);
            ElContextoBD.SaveChanges();
        }

        private double? ObtengaLaGananciaAproximada(List<MenuIngredientes> elMenuDeIngredientes, Model.Menu platillo)
        {
            double? elResultado = new double?();
            double sumaValoresAproximadosDeLosIngredientes = 0;
            foreach (Model.MenuIngredientes item in elMenuDeIngredientes)
            {
                if (item.Id_Menu == platillo.Id)
                    sumaValoresAproximadosDeLosIngredientes += item.ValorAproximado;
            }
            if (sumaValoresAproximadosDeLosIngredientes > 0)
                elResultado = platillo.Precio - sumaValoresAproximadosDeLosIngredientes;
            else
                elResultado = platillo.Precio + sumaValoresAproximadosDeLosIngredientes;

            return elResultado;
        }

        private List<MenuIngredientes> ObtengaElResultadoDeLosIngredientesDeUnPlatillo(List<MenuIngredientes> laLista)
        {
            List<Model.MenuIngredientes> elResultado = new List<Model.MenuIngredientes>();
            List<Model.Ingredientes> losIngredientes;
            List<Model.Medidas> lasMedidas;

            losIngredientes = ObtengaLaListaDeIngredientes();
            lasMedidas = ObtengaLaListaDeMedidas();

            elResultado = ObtengaLasFilasDeLosIngredientesDeUnPlatillo(laLista, losIngredientes, lasMedidas);

            return elResultado;

        }

        private List<MenuIngredientes> ObtengaLasFilasDeLosIngredientesDeUnPlatillo(List<MenuIngredientes> laLista, List<Ingredientes> losIngredientes, List<Medidas> lasMedidas)
        {
            List<Model.MenuIngredientes> elResultado = new List<Model.MenuIngredientes>();

            foreach (MenuIngredientes item in laLista)
            {
                Model.MenuIngredientes elIngrediente = new Model.MenuIngredientes();

                elIngrediente.Id = item.Id;
                elIngrediente.Id_Menu = item.Id_Menu;
                elIngrediente.Id_Ingredientes = item.Id_Ingredientes;
                elIngrediente.Cantidad = item.Cantidad;
                elIngrediente.Id_Medidas = item.Id_Medidas;
                elIngrediente.ValorAproximado = item.ValorAproximado;

                elIngrediente.Nombre = ObtengaElNombreDelIngrediente(losIngredientes, item.Id_Ingredientes);
                elIngrediente.NombreDeLaMedida = ObtengaElNombreDeLaMedida(lasMedidas, item.Id_Medidas);

                elResultado.Add(elIngrediente);
            }

            return elResultado;
        }


        private string ObtengaElNombreDeLaMedida(List<Medidas> lasMedidas, int id_Medidas)
        {
            string elResultado = "";

            foreach (var item in lasMedidas)
            {
                if (item.Id == id_Medidas)
                {
                    elResultado = item.Nombre;
                    break;
                }
            }
            return elResultado;
        }

        private string ObtengaElNombreDelIngrediente(List<Model.Ingredientes> losIngredientes, int id_Ingredientes)
        {
            string elResultado = "";

            foreach (var item in losIngredientes)
            {
                if (item.Id == id_Ingredientes)
                {
                    elResultado = item.Nombre;
                    break;
                }
            }
            return elResultado;
        }

        private List<MenuIngredientes> ObtengaLaListaDeIngredientesDelMenuPorId(int id)
        {
            List<MenuIngredientes> elResultado = new List<MenuIngredientes>();
            List<MenuIngredientes> elMenuDeIngredientesAuxiliar;

            var elMenuDeIngredientes = from c in ElContextoBD.MenuIngredientes
                                       select c;

            elMenuDeIngredientesAuxiliar = elMenuDeIngredientes.ToList();

            foreach (var item in elMenuDeIngredientesAuxiliar)
            {
                if (item.Id_Menu == id)
                {
                    elResultado.Add(item);
                }
            }
            return elResultado;
        }

        //----------------------------------------------------------------------------------------------------------------------------------------

        public List<MesaOrden> ObtengaLaListaDeOrdenes()
        {
            var resultado = from c in ElContextoBD.MesaOrden
                            select c;
            return resultado.ToList();
        }

        public MesasOrdenes ObtengaLasMesasDeOrdenes()
        {
            Model.MesasOrdenes lasMesasDeOrdenes = new Model.MesasOrdenes();

            lasMesasDeOrdenes.MesasNumero1 = ObtengaLasListaDeMesasNumero1();
            lasMesasDeOrdenes.MesasNumero2 = ObtengaLaListaDeMesasNumero2();

            return lasMesasDeOrdenes;
        }

        public MesaOrden ObtenerUnaOrden()
        {
            Model.MesaOrden lasMesasDeOrdenes = new Model.MesaOrden();

            lasMesasDeOrdenes.lasMesas = ObtengaLaListaDeMesas();
            lasMesasDeOrdenes.losPlatillos = ObtengaLaListaDePlatillos();
            return lasMesasDeOrdenes;
        }

        public void AgregarUnaOrden(MesaOrden lasOrden)
        {
            lasOrden.Estado = Model.EstadoDeOrdenes.Solicitadas;
            ElContextoBD.MesaOrden.Add(lasOrden);
            ElContextoBD.SaveChanges();
        }

        public List<Menu> ObtengaLosPlatillosAsociadosAUnaMesa(int id)
        {
            List<Model.MesaOrden> laListaDeOrdenesAsociadas;

            var elResultado = from c in ElContextoBD.MesaOrden
                              join x in ElContextoBD.Menu on c.Id_Menu equals x.Id
                              where c.Id_Mesa == id && c.Estado == EstadoDeOrdenes.Solicitadas
                              select x;


            var lasOrdenes = from c in ElContextoBD.MesaOrden
                             join x in ElContextoBD.Menu on c.Id_Menu equals x.Id
                             where c.Id_Mesa == id && c.Estado == EstadoDeOrdenes.Solicitadas
                             select c;

            laListaDeOrdenesAsociadas = lasOrdenes.ToList();


            int contador = 0;
            foreach (var item in elResultado.ToList())
            {
                item.Id_Orden = laListaDeOrdenesAsociadas[contador].Id;
                contador++;
            }

            return elResultado.ToList();
        }




        public void CambieEstadoDeOrden(MesaOrden laOrden)
        {
            Model.MesaOrden laOrdenAModificar;

            laOrdenAModificar = ObtengaLaOrden(laOrden);

            laOrdenAModificar.Estado = EstadoDeOrdenes.Servidas;
            ElContextoBD.MesaOrden.Update(laOrdenAModificar);
            ElContextoBD.SaveChanges();
        }

        public List<Mesas> ObtengaLasListaDeMesasNumero1()
        {
            List<Model.Mesas>? LaListaDeMesas;
            List<Model.Mesas>? LalistaDeMesasFiltrada = new List<Model.Mesas>();
            LaListaDeMesas = ObtengaLaListaDeMesas();

            for (int i = 0; i <= (LaListaDeMesas.Count() - 1); i++)
            {
                if (ValideSiMesaEstaAsociadaAUnaOrden(LaListaDeMesas[i].Id) == true)
                {

                    LaListaDeMesas[i].estadoDeLaMesa = EstadoDeLaMesa.Ocupada;
                    LalistaDeMesasFiltrada.Add(LaListaDeMesas[i]);

                }
                else
                {
                    LalistaDeMesasFiltrada.Add(LaListaDeMesas[i]);
                }
                i++;
            }
            return LalistaDeMesasFiltrada;
        }

        public List<Mesas> ObtengaLaListaDeMesasNumero2()
        {
            List<Model.Mesas>? LaListaDeMesas;
            List<Model.Mesas>? LaListaDeMesasFiltradas = new List<Model.Mesas>();
            LaListaDeMesas = ObtengaLaListaDeMesas();

            for (int i = 1; i <= (LaListaDeMesas.Count() - 1); i++)
            {

                if (ValideSiMesaEstaAsociadaAUnaOrden(LaListaDeMesas[i].Id) == true)
                {
                    LaListaDeMesas[i].estadoDeLaMesa = EstadoDeLaMesa.Ocupada;
                    LaListaDeMesasFiltradas.Add(LaListaDeMesas[i]);
                }
                else
                {
                    LaListaDeMesasFiltradas.Add(LaListaDeMesas[i]);
                }
                i++;
            }
            return LaListaDeMesasFiltradas;
        }

        public Boolean ValideSiMesaEstaAsociadaAUnaOrden(int Id)
        {
            var resultado = from c in ElContextoBD.MesaOrden
                            where c.Id_Mesa == Id
                            select c;
            if (resultado.Count() != 0)
            {
                return true;
            }

            return false;
        }


        public MesaOrden ObtengaLaOrden(Model.MesaOrden laOrden)
        {
            Model.MesaOrden laOrdenAModificar;

            var resultado = from c in ElContextoBD.MesaOrden
                            where c.Id_Mesa == laOrden.Id_Mesa && c.Id_Menu == laOrden.Id_Menu && c.Id == laOrden.Id
                            select c;

            laOrdenAModificar = ElContextoBD.MesaOrden.Find(resultado.First().Id);

            return laOrdenAModificar;
        }
    }
}