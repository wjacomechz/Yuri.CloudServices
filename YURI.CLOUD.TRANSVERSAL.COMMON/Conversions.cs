using System.ComponentModel;
using System.Reflection;
using System.Text;
using System.Xml.Serialization;
using System.Xml;
using Newtonsoft.Json;

namespace YURI.CLOUD.TRANSVERSAL.COMMON
{
    public static class Conversions
    {
        public static DateTime DBNullToDate(object Value)
        {
            if (Convert.IsDBNull(Value))
            {
                return DateTime.Parse("0001-01-01");
            }
            else if (Value is null)
            {
                return DateTime.Parse("0001-01-01");
            }
            else
            {
                return Convert.ToDateTime(Value);
            }
        }

        public static string DBNullToString(object Value)
        {
            if (Convert.IsDBNull(Value))
            {
                return string.Empty;
            }
            else if (Value is null)
            {
                return string.Empty;
            }
            else
            {
                return Value.ToString();
            }
        }

        public static object DBNullToNothing(object Value)
        {
            if (Convert.IsDBNull(Value))
            {
                return null;
            }
            else if (Value is null)
            {
                return null;
            }
            else
            {
                return Value;
            }
        }

        public static string DBNullDateToString(object Value, string format)
        {
            if (Convert.IsDBNull(Value))
            {
                return string.Empty;
            }
            else if (Value is null)
            {
                return string.Empty;
            }
            else
            {
                try
                {
                    var dtime = Convert.ToDateTime(Value);
                    return dtime.ToString(format);
                }
                catch (Exception)
                {
                    return string.Empty;
                }
            }
        }

        public static int DBNullToInt32(object Value)
        {
            if (Convert.IsDBNull(Value))
            {
                return 0;
            }
            else if (Value is null)
            {
                return 0;
            }
            else if (IsNumeric(Value))
            {
                return Convert.ToInt32(Value);
            }
            else
            {
                return 0;
            }
        }

        public static short DBNullToInt16(object Value)
        {
            if (Convert.IsDBNull(Value))
            {
                return 0;
            }
            else if (Value is null)
            {
                return 0;
            }
            else if (IsNumeric(Value))
            {
                return Convert.ToInt16(Value);
            }
            else
            {
                return 0;
            }
        }

        public static byte DBNullToByte(object Value)
        {
            if (Convert.IsDBNull(Value))
            {
                return 0;
            }
            else if (Value is null)
            {
                return 0;
            }
            else if (IsNumeric(Value))
            {
                return Convert.ToByte(Value);
            }
            else
            {
                return 0;
            }
        }

        public static string DecimalToFixedString(decimal Value)
        {
            var str = string.Format("{0:0.00}", Value);
            return str.Replace(",", string.Empty).Replace(".", string.Empty);
        }

        public static decimal FixedStringToDecimal(string Value)
        {
            if (string.IsNullOrEmpty(Value)) return 0;
            var size = Value.Length;
            string strEnteros = "0";
            string strDecimales;
            if (size > 2)
            {
                strEnteros = Value.Substring(0, size - 2);
                strDecimales = Value.Substring(size - 2);
            }
            else
                strDecimales = Value;
            var enteros = Convert.ToDecimal(strEnteros);
            var decimales = Convert.ToDecimal(strDecimales);
            decimales *= (decimal)0.01;
            return enteros + decimales;
        }

        public static bool DBNullToBool(object Value)
        {
            try
            {
                if (Convert.IsDBNull(Value))
                {
                    return false;
                }
                else if (Value is null)
                {
                    return false;
                }
                return Convert.ToBoolean(Convert.ToInt16(Value));
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static double DBNullToDouble(object Value)
        {
            if (Convert.IsDBNull(Value))
            {
                return 0;
            }
            else if (Value is null)
            {
                return 0;
            }
            else if (IsNumeric(Value))
            {
                return Convert.ToDouble(Value);
            }
            else
            {
                return 0;
            }
        }

        public static decimal DBNullToDecimal(object Value)
        {
            if (Convert.IsDBNull(Value))
            {
                return 0;
            }
            else if (Value is null)
            {
                return 0;
            }
            else if (IsNumeric(Value))
            {
                return Convert.ToDecimal(Value);
            }
            else
            {
                return 0;
            }
        }

        public static DateTime StringToDate(string Value)
        {
            if (IsDateTime(Value))
            {
                return Convert.ToDateTime(Value);
            }
            else
            {
                return Convert.ToDateTime("2000-01-01");
            }
        }

        public static DateTime StringToDate(string Value, string formato)
        {
            DateTime dt;
            bool result = DateTime.TryParseExact(Value, formato, null, System.Globalization.DateTimeStyles.None, out dt);
            if (result)
                return dt;
            else
                return Convert.ToDateTime("1900-01-01");
        }

        public static decimal StringToDecimal(string Value)
        {
            decimal convertDecimal = decimal.Parse(Value, System.Globalization.NumberStyles.AllowDecimalPoint, System.Globalization.CultureInfo.InvariantCulture);
            return convertDecimal;
        }


        public static string DecimalToString(decimal Value)
        {
            var str = Value.ToString();
            return str;
        }


        public static string NothingToString(object Value)
        {
            if (Value is null)
            {
                return string.Empty;
            }
            else
            {
                return Convert.ToString(Value);
            }
        }

        public static object NothingToDBNULL(object Value)
        {
            try
            {
                if (Convert.IsDBNull(Value))
                {
                    return DBNull.Value;
                }
            }
            catch (Exception)
            {
            }
            if (Value is null)
                return DBNull.Value;
            else
                return Value;
        }

        public static object EmptyToNothing(object Value)
        {
            if (string.IsNullOrEmpty(Convert.ToString(Value)))
            {
                return null;
            }
            else
            {
                return Value;
            }
        }

        public static object NothingEmptyToDBNULL(object Value)
        {
            if (string.IsNullOrEmpty(Convert.ToString(Value)))
            {
                return DBNull.Value;
            }
            else
            {
                return Value;
            }
        }

        public static object DateNothingToDBNULL(object Value)
        {
            if (Convert.IsDBNull(Value))
            {
                return DBNull.Value;
            }

            if (Value is null)
            {
                return DBNull.Value;
            }
            else if (Convert.ToDateTime(Value) == DateTime.MinValue)
            {
                return DBNull.Value;
            }
            else
            {
                return Value;
            }
        }

        public static bool IsNumeric(object Value)
        {
            try
            {
                var resut = double.TryParse(Value?.ToString(), out double num);
                return resut;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static bool IsDateTime(object Value)
        {
            return Value is DateTime ? true : false;
        }

        public static bool IsValidDateTime(DateTime? fecha)
        {
            var fechaMinima = new DateTime(1901, 1, 1);
            if (fecha == null) return false;
            else if (fecha == default(DateTime)) return false;
            else if (fecha < fechaMinima) return false;
            else return true;
        }

        public static string ExceptionToString(System.Exception Value)
        {
            try
            {
                string msg = Value.Message;
                if (Value.InnerException != null)
                    msg = msg + "-" + ExceptionToString(Value.InnerException);
                msg = Utilities.QuitarSaltosLinea(msg, " ");
                return msg;
            }
            catch (Exception)
            {
            }
            return string.Empty;
        }

        public static string LongToStringZeroOnLeft(long Value, short Limit)
        {
            return StringToStringZeroOnLeft(Value.ToString(), Limit);
        }

        public static string StringToStringZeroOnLeft(string Value, short Limit)
        {
            var cadena = Value.ToString().PadLeft(Limit, Convert.ToChar("0"));
            if (cadena.Length > Limit)
                cadena = cadena.Substring(cadena.Length - Limit, Limit);
            return cadena;
        }

        public static string DecimalToDecimalForXML(decimal Value, int NumDecimales = 2)
        {
            string Formato = "##0.00";
            switch (NumDecimales)
            {
                case 1: { Formato = "##0.0"; break; }
                case 2: { Formato = "##0.00"; break; }
                case 3: { Formato = "##0.000"; break; }
                case 4: { Formato = "##0.0000"; break; }
                case 5: { Formato = "##0.00000"; break; }
                case 6: { Formato = "##0.000000"; break; }
            }
            return Value.ToString(Formato).Replace(",", ".");
        }

        public static string RealDecimalToDecimalForXML(decimal Value)
        {
            string Formato = "##0";
            int NumDecimales = Utilities.ContarDecimales(Value);
            if (NumDecimales > 1 | NumDecimales == 1 & Value % 1 != 0)
            {
                Formato += ".";
                for (var i = 1; i <= NumDecimales; i++)
                    Formato += "0";
            }
            return Value.ToString(Formato).Replace(",", ".");
        }

        public static string XErrorCauseToString(Task<HttpResponseMessage> message)
        {
            StringBuilder sb = new StringBuilder();
            string XErrorCause = null; TryGetValuesHeader(ref XErrorCause, message, "X-Error-Cause");
            if (!string.IsNullOrEmpty(XErrorCause))
                sb.Append(XErrorCause);
            else
            {
                sb.AppendFormat("Código de estado HTTP: {0}. ", message.Result.StatusCode);
                if (!string.IsNullOrEmpty(message.Result.ReasonPhrase))
                    sb.AppendFormat("Mensaje: {0}. ", message.Result.ReasonPhrase);
            }
            return sb.ToString();
        }

        private static void TryGetValuesHeader(ref string ParObj, Task<HttpResponseMessage> message, string NombreHeader)
        {
            IEnumerable<string> headerValues;
            if (message.Result.Headers.TryGetValues(NombreHeader, out headerValues))
                ParObj = headerValues.FirstOrDefault();
            if (ParObj == null)
            {
                if (message.Result.Content.Headers.TryGetValues(NombreHeader, out headerValues))
                    ParObj = headerValues.FirstOrDefault();
            }
        }

        public static string SerializeXml(object obj, ref string mensaje, bool RemoveDeclarations = false)
        {
            try
            {
                using (var strWriter = new Utf8StringWriter())
                {
                    var settings = new XmlWriterSettings();
                    settings.Indent = true;
                    settings.OmitXmlDeclaration = true;
                    using (var writer = XmlWriter.Create(strWriter, settings))
                    {
                        var serializer = new XmlSerializer(obj.GetType());
                        if (!RemoveDeclarations) serializer.Serialize(writer, obj);
                        else
                        {
                            var ns = new XmlSerializerNamespaces();
                            ns.Add(string.Empty, obj.GetType().GetCustomAttribute<XmlRootAttribute>().Namespace);
                            serializer.Serialize(writer, obj, ns);
                        }
                        string resultXml = strWriter.ToString();
                        return resultXml;
                    }
                }
            }
            catch (Exception ex)
            {
                mensaje = ExceptionToString(ex);
                return null;
            }
        }

        public static T DeserializeXmlObject<T>(string str, ref string mensaje)
        {
            try
            {
                using (StringReader sr = new StringReader(str))
                {
                    XmlSerializer ser = new XmlSerializer(typeof(T));
                    return (T)ser.Deserialize(sr);
                }
            }
            catch (Exception ex)
            {
                mensaje = ExceptionToString(ex);
                return default;
            }
        }

        public static T DeserializeXmlObject<T>(Stream stm, ref string mensaje)
        {
            try
            {
                using (StreamReader sr = new StreamReader(stm))
                {
                    XmlSerializer ser = new XmlSerializer(typeof(T));
                    return (T)ser.Deserialize(sr);
                }
            }
            catch (Exception ex)
            {
                mensaje = ExceptionToString(ex);
                return default;
            }
        }

        public static string SerializeJson(object obj, ref string mensaje)
        {
            try
            {
                return JsonConvert.SerializeObject(obj);
            }
            catch (Exception ex)
            {
                mensaje = ExceptionToString(ex);
                return default;
            }
        }

        public static T DeserializeJsonObject<T>(string str, ref string mensaje)
        {
            try
            {
                return JsonConvert.DeserializeObject<T>(str);
            }
            catch (Exception ex)
            {
                mensaje = ExceptionToString(ex);
                return default;
            }
        }

        public static string StringToBase64(string Cadena)
        {
            try
            {
                return Convert.ToBase64String(Encoding.UTF8.GetBytes(Cadena));
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }

        public static XmlDocument FileToXmlDocument(Stream stm, ref string mensaje)
        {
            try
            {
                if (stm.Position > 0)
                    stm.Position = 0;

                XmlDocument Xml = new XmlDocument();
                Xml.Load(stm);
                return Xml;
            }
            catch (Exception ex)
            {
                mensaje = ExceptionToString(ex);
                return null;
            }
        }

        private class Utf8StringWriter : StringWriter
        {
            public override Encoding Encoding { get { return Encoding.UTF8; } }
        }

        public static string EnumtoString(this Enum value)
        {
            Type type = value.GetType();
            string name = Enum.GetName(type, value);
            if (name != null)
            {
                FieldInfo field = type.GetField(name);
                if (field != null)
                {
                    if (Attribute.GetCustomAttribute(field,
                             typeof(DescriptionAttribute)) is DescriptionAttribute attr)
                    {
                        return attr.Description;
                    }
                }
            }
            return value.ToString();
        }

        public static System.Exception StringToException(string excepcion)
        {
            if (string.IsNullOrEmpty(excepcion)) throw new System.Exception("Excepción vacía");
            if (excepcion.Contains("ERUSER:"))
            {
                excepcion = excepcion.Replace("ERUSER:", null);
                return new Exception(excepcion);
            }
            else
            {
                excepcion = excepcion.Replace("EREDOC:", null);
                return new System.Exception(excepcion);
            }
        }
    }


}