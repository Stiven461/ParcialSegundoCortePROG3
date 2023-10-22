using Datos.Interfaces;
using Datos.Repositorios;
using Entidades;
using System.Collections.Generic;

namespace Negocio.Servicios
{
    public class ServicioProducto
    {
        private readonly RepositorioProducto repositorioProducto = new RepositorioProducto();

        public bool Actualizar(Producto entidad)
        {
            return this.repositorioProducto.Actualizar(entidad);
        }

        public bool Eliminar(string id)
        {
            return this.repositorioProducto.Eliminar(id);
        }

        public List<Producto> ObtenerTodos()
        {
            return this.repositorioProducto.ObtenerTodos(); 
        }

        public Producto ObtenerPorIdentificacion(string id)
        {
            return repositorioProducto.BuscarProductoPorIdentificacion(id);
        }

        public bool Registrar(Producto entidad)
        {
            return this.repositorioProducto.Registrar(entidad); 
        }
    }
}
