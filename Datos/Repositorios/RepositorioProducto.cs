using Datos.Interfaces;
using Entidades;
using Entidades.Enums;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Datos.Repositorios
{
    public class RepositorioProducto : ICrudGenerico<Producto>
    {
        private string archivoProductos = "productos.txt";

        public bool Registrar(Producto producto)
        {
            try
            {
                if (string.IsNullOrEmpty(producto.Identificacion) || string.IsNullOrEmpty(producto.Nombre) || producto.Salario == 0)
                {
                    Console.WriteLine("La identificación, el nombre y el salario son campos obligatorios.");
                    return false;
                }

                if (!EsNombreValido(producto.Nombre))
                {
                    Console.WriteLine("El nombre solo puede contener letras.");
                    return false;
                }

                if (!Enum.IsDefined(typeof(EstadoProducto), producto.Estado))
                {
                    Console.WriteLine("El estado debe ser 'Activo' o 'Inactivo'.");
                    return false;
                }

                if (BuscarProductoPorIdentificacion(producto.Identificacion) == null)
                {
                    using (StreamWriter sw = File.AppendText(archivoProductos))
                    {
                        string linea = $"{producto.Identificacion},{producto.Nombre},{producto.Salario},{producto.Estado}";
                        sw.WriteLine(linea);
                        return true;
                    }
                }
                else
                {
                    Console.WriteLine("El producto ya existe.");
                }
            }
            catch (IOException e)
            {
                Console.WriteLine($"Error al registrar producto: {e.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error inesperado: {ex.Message}");
            }

            return false;
        }

        public List<Producto> ObtenerTodos()
        {
            List<Producto> productos = new List<Producto>();

            try
            {
                string[] lineas = File.ReadAllLines(archivoProductos);
                foreach (var linea in lineas)
                {
                    var campos = linea.Split(',');
                    if (campos.Length == 4)
                    {
                        if (Enum.TryParse(campos[3], out EstadoProducto estado))
                        {
                            productos.Add(new Producto
                            {
                                Identificacion = campos[0],
                                Nombre = campos[1],
                                Salario = decimal.Parse(campos[2]),
                                Estado = estado
                            });
                        }
                        else
                        {
                            Console.WriteLine($"Error al obtener productos: Estado no válido - {campos[3]}");
                        }
                    }
                }
            }
            catch (IOException e)
            {
                Console.WriteLine($"Error al obtener productos: {e.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error inesperado: {ex.Message}");
            }

            return productos;
        }

        public Producto BuscarProductoPorIdentificacion(string identificacion)
        {
            var lineas = File.ReadAllLines(archivoProductos);

            foreach (var linea in lineas)
            {
                var campos = linea.Split(',');
                if (campos.Length == 4 && campos[0] == identificacion)
                {
                    if (Enum.TryParse(campos[3], out EstadoProducto estado))
                    {
                        return new Producto
                        {
                            Identificacion = campos[0],
                            Nombre = campos[1],
                            Salario = decimal.Parse(campos[2]),
                            Estado = estado
                        };
                    }
                    else
                    {
                        Console.WriteLine($"Error al buscar producto: Estado no válido - {campos[3]}");
                    }
                }
            }

            return null;
        }

        private bool EsNombreValido(string nombre)
        {
            return !string.IsNullOrEmpty(nombre) && nombre.All(char.IsLetter);
        }

        public bool Actualizar(Producto entidad)
        {
            throw new NotImplementedException();
        }

        public bool Eliminar(string id)
        {
            throw new NotImplementedException();
        }
    }
}
