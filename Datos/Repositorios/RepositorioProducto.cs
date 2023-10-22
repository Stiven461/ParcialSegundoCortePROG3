using Datos.Interfaces;
using Entidades;
using Entidades.Enums;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace Datos.Repositorios
{
    public class RepositorioProducto : ICrudGenerico<Producto>
    {
        private string archivoProductos = "productos.txt";

        public RepositorioProducto()
        {
            if (!File.Exists(archivoProductos))
            {
                File.Create(archivoProductos).Close();
            }
        }

        public bool Registrar(Producto producto)
        {
            try
            {
                if (string.IsNullOrEmpty(producto.Identificacion) || string.IsNullOrEmpty(producto.Nombre) || producto.Salario == 0)
                {
                    MessageBox.Show("La identificación, el nombre y el salario son campos obligatorios.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

                if (!EsNombreValido(producto.Nombre))
                {
                    MessageBox.Show("El nombre solo puede contener letras.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

                if (!Enum.IsDefined(typeof(EstadoProducto), producto.Estado))
                {
                    MessageBox.Show("El estado debe ser 'Activo' o 'Inactivo'.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

                if (BuscarProductoPorIdentificacion(producto.Identificacion) == null)
                {
                    using (StreamWriter sw = File.AppendText(archivoProductos))
                    {
                        string linea = $"{producto.Identificacion},{producto.Nombre},{producto.Salario},{producto.Estado}";
                        sw.WriteLine(linea);
                        MessageBox.Show("Producto registrado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return true;
                    }
                }
                else
                {
                    MessageBox.Show("El producto ya existe.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (IOException e)
            {
                MessageBox.Show($"Error al registrar producto: {e.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error inesperado: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            try
            {
                var lineas = File.ReadAllLines(archivoProductos);
                bool productoEncontrado = false;

                for (int i = 0; i < lineas.Length; i++)
                {
                    var campos = lineas[i].Split(',');
                    if (campos.Length == 4 && campos[0] == entidad.Identificacion)
                    {
                        lineas[i] = $"{entidad.Identificacion},{entidad.Nombre},{entidad.Salario},{entidad.Estado}";
                        productoEncontrado = true;
                    }
                }

                if (productoEncontrado)
                {
                    File.WriteAllLines(archivoProductos, lineas);
                    return true;
                }
                else
                {
                    Console.WriteLine("Producto no encontrado.");
                    return false;
                }
            }
            catch (IOException e)
            {
                Console.WriteLine($"Error al actualizar producto: {e.Message}");
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error inesperado: {ex.Message}");
                return false;
            }
        }

        public bool Eliminar(string id)
        {
            try
            {
                var lineas = File.ReadAllLines(archivoProductos);
                bool productoEncontrado = false;

                for (int i = 0; i < lineas.Length; i++)
                {
                    var campos = lineas[i].Split(',');
                    if (campos.Length == 4 && campos[0] == id)
                    {
                        lineas[i] = null;
                        productoEncontrado = true;
                    }
                }

                if (productoEncontrado)
                {
                    File.WriteAllLines(archivoProductos, lineas);
                    return true;
                }
                else
                {
                    Console.WriteLine("Producto no encontrado.");
                    return false;
                }
            }
            catch (IOException e)
            {
                Console.WriteLine($"Error al eliminar producto: {e.Message}");
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error inesperado: {ex.Message}");
                return false;
            }
        }
    }
}
