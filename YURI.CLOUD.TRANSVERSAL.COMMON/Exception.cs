namespace YURI.CLOUD.TRANSVERSAL.COMMON
{
    public class Exception : System.Exception
    {
        public string mensaje { set; get; }

        public Exception(string mensaje)
        {
            this.mensaje = mensaje;
        }

        public Exception(Enum value) : base(value.ToString("D"))
        {
            this.mensaje = string.Empty;
        }
    }
}
