using System.Collections.Generic;

namespace Datos.Interfaces
{
    public interface ICrudGenerico<T> where T : class
    {
        bool Registrar(T entidad);

        bool Actualizar(T entidad);

        bool Eliminar(string id);

        List<T> ObtenerTodos();
    }
}
