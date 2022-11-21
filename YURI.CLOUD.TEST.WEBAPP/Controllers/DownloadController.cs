using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using YURI.CLOUD.APLICACION.DTOs.ManejoArchivos;
using YURI.CLOUD.TEST.WEBAPP.Models;

namespace YURI.CLOUD.TEST.WEBAPP.Controllers
{
    public class DownloadController : BaseController
    {
        private readonly IOptions<Settings.WebAppSettings> _settings;
        private readonly IHttpContextAccessor _accessor;

        public DownloadController(IOptions<Settings.WebAppSettings> settings, IHttpContextAccessor accessor)
        {
            this._settings = settings;
            this._accessor = accessor;
        }

        public FileResult DescargarArchivo()
        {
            string? actionName = this.ControllerContext.RouteData.Values["action"]?.ToString();
            string? controllerName = this.ControllerContext.RouteData.Values["controller"]?.ToString();
            var httpcontext = _accessor.HttpContext;
            string? s_sesionViewModel = "pendiente";// httpcontext.Session.GetString("SesionViewModel");
            if (!string.IsNullOrEmpty(s_sesionViewModel))
            {
                ArchivoModel model_archivo = new ArchivoModel(_settings);
                string urlremota = httpcontext.Request.Form["UrlS3"];
                string nombArchivo = httpcontext.Request.Form["NombreArchivo"];
                string formatoArchivo = httpcontext.Request.Form["ExtensionArchivo"];
                if (model_archivo.BuscarArchivo(new BuscarArchivoParam() {
                    Key = urlremota
                }, controllerName, actionName))
                {
                    byte[] result = model_archivo.Archivo.ContenidoArchivo;
                    var stream = new MemoryStream(result);
                    return File(stream, "application/" + formatoArchivo.Replace(".", ""), nombArchivo + formatoArchivo);
                }
                else
                {
                    string text = model_archivo.Mensaje;
                    byte[] result = System.Text.Encoding.ASCII.GetBytes(text);
                    var stream = new MemoryStream(result);
                    return File(stream, "text/plain", "stream.txt");
                }
            }
            else
            {
                string text = "Sesion terminada, vuelva a realizar el login";
                byte[] result = System.Text.Encoding.ASCII.GetBytes(text);
                var stream = new MemoryStream(result);
                return File(stream, "text/plain", "stream.txt");
            }
        }
    }
}
