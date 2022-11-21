namespace YURI.CLOUD.APLICACION.DTOs.ManejoArchivos
{
    public sealed class BuscarArchivoResult : Common.AppResult
    {
        public string Cliente { get; set; }
        public string Repositorio { get; set; }
        public string TipoRepositorio { get; set; }
        public byte[] ContenidoArchivo { get; set; }
        public BuscarArchivoResult()
        {
            Cliente = string.Empty;
            Repositorio = string.Empty;
            TipoRepositorio = string.Empty;
            ContenidoArchivo = new byte[0];
        }
    }
}
