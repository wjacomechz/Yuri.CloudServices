using Microsoft.Extensions.Options;
using static System.Net.Mime.MediaTypeNames;
using System.Net;
using YURI.CLOUD.TRANSVERSAL.COMMON;
using Exception = System.Exception;

namespace YURI.CLOUD.TEST.WEBAPP.Models
{
    public class BaseModel
    {
        #region Atributos
        private bool _success;
        private bool _fatal;
        private string _mensaje;
        private string _codigo;
        private readonly IOptions<Settings.WebAppSettings> _settings;
        private List<string> _conetenedor;
        #endregion

        #region Propiedades
        public List<string> Conetenedor
        {
            get { return _conetenedor; }
            set { _conetenedor = value; }
        }
        public string Mensaje
        {
            get { return _mensaje; }
            set { _mensaje = value; }
        }
        public string Codigo
        {
            get { return _codigo; }
            set { _codigo = value; }
        }
        public bool Success
        {
            get { return _success; }
            set { _success = value; }
        }
        public bool Fatal
        {
            get { return _fatal; }
            set { _fatal = value; }
        }

        public IOptions<Settings.WebAppSettings> Settings
        {
            get { return _settings; }
        }
        #endregion

        #region Enumeraciones
        public enum LoggerTrx : short
        {
            SendAndRecibeAPI = 1,
            Trx = 2
        }
        public enum Direccion : short
        {
            Envia = 1,
            Recibe = 2
        }
        #endregion

        public BaseModel(IOptions<Settings.WebAppSettings> settings)
        {
            _success = false;
            _fatal = false;
            _mensaje = string.Empty;
            _settings = settings;
            _conetenedor = new List<string>();
        }

        /// <summary>
        /// Descripcion: Consume api restFull de tipo POST.
        /// </summary>
        /// <param name="url">URL.</param>
        /// <param name="body">JSON Request.</param>
        /// <param name="timeout">Tiempo de espera de la respuesta</param>
        /// <param name="excepcion">REF excepcion generada.</param>
        /// <returns>Restorna string con JSON de respuesta.</returns>
        public string ExecuteRESTPOST(string url, string body, int timeout, ref bool excepcion)
        {
            excepcion = false;
            try
            {
                var httpWebRequest = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(url);
                httpWebRequest.ContentType = "application/json";
                httpWebRequest.Method = "POST";
                httpWebRequest.Timeout = timeout;
                string result = string.Empty;
                using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                {
                    streamWriter.Write(body);
                    streamWriter.Flush();
                    streamWriter.Close();
                }
                var httpResponse = (System.Net.HttpWebResponse)httpWebRequest.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    result = streamReader.ReadToEnd();
                }
                return result;
            }
            catch (System.Net.WebException webex)
            {
                excepcion = true;
                System.Net.HttpWebResponse errorResponse = (System.Net.HttpWebResponse)webex.Response;
                string errorText = string.Empty;
                if (errorResponse != null)
                {
                    if (errorResponse.StatusCode == System.Net.HttpStatusCode.BadRequest)
                    {
                        using (StreamReader responseStream = new StreamReader(errorResponse.GetResponseStream()))
                        {
                            errorText = responseStream.ReadToEnd();
                            excepcion = false;
                            return errorText;
                        }
                    }
                    else
                    {
                        #region JSON ERROR DIFERENTE AL 400(BadRequest)
                        errorText = "Error diferente al BadRequest: " + webex.Message;
                        string code = "0999";// DenialCode.ERROR_NO_DEFINIDO;
                        string strjson = "{\"exception\":{\"errors\":\"" + errorText + "\",\"code\":\"" + code + "\"}}";
                        return strjson;
                        #endregion
                    }
                }
                else
                {
                    errorText = webex.Message;
                    string code = "0999";// (webex.Status == System.Net.WebExceptionStatus.Timeout) ? Recursos.DenialCodeAplicacion.HOST_TIMEDOUT : Recursos.DenialCodeAplicacion.HOST_ERROR_COMUNICACION;
                    string strjson = "{\"exception\":{\"errors\":\"" + errorText + "\",\"code\":\"" + code + "\"}}";
                    return strjson;
                }
            }
            catch (Exception ex)
            {
                excepcion = true;
                string errorText = ex.Message;
                string code = "0999";// Recursos.DenialCodeAplicacion.ERROR_NO_DEFINIDO;
                string strjson = "{\"exception\":{\"errors\":\"" + errorText + "\",\"code\":\"" + code + "\"}}";
                return strjson;
            }
        }

        public string ExecuteRESTPOST(string url, string cookie, string data, int timeout, ref bool excepcion)
        {
            excepcion = false;
            try
            {
                HttpWebRequest httpRequest = (HttpWebRequest)WebRequest.Create(url);
                httpRequest.Method = "POST";
                httpRequest.ContentType = "application/x-www-form-urlencoded";
                if (!string.IsNullOrEmpty(cookie))
                    httpRequest.Headers.Add("Cookie", cookie);

                // Convert the post string to a byte array
                byte[] bytedata = System.Text.Encoding.UTF8.GetBytes(data);
                httpRequest.ContentLength = bytedata.Length;

                // Create the stream
                Stream requestStream = httpRequest.GetRequestStream();
                requestStream.Write(bytedata, 0, bytedata.Length);
                requestStream.Close();

                // Get the response from remote server
                HttpWebResponse httpWebResponse = (HttpWebResponse)httpRequest.GetResponse();
                Stream responseStream = httpWebResponse.GetResponseStream();

                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                using (StreamReader reader = new StreamReader(responseStream, System.Text.Encoding.UTF8))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        sb.Append(line);
                    }
                }

                string serverResponse = sb.ToString();
                return serverResponse;

            }
            catch (System.Net.WebException webex)
            {
                excepcion = true;
                System.Net.HttpWebResponse errorResponse = (System.Net.HttpWebResponse)webex.Response;
                string errorText = string.Empty;
                if (errorResponse != null)
                {
                    if (errorResponse.StatusCode == System.Net.HttpStatusCode.BadRequest)
                    {
                        using (StreamReader responseStream = new StreamReader(errorResponse.GetResponseStream()))
                        {
                            errorText = responseStream.ReadToEnd();
                            excepcion = false;
                            return errorText;
                        }
                    }
                    else
                    {
                        #region JSON ERROR DIFERENTE AL 400(BadRequest)
                        errorText = "Error diferente al BadRequest: " + webex.Message;
                        string code = "0999";// DenialCode.ERROR_NO_DEFINIDO;
                        string strjson = "{\"exception\":{\"errors\":\"" + errorText + "\",\"code\":\"" + code + "\"}}";
                        return strjson;
                        #endregion
                    }
                }
                else
                {
                    errorText = webex.Message;
                    string code = "0999";// (webex.Status == System.Net.WebExceptionStatus.Timeout) ? Recursos.DenialCodeAplicacion.HOST_TIMEDOUT : Recursos.DenialCodeAplicacion.HOST_ERROR_COMUNICACION;
                    string strjson = "{\"exception\":{\"errors\":\"" + errorText + "\",\"code\":\"" + code + "\"}}";
                    return strjson;
                }
            }
            catch (Exception ex)
            {
                excepcion = true;
                string errorText = ex.Message;
                string code = "0999";// Recursos.DenialCodeAplicacion.ERROR_NO_DEFINIDO;
                string strjson = "{\"exception\":{\"errors\":\"" + errorText + "\",\"code\":\"" + code + "\"}}";
                return strjson;
            }
        }

        public string GetUrlApi(string apirouter)
        {
            string url = this.Settings.Value.UrlApi.Protocolo + "://" + this.Settings.Value.UrlApi.Server + ":" + this.Settings.Value.UrlApi.Puerto + apirouter;
            return url;
        }

        public void LoggerExcepciones(string interfaz, string tipo_excepcion, string controlador, string accion, string mensaje_error, string stack_trace, string usuario, string accessToken, string mensaje_devuelto)
        {
            try
            {
                DateTime fecha = DateTime.Now;
                string formato_origen_error = @"{0}\{1}";
                bool flagex = false;
                string url = "url log";// this.GetUrlApi(this.Settings.Value.UrlApi.Apis.Audit.RegistrarExcepcion);
                //string body = Newtonsoft.Json.JsonConvert.SerializeObject(new APLICATION.DTOs.RqtLoggerExAppDto()
                //{
                //    AccessToken = accessToken,
                //    Alias = usuario,
                //    Interface = string.Format(formato_origen_error, controlador, accion),
                //    TipoError = tipo_excepcion,
                //    MensajeDevuelto = mensaje_devuelto,
                //    MensajeError = mensaje_error,
                //    StackTrace = stack_trace,
                //    OrigenError = interfaz,
                //    Aplicacion = this.Settings.Value.CodAplicacion,
                //    Fecha = new DateTime(fecha.Year, fecha.Month, fecha.Day),
                //    Hora = (short)fecha.Hour,
                //    Minuto = (short)fecha.Minute,
                //    Segundo = (short)fecha.Second,
                //    Milesima = (short)fecha.Millisecond
                //});
                string body = "error";
                string jsonResult = "OK";//this.ExecuteRESTPOST(url, body, this.Settings.Value.UrlApi.Apis.Audit.TimeOut, ref flagex);
            }
            catch (Exception ex)
            {
                this.Success = false;
                this.Fatal = true;
                this.Mensaje = "Ocurrió un error al tratar de registrar log excepciones";
            }
        }

        public void TmpLoggerFileLog(string direccion, string interfaz, string texto)
        {
            try
            {
                string directorio = "C:\\Almxpressex\\Logs\\";
                string aplicacion = "webapp";
                if (!Directory.Exists(@directorio))
                    Directory.CreateDirectory(@directorio);
                string archivo_log = directorio + "\\" + aplicacion + "_" + DateTime.Now.ToString("yyyyMMdd") + ".txt";
                StreamWriter sb = new StreamWriter(@archivo_log, true);
                sb.WriteLine("[" + DateTime.Now.ToString("HH:mm:ss") + "][" + direccion + "][" + interfaz + "]" + texto);
                sb.Close();
            }
            catch
            { }
        }

        public string FillLineaLog(string hora, string id, Direccion direccion, string pathurl, string param, string response)
        {
            string linea = string.Empty;
            if (direccion == Direccion.Envia)
                linea = this.Settings.Value.LOG.FormatoSendAPI.Replace("hora", hora).Replace("ID", id).Replace("pathurl", pathurl).Replace("param", param);
            else if (direccion == Direccion.Recibe)
                linea = this.Settings.Value.LOG.FormatoReciveAPI.Replace("hora", hora).Replace("ID", id).Replace("response", response);
            return linea;
        }

        public void LoggerTransaccion(string interfaz, LoggerTrx tipo)
        {
            try
            {
                if (tipo == LoggerTrx.SendAndRecibeAPI && this.Settings.Value.LOG.HabilitarSendReciveAPI)
                {
                    foreach (string item in this.Conetenedor)
                    {
                        string directorio = this.Settings.Value.LOG.Ruta;
                        string aplicacion = this.Settings.Value.LOG.FileNameSendAndRecive;
                        if (!Directory.Exists(@directorio))
                            Directory.CreateDirectory(@directorio);
                        string archivo_log = directorio + "\\" + aplicacion + "_" + DateTime.Now.ToString("yyyyMMdd") + ".txt";
                        StreamWriter sb = new StreamWriter(@archivo_log, true);
                        sb.WriteLine(item);
                        sb.Close();
                    }
                }
            }
            catch
            { }
        }


        

    }
}
