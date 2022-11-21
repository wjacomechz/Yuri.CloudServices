using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using NuGet.Configuration;
using System.Globalization;
using System.Resources;
using YURI.CLOUD.TEST.WEBAPP.Models;
using YURI.CLOUD.APLICACION.DTOs.ManejoArchivos;

namespace YURI.CLOUD.TEST.WEBAPP.Controllers
{
    public class FileManagementController : BaseController
    {
        private readonly IOptions<Settings.WebAppSettings> _settings;
        private readonly IHttpContextAccessor _accessor;

        public FileManagementController(IOptions<Settings.WebAppSettings> settings, IHttpContextAccessor accessor)
        {
            this._settings = settings;
            this._accessor = accessor;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult SubirArchivo(IFormFile? formFile, string Directorio)
        {
            if (!string.IsNullOrEmpty(Directorio))
            {
                string? actionName = this.ControllerContext.RouteData.Values["action"]?.ToString();
                string? controllerName = this.ControllerContext.RouteData.Values["controller"]?.ToString();
                if (true)//model.isvalid
                {
                    var httpcontext = _accessor.HttpContext;
                    string s_sesionViewModel = "cxcc";
                    if (!string.IsNullOrEmpty(s_sesionViewModel))
                    {
                        bool success = false;
                        string mensaje = string.Empty;
                        try
                        {
                            string nombArchivo_usr = string.Empty, nombimg_usr_base = string.Empty, strcontenido_archivo = string.Empty;
                            if (formFile != null)
                            {
                                string[] retorno_valimg = this.ValidarImg(Resources.DefaultParameters.Uploaded_ImgUsr, formFile).Split('|');
                                if (retorno_valimg[0].Equals("0000"))
                                {
                                    nombimg_usr_base = retorno_valimg[1];
                                    nombArchivo_usr = Path.GetFileNameWithoutExtension(formFile.FileName)+"_" +  Guid.NewGuid().ToString().Substring(0, 15) + Path.GetExtension(formFile.FileName).ToLower();
                                    using (var ms = new MemoryStream())
                                    {
                                        formFile.CopyTo(ms);
                                        var fileBytes = ms.ToArray();
                                        strcontenido_archivo = Convert.ToBase64String(fileBytes);
                                    }
                                    var rutaS3 = $"files_users/wjacomechz/{nombArchivo_usr}";
                                    ArchivoModel archivo_model = new ArchivoModel(this._settings);
                                    success = archivo_model.SubirArchivo(new SubirArchivoParam()
                                    {
                                        ArrayByteContenido = strcontenido_archivo,
                                        FechaRegistro = DateTime.Now,
                                        Nombre = nombArchivo_usr,
                                        Formato = Path.GetExtension(formFile.FileName).ToLower(),
                                        Url = rutaS3
                                    }, controllerName, actionName);
                                    mensaje = archivo_model.Mensaje;
                                }
                                else
                                {
                                    success = false;
                                    mensaje = retorno_valimg[1];
                                }
                            }
                            else
                            {
                                success = false;
                                mensaje = "No ha cargado el archivo...";
                            }
                        }
                        catch (IOException IOEX)
                        {
                            success = false;
                            mensaje = "Error técnico: " + IOEX.Message;
                        }
                        catch (Exception EX)
                        {
                            success = false;
                            mensaje = "Error técnico: " + EX.Message;
                        }
                        return Json(new { success = success, mensaje = mensaje, filtro = "ALL" });
                    }
                    else
                        return Json(new { success = false, mensaje = Resources.DefaultMenssage.DATOINVALIDO_0001 });
                }
                else
                    return Json(new { success = false, mensaje = Resources.DefaultMenssage.CrossSiteRequestForgery });
            }
            else
                return Json(new { success = false, mensaje = Resources.DefaultMenssage.DATOINVALIDO_0028 });
        }


        private string ValidarImg(string codaplicacion, IFormFile file)
        {
            string retorno = string.Empty;
            if (file != null)
            {
                if (Resources.DefaultParameters.ResourceManager.GetString("Uploaded_" + codaplicacion) != null)
                {
                    if (Path.GetExtension(file.FileName).ToLower().Equals(".png") || Path.GetExtension(file.FileName).ToLower().Equals(".jpg") || Path.GetExtension(file.FileName).ToLower().Equals(".pdf") || Path.GetExtension(file.FileName).ToLower().Equals(".jpeg"))
                        retorno = "0000|" + file.FileName;
                    else
                        retorno = "1907|Se permite imágenes en formato jpg ; png ; pdf"; ;
                }
                else
                    retorno = "1907|Cod. Aplicación para validar imágenes es incorrecto.";
            }
            else
                retorno = "1907|Archivo imagen es invalido.";
            return retorno;
        }

    }
}
