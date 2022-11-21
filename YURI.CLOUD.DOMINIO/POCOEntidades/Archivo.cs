namespace YURI.CLOUD.DOMINIO.POCOEntidades
{
    public sealed class Archivo
    {
        public string Nombre { get; set; }
        public string Extension { get; set; }
        public string Url { get; set; }
        public DateTime FechaRegistro { get; set; } 

        public Archivo()
        {
            Nombre = string.Empty;
            Extension = string.Empty;
            Url = string.Empty;
        }
    }
}
