namespace Entidades
{
    public class DetalleLiquidacion
    {
        public int IdDetalleLiquidacion { get; set; }
        public int IdLiquidación { get; set; }
        public int Año { get; set; }
        public int Mes {  get; set; }
        public string Identificacion { get; set; }
        public string Nombre { get; set; }
        public decimal Salario { get; set; }
        public decimal Salud { get; set; }
        public decimal Pension { get; set; }
        public decimal AuxilioTransporte { get; set; }
        public decimal Devengado { get; set; } 
    }
}
    