namespace YURI.CLOUD.TEST.WEBAPP.ViewModels.Shared
{
    public class ArchivoGridViewModel
    {
        public string Nombre { get; set; }
        public string Extension { get; set; }
        public string Url { get; set; }
        public DateTime FechaRegistro { get; set; }
        public ArchivoGridViewModel()
        {
            Nombre = string.Empty;
            Url = string.Empty;
            Extension = string.Empty;
        }
    }
}
