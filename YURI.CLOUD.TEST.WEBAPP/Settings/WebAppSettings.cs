namespace YURI.CLOUD.TEST.WEBAPP.Settings
{
    public class WebAppSettings
    {
        public string CodAplicacion { get; set; }
        public LogSettings LOG { get; set; }
        public URLAPI UrlApi { get; set; }

        public WebAppSettings()
        {
            UrlApi = new URLAPI();
            CodAplicacion = string.Empty;
            LOG = new LogSettings();
        }
    }

    public sealed class LogSettings
    {
        public bool HabilitarSendReciveAPI { get; set; }
        public short DestinoSendReciveAPI { get; set; }
        public string FormatoSendAPI { get; set; }
        public string FormatoReciveAPI { get; set; }
        public string Ruta { get; set; }
        public string FileNameSendAndRecive { get; set; }

        public LogSettings()
        {
            FormatoReciveAPI = string.Empty;
            FormatoSendAPI = string.Empty;
            Ruta = string.Empty;
            FileNameSendAndRecive = string.Empty;
        }
    }

    public class URLAPI
    {
        public string Protocolo { get; set; }
        public string Server { get; set; }
        public string Puerto { get; set; }
        public APIs Apis { get; set; }

        public URLAPI()
        {
            Protocolo = string.Empty;
            Server = string.Empty;
            Puerto = string.Empty;
            Apis = new APIs();
        }
    }

    public class APIs
    {
        public FileManagementAPI FileManagement { get; set; }
        public APIs()
        {
            FileManagement = new FileManagementAPI();
        }
    }

    public class FileManagementAPI
    {
        public int TimeOut { get; set; }
        public string SubirArchivo { get; set; }
        public string ListaArchivos { get; set; }
        public string BuscarArchivo { get; set; }

        public FileManagementAPI()
        {
            SubirArchivo = string.Empty;
            ListaArchivos = string.Empty;
            BuscarArchivo = string.Empty;
        }
    }
}
