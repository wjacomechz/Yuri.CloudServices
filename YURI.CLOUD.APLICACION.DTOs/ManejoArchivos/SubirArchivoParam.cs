namespace YURI.CLOUD.APLICACION.DTOs.ManejoArchivos
{
    public class SubirArchivoParam
    {
        public long IdDirectorio { get; set; }
        public long IdCaja { get; set; }
        public string Nombre { get; set; }
        public string Formato { get; set; }
        public decimal Peso { get; set; }
        public string Unidad { get; set; }
        public string ArrayByteContenido { get; set; }
        public DateTime FechaRegistro { get; set; }
        public string Url { get; set; }
        public SubirArchivoParam()
        {
            Nombre = string.Empty;
            Formato = string.Empty;
            Unidad = string.Empty;
            ArrayByteContenido = string.Empty;
            Url = string.Empty;
        }
    }
}
