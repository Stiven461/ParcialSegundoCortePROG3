using Entidades.Enums;

namespace Entidades
{
    public class Producto
    {
        public string Identificacion { get; set; }
        public string Nombre { get; set;}
        public decimal Salario {  get; set; }
        public EstadoProducto Estado {  get; set; } 
    }
}
