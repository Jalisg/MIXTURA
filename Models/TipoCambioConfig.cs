namespace MIXTURAA.Models
{
    public class TipoCambioConfig
    {
        public static decimal TipoCambioBCCN { get; set; } = 36.62m; // Tasa oficial promedio Banco Central de Nicaragua
        public static string MonedaActual { get; set; } = "NIO"; // "NIO" o "USD"

        public static decimal Convertir(decimal montoEnUSD, string monedaDestino)
        {
            if (monedaDestino == "NIO")
            {
                return montoEnUSD * TipoCambioBCCN;
            }
            return montoEnUSD;
        }

        public static string ObtenerSimbolo(string moneda)
        {
            return moneda == "NIO" ? "C$" : "$";
        }
    }
}