using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using YURI.CLOUD.TEST.WEBAPP.Settings;
using YURI.CLOUD.APLICACION.DTOs.Common;
using YURI.CLOUD.APLICACION.DTOs.ManejoArchivos;
using YURI.CLOUD.DOMINIO.POCOEntidades;

namespace YURI.CLOUD.TEST.WEBAPP.Models
{
    public class ArchivoModel : BaseModel
    {
        public List<Archivo> _archivos;
        public BuscarArchivoResult _archivo;

        #region Propiedades
        public List<Archivo> Archivos
        {
            get { return _archivos; }
            set { _archivos = value; }
        }

        public BuscarArchivoResult Archivo
        {
            get { return _archivo; }
            set { _archivo = value; }
        }

        #endregion

        public ArchivoModel(IOptions<WebAppSettings> settings) : base(settings)
        {
            _archivos = new List<Archivo>();
            _archivo = new BuscarArchivoResult();
        }

        public bool SubirArchivo(SubirArchivoParam parametros, string controller, string action)
        {
            bool result = false;
            try
            {
                bool flagex = false;
                string url = this.GetUrlApi(this.Settings.Value.UrlApi.Apis.FileManagement.SubirArchivo);
                string body = JsonConvert.SerializeObject(parametros);
                string jsonResult = this.ExecuteRESTPOST(url, body, this.Settings.Value.UrlApi.Apis.FileManagement.TimeOut, ref flagex);
                if (!string.IsNullOrEmpty(jsonResult))
                {
                    if (flagex)
                    {
                        this.Success = false;
                        this.Mensaje = Resources.DefaultMenssage.ExcepcionSrvREST;
                    }
                    else
                    {
                        AppResult? outObject = JsonConvert.DeserializeObject<AppResult>(jsonResult);
                        if (outObject != null && outObject.CodigoRetorno.Equals("0000"))
                        {
                            result = this.Success = true;
                            this.Mensaje = outObject.MensajeRetorno;
                        }
                        else
                        {
                            result = this.Success = false;
                            this.Mensaje = outObject.MensajeRetorno;
                        }
                    }
                }
                else
                {
                    result = this.Success = false;
                    this.Mensaje = Resources.DefaultMenssage.JsonResult_IsNullOrEmpty;
                }
            }
            catch (Exception ex)
            {
                result = this.Success = false;
                this.Fatal = true;
                this.Mensaje = "Ocurrió un error al enviar a guardar la compra del cliente.";
                this.LoggerExcepciones("GuardarImportacionTracking", "Exception", controller, action, ex.Message, ex.StackTrace, "_handshake.Alias", string.Empty, this.Mensaje);
            }
            finally
            {
                this.LoggerTransaccion("GuardarImportacionTracking", LoggerTrx.SendAndRecibeAPI);
            }
            return result;
        }

        public bool ConsultarArchivos(ListaArchivoParam parametros, string controller, string action)
        {
            bool result = false;
            try
            {
                bool flagex = false;
                string url = this.GetUrlApi(this.Settings.Value.UrlApi.Apis.FileManagement.ListaArchivos);
                string body = JsonConvert.SerializeObject(parametros);
                string jsonResult = this.ExecuteRESTPOST(url, body, this.Settings.Value.UrlApi.Apis.FileManagement.TimeOut, ref flagex);
                if (!string.IsNullOrEmpty(jsonResult))
                {
                    if (flagex)
                    {
                        this.Success = false;
                        this.Mensaje = Resources.DefaultMenssage.ExcepcionSrvREST;
                    }
                    else
                    {
                        ListaArchivosResult? outObject = JsonConvert.DeserializeObject<ListaArchivosResult>(jsonResult);
                        if (outObject != null && outObject.CodigoRetorno.Equals("0000"))
                        {
                            result = this.Success = true;
                            this.Mensaje = outObject.MensajeRetorno;
                            this.Archivos = outObject.Data;
                        }
                        else
                        {
                            result = this.Success = false;
                            this.Mensaje = outObject.MensajeRetorno;
                        }
                    }
                }
                else
                {
                    result = this.Success = false;
                    this.Mensaje = Resources.DefaultMenssage.JsonResult_IsNullOrEmpty;
                }
            }
            catch (Exception ex)
            {
                result = this.Success = false;
                this.Fatal = true;
                this.Mensaje = "Ocurrió un error al enviar a guardar la compra del cliente.";
                this.LoggerExcepciones("GuardarImportacionTracking", "Exception", controller, action, ex.Message, ex.StackTrace, "_handshake.Alias", string.Empty, this.Mensaje);
            }
            finally
            {
                this.LoggerTransaccion("GuardarImportacionTracking", LoggerTrx.SendAndRecibeAPI);
            }
            return result;
        }

        public bool BuscarArchivo(BuscarArchivoParam parametros, string controller, string action)
        {
            bool result = false;
            try
            {
                bool flagex = false;
                string url = this.GetUrlApi(this.Settings.Value.UrlApi.Apis.FileManagement.BuscarArchivo);
                string body = JsonConvert.SerializeObject(parametros);
                string jsonResult = this.ExecuteRESTPOST(url, body, this.Settings.Value.UrlApi.Apis.FileManagement.TimeOut, ref flagex); 
                if (!string.IsNullOrEmpty(jsonResult))
                {
                    if (flagex)
                    {
                        this.Success = false;
                        this.Mensaje = Resources.DefaultMenssage.ExcepcionSrvREST;
                    }
                    else
                    {
                        BuscarArchivoResult? outObject = JsonConvert.DeserializeObject<BuscarArchivoResult>(jsonResult);
                        if (outObject != null && outObject.CodigoRetorno.Equals("0000"))
                        {
                            result = this.Success = true;
                            this.Mensaje = outObject.MensajeRetorno;
                            this.Archivo = outObject;
                        }
                        else
                        {
                            result = this.Success = false;
                            this.Mensaje = outObject.MensajeRetorno;
                        }
                    }
                }
                else
                {
                    result = this.Success = false;
                    this.Mensaje = Resources.DefaultMenssage.JsonResult_IsNullOrEmpty;
                }
            }
            catch (Exception ex)
            {
                result = this.Success = false;
                this.Fatal = true;
                this.Mensaje = "Ocurrió un error al tratar de obtener la informacion del archivo.";
                this.LoggerExcepciones("BuscarArchivoAXRemoto", "Exception", controller, action, ex.Message, ex.StackTrace, "_handshake.Alias", string.Empty, this.Mensaje);
            }
            finally
            {
                this.LoggerTransaccion("BuscarArchivoAXRemoto", LoggerTrx.SendAndRecibeAPI);
            }
            return result;
        }
    }
}
