using Entidades.Enums;
using Entidades;
using Negocio.Servicios;
using System;
using System.Windows.Forms;

namespace Presentacion.Formularios
{
    public partial class ControlProductosForm : Form
    {
        private ServicioProducto productoService = new ServicioProducto();

        public ControlProductosForm()
        {
            InitializeComponent();
            CargarProductos();
            CargarEstados();
        }

        private void CargarProductos()
        {
            dataGridView1.DataSource = productoService.ObtenerTodos();
        }

        private void CargarEstados()
        {
            comboBoxEstado.DataSource = Enum.GetValues(typeof(EstadoProducto));
        }

        private void registrar(object sender, EventArgs e)
        {
            string identificacion = textBoxIdentificacion.Text;
            string nombre = textBoxNombre.Text;
            decimal salario;
            if (decimal.TryParse(textBoxSalario.Text, out salario))
            {
                if (comboBoxEstado.SelectedItem != null)
                {
                    EstadoProducto estado = (EstadoProducto)comboBoxEstado.SelectedItem;
                    Producto nuevoProducto = new Producto
                    {
                        Identificacion = identificacion,
                        Nombre = nombre,
                        Salario = salario,
                        Estado = estado
                    };
                    if (productoService.Registrar(nuevoProducto))
                    {
                        MessageBox.Show("Producto registrado correctamente.");
                        CargarProductos();
                    }
                    else
                    {
                        MessageBox.Show("Error al registrar el producto.");
                    }
                }
                else
                {
                    MessageBox.Show("Seleccione un estado válido.");
                }
            }
            else
            {
                MessageBox.Show("Ingrese un salario válido.");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            registrar(sender, e);
        }

        private void buttonEliminar_Click(object sender, EventArgs e)
        {
            string identificacion = textBoxIdentificacion.Text;

            if (productoService.Eliminar(identificacion))
            {
                MessageBox.Show("Producto eliminado correctamente.");
                CargarProductos();
            }
            else
            {
                MessageBox.Show("Error al eliminar el producto o el producto no existe.");
            }
        }
    }
}
