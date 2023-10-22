namespace Entidades
{
    public class Liquidacion
    {
        public string IdFactura {  get; set; } 
        public int Año { get; set; }
        public int Mes {  get; set; }
        public decimal TotalSalario { get; set; }
        public decimal TotalSalud {  get; set; }
        public decimal TotalPension { get; set; }
        public decimal TotalAuxilioTransporte { get; set; }
        public decimal Total { get; set; }
    }
}
