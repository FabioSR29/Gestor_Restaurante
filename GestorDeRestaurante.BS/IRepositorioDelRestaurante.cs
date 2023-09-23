using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestorDeRestaurante.BS
{
    public interface IRepositorioDelRestaurante
    {
        List<Model.Ingredientes> ObtengaLaListaDeIngredientes();
        void AgregueIngredientes(Model.Ingredientes elIngrediente);
        void EditarIngredientes(Model.Ingredientes elIngrediente);
        Model.Ingredientes ObtenerIngredientePorId(int Id);
        List<Model.IngredientesDelPlatillo> ObtengaLaListaDePlatillosPorIngrediente(int idIngredientes);

        List<Model.Medidas> ObtengaLaListaDeMedidas();
        Model.Medidas ObTengalaMedida(string Nombre);
        List<Model.Medidas> ObTengaLasMedidasPorNombre(string nombre);
        Model.Medidas ObtenerPorIdLaMedida(int Id);
        void AgregueLaMedida(Model.Medidas medida);
        void EditarLaMedida(Model.Medidas medida);


        List<Model.Mesas> ObtengaLaListaDeMesas();
        void AgregueLasMesas(Model.Mesas lasMesas);
        void EditarLasMesas(Model.Mesas lasMesas);
        Model.Mesas ObtenerMesasPorId(int Id);


        List<Model.Menu> ObtengaLaListaDePlatillos();
        void AgregueElPlatillo(Model.Menu elPlatillo);
        Model.MenuCompleto ObtengaElMenuCompleto();
        Model.Menu ObtenerPlatillosPorId(int? Id);
        void EditarLosPlatilos(Model.Menu ElPlatillo);
        


        List<Model.Menu> ObtengaLaListaDelMenuParaIngredientes(List<Model.Menu> losPlatillos, List<Model.MenuIngredientes> elMenuDeIngredientes);
        List<Model.MenuIngredientes> ObtengaLaListaDeIngredientesDeUnPlatilloPorId(int id);
        List<Model.MenuIngredientes> ObtengaElMenuDeIngredientes();
        void AgregueElIngredienteAsociado(Model.MenuIngredientes elIngredienteAsociado);
        Model.MenuIngredientes ObtenerIngredienteAsociadoPorId(int id);
        void DesasociarUnIngrediente(Model.MenuIngredientes elIngrediente);


        List<Model.MesaOrden> ObtengaLaListaDeOrdenes();
        Model.MesasOrdenes ObtengaLasMesasDeOrdenes();
        Model.MesaOrden ObtenerUnaOrden();
        void AgregarUnaOrden(Model.MesaOrden laOrden);
        List<Model.Menu> ObtengaLosPlatillosAsociadosAUnaMesa(int id);
        void CambieEstadoDeOrden(Model.MesaOrden laOrden);
    }
}
