namespace YURI.CLOUD.APLICACION.DTOs.ManejoArchivos
{
    public sealed class BuscarArchivoParam
    {
        public string Contenedor { get; set; }
        public string Key { get; set; }
        public BuscarArchivoParam()
        {
            Contenedor = string.Empty;
            Key = string.Empty;
        }
    }
}
