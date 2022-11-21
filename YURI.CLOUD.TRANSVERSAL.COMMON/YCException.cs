namespace YURI.CLOUD.TRANSVERSAL.COMMON
{
    public class YCException : System.Exception
    {
        public string mensaje { set; get; }

        public YCException(string mensaje)
        {
            this.mensaje = mensaje;
        }

        public YCException(Enum value) : base(value.ToString("D"))
        {
            this.mensaje = string.Empty;
        }
    }
}
