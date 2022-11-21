using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography.Xml;
using System.Xml;

namespace YURI.CLOUD.TRANSVERSAL.COMMON
{
   
    public static class YCUtilities
    {

        public static void SetCultureInfo()
        {
            SetCultureInfo("es-EC");
        }

        public static void SetCultureInfo(string Name)
        {
            var forceDotCulture = new CultureInfo(Name);
            forceDotCulture.NumberFormat.NumberDecimalSeparator = ".";
            forceDotCulture.NumberFormat.CurrencyDecimalSeparator = ".";
            forceDotCulture.NumberFormat.NumberGroupSeparator = ",";
            forceDotCulture.NumberFormat.CurrencyGroupSeparator = ",";
            forceDotCulture.DateTimeFormat.ShortDatePattern = "dd/MM/yyyy";
            CultureInfo.DefaultThreadCurrentCulture = forceDotCulture;
            CultureInfo.DefaultThreadCurrentUICulture = forceDotCulture;
            System.Threading.Thread.CurrentThread.CurrentCulture = forceDotCulture;
            System.Threading.Thread.CurrentThread.CurrentUICulture = forceDotCulture;
        }

        public static object GetPropertyValues(Object obj, string nameProperty)
        {
            Type t = obj.GetType();
            PropertyInfo[] props = t.GetProperties();

            return props.FirstOrDefault(x => x.Name.Equals(nameProperty)).GetValue(obj);
        }

        public static T LeerAppSettings<T>(Type type, ref string mensaje, string nameFile = "appsettings.json")
        {
            string assemblyPath = Path.GetDirectoryName(type.Assembly.Location);
            var builder = new ConfigurationBuilder()
             .SetBasePath(assemblyPath)
             .AddJsonFile(nameFile)
             .Build();
            var result = builder.Get<T>();
            if (result == null) mensaje = "Archivo appsettings.json inválido";
            return result;
        }

        public static string GenerarClaveAleatorio(short numeroCaracteres)
        {
            char[] letras = new char[52];
            int n = 0;
            for (int item = 65; item <= 90; item++)
            {
                letras[n] = (char)item;
                letras[n + 1] = char.ToLower(letras[n]);
                n += 2;
            }
            string claveAleatoria = string.Empty;
            Random rnd = new Random(DateTime.Now.Millisecond);
            for (n = 0; n < numeroCaracteres; n++)
            {
                int numero = rnd.Next(0, 51);
                claveAleatoria += letras[numero];
            }
            return claveAleatoria;
        }

        // Función para quitar los saltos de línea de un texto 
        public static string QuitarSaltosLinea(string texto, string caracterReemplazar = " ")
        {
            if (texto != null)
            {
                if ((texto != ""))
                {
                    var str = texto.Replace(((char)10).ToString(), caracterReemplazar).Replace(((char)13).ToString(), caracterReemplazar);
                    str = str.Replace(Environment.NewLine, " ");
                    str = System.Text.RegularExpressions.Regex.Replace(str, " {2,}", " ");
                    return str;
                }
                else
                    return string.Empty;
            }
            else
                return texto;
        }

        public static int ContarDecimales(decimal valor)
        {
            int NumDecimales;
            int posicion;
            var cadena = Convert.ToString(valor);
            cadena = cadena.Replace(".", ",");
            posicion = cadena.IndexOf(",");
            if (posicion != -1)
            {
                cadena = cadena.Substring(posicion);
                cadena = cadena.Replace(",", "");
                NumDecimales = cadena.Length;
            }
            else
                NumDecimales = 0;
            return NumDecimales;
        }

        public static bool ValidarCertificadoDigital(byte[] byteCertificado, string claveCertificado, ref string mensaje)
        {
            try
            {
                using (X509Certificate2 cer = new X509Certificate2(byteCertificado, claveCertificado))
                {
                    var ExpirationDate = Convert.ToDateTime(cer.GetExpirationDateString());
                    if (ExpirationDate < DateTime.Now) throw new System.Exception("Certificado digital caducado");
                };
                return true;
            }
            catch (YCException ex)
            {
                mensaje = YCConversions.ExceptionToString(ex);
                return false;
            }
        }

        public static string CadenaConexion(string dataSource, string initialCatalog, string userId, string password, string cryptoKey, ref string mensaje, long TimeOut = 120)
        {
            try
            {
                if (string.IsNullOrEmpty(cryptoKey)) throw new System.Exception("cryptoKey is null");
                if (string.IsNullOrEmpty(dataSource)) throw new System.Exception("dataSource is null");
                if (string.IsNullOrEmpty(initialCatalog)) throw new System.Exception("initialCatalog is null");
                if (string.IsNullOrEmpty(userId)) throw new System.Exception("userId is null");
                if (string.IsNullOrEmpty(password)) throw new System.Exception("password is null");
                password =  YCCyrpto.DescifrarClave(password, cryptoKey);
                if (string.IsNullOrEmpty(password)) throw new System.Exception("password encrypt is invalid");

                return string.Format("Data Source={0};" +
                                        "Initial Catalog={1};" +
                                        "User ID={2};" +
                                        "Password={3};" +
                                        "Connection Timeout={4};" +
                                        "Persist Security Info=True;" +
                                        "trustServerCertificate=true;",
                                        dataSource, initialCatalog, userId, password, TimeOut);
            }
            catch (YCException ex)
            {
                mensaje = YCConversions.ExceptionToString(ex);
                return null;
            }
        }


        public static string CadenaConexion(string prefijo, string cryptoKey, ref string mensaje, long TimeOut = 120)
        {
            try
            {
                string campo = null;
                if (string.IsNullOrEmpty(cryptoKey)) throw new System.Exception("cryptoKey is null");
                campo = $"{prefijo}.dataSource"; var dataSource = System.Configuration.ConfigurationManager.AppSettings[campo];
                if (string.IsNullOrEmpty(dataSource)) throw new System.Exception($"Config: {campo} is null");
                campo = $"{prefijo}.initialCatalog"; var initialCatalog = System.Configuration.ConfigurationManager.AppSettings[campo];
                if (string.IsNullOrEmpty(initialCatalog)) throw new System.Exception($"Config: {campo} is null");
                campo = $"{prefijo}.userId"; var userId = System.Configuration.ConfigurationManager.AppSettings[campo];
                if (string.IsNullOrEmpty(userId)) throw new System.Exception($"Config: {campo} is null");
                campo = $"{prefijo}.password"; var password = System.Configuration.ConfigurationManager.AppSettings[campo];
                if (string.IsNullOrEmpty(password)) throw new System.Exception($"Config: {campo} is null");
                password = YCCyrpto.DescifrarClave(password, cryptoKey);
                if (string.IsNullOrEmpty(password)) throw new System.Exception($"Config: {campo} encrypt is invalid");

                return string.Format("Data Source={0};" +
                                        "Initial Catalog={1};" +
                                        "User ID={2};" +
                                        "Password={3};" +
                                        "Connection Timeout={4};" +
                                        "Persist Security Info=True;",
                                        dataSource, initialCatalog, userId, password, TimeOut);
            }
            catch (YCException ex)
            {
                mensaje = YCConversions.ExceptionToString(ex);
                return null;
            }
        }

        [DllImport("kernel32.dll")]
        private static extern bool SetProcessWorkingSetSize(IntPtr procHandle, int min, int max);

        // Funcion de liberacion de memoria
        public static bool ClearMemory()
        {
            try
            {
                if ((Process.GetCurrentProcess().WorkingSet64 / 1048576) > 150)
                {
                    GC.Collect();
                    GC.WaitForPendingFinalizers();
                    Process Mem;
                    Mem = Process.GetCurrentProcess();
                    SetProcessWorkingSetSize(Mem.Handle, -1, -1);
                    return true;
                }

            }
            catch (YCException)
            {
            }
            return false;
        }

        public static List<List<T>> SplitListIntoMultiple<T>(short NumlList, List<T> OriginalList)
        {
            if (OriginalList.Count < NumlList) NumlList = (short)OriginalList.Count;
            List<List<T>> _listCollection = new List<List<T>>();
            int size = (int)Math.Truncate((double)OriginalList.Count / NumlList);
            List<T> _list;
            for (int i = 1; i <= (NumlList - 1); i++)
            {
                _list = new List<T>();
                _list.AddRange(OriginalList.GetRange(size * (i - 1), size));
                _listCollection.Add(_list);
            };
            _list = new List<T>();
            _list.AddRange(OriginalList.GetRange(size * (NumlList - 1), OriginalList.Count - (size * (NumlList - 1))));
            _listCollection.Add(_list);
            return _listCollection;
        }

        public static bool ValidarFirmaXmlArchivo(XmlDocument XmlArchivoFirmado, ref string mensaje)
        {
            try
            {
                XmlArchivoFirmado.PreserveWhitespace = true;
                SignedXml signedXml = new SignedXml(XmlArchivoFirmado);
                XmlNodeList nodeList = XmlArchivoFirmado.GetElementsByTagName("Signature");
                XmlNodeList certificates = XmlArchivoFirmado.GetElementsByTagName("X509Certificate");
                if (nodeList.Count == 0) throw new System.Exception("Firma digital no encontrada en el archivo xml");
                if (certificates.Count == 0) throw new System.Exception("Certificado digital no encontrado en el archivo xml");
                X509Certificate2 dcert2 = new X509Certificate2(Convert.FromBase64String(certificates[0].InnerText));
                bool passes = false;
                foreach (XmlElement element in nodeList)
                {
                    signedXml.LoadXml(element);
                    passes = signedXml.CheckSignature(dcert2, true);
                }
                return passes;
            }
            catch (YCException ex)
            {
                mensaje = YCConversions.ExceptionToString(ex);
                return false;
            }
        }

        public static string HuellaCertificadoXmlFirmado(XmlDocument XmlArchivoFirmado, ref string mensaje)
        {
            try
            {
                XmlArchivoFirmado.PreserveWhitespace = true;
                SignedXml signedXml = new SignedXml(XmlArchivoFirmado);
                XmlNodeList nodeList = XmlArchivoFirmado.GetElementsByTagName("Signature");
                XmlNodeList certificates = XmlArchivoFirmado.GetElementsByTagName("X509Certificate");
                if (nodeList.Count == 0) throw new System.Exception("Firma digital no encontrada en el archivo xml");
                if (certificates.Count == 0) throw new System.Exception("Certificado digital no encontrado en el archivo xml");
                X509Certificate2 dcert2 = new X509Certificate2(Convert.FromBase64String(certificates[0].InnerText));
                return dcert2.Thumbprint;
            }
            catch (YCException ex)
            {
                mensaje = YCConversions.ExceptionToString(ex);
                return null;
            }
        }

        public static List<KeyValuePair<string, string>> OIDsCertificadoXmlFirmado(XmlDocument XmlArchivoFirmado, ref string mensaje)
        {
            try
            {
                XmlArchivoFirmado.PreserveWhitespace = true;
                SignedXml signedXml = new SignedXml(XmlArchivoFirmado);
                XmlNodeList nodeList = XmlArchivoFirmado.GetElementsByTagName("Signature");
                XmlNodeList certificates = XmlArchivoFirmado.GetElementsByTagName("X509Certificate");
                if (nodeList.Count == 0) throw new System.Exception("Firma digital no encontrada en el archivo xml");
                if (certificates.Count == 0) throw new System.Exception("Certificado digital no encontrado en el archivo xml");
                X509Certificate2 dcert2 = new X509Certificate2(Convert.FromBase64String(certificates[0].InnerText));
                return dcert2.Subject.Split(',').Select(x => new KeyValuePair<string, string>(x.Split('=')[0]?.Trim(), x.Split('=')[1].Trim())).ToList();
            }
            catch (YCException ex)
            {
                mensaje = YCConversions.ExceptionToString(ex);
                return null;
            }
        }

        public static string CorregirListaUrlApi(string value, bool QuitarSlash)
        {
            string _Url = null;
            try
            {
                foreach (var i in value?.Split(';'))
                {
                    var url = i;
                    if (i.EndsWith("/"))
                    {
                        if (QuitarSlash) url = i.Remove(i.Length - 1);
                    }
                    else
                    {
                        if (!QuitarSlash) url += "/";
                    }
                    _Url += url + ";";
                }
                _Url = _Url?.TrimEnd(';');
            }
            catch (YCException)
            {
            }
            return _Url;
        }

        public static int GetWorkingDays(DateTime firstDay, DateTime lastDay, HashSet<DateTime> bankHolidays, bool upperRange)
        {
            Func<int, bool> isWorkingDay = days =>
            {
                var currentDate = firstDay.AddDays(days);
                var isNonWorkingDay =
                    currentDate.DayOfWeek == DayOfWeek.Saturday ||
                    currentDate.DayOfWeek == DayOfWeek.Sunday ||
                    bankHolidays.Contains(currentDate.Date);
                return !isNonWorkingDay;
            };

            var day = 0;
            if (!upperRange) ++day;

            return Enumerable.Range(0, day + (lastDay - firstDay).Days).Count(isWorkingDay);
        }
    }

}
