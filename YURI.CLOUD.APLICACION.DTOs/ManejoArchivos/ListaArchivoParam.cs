namespace YURI.CLOUD.APLICACION.DTOs.ManejoArchivos
{
    public class ListaArchivoParam
    {
        public bool BuscarTodo { get; set; }
        public string Directorio { get; set; }
        public ListaArchivoParam()
        {
            BuscarTodo = false;
            Directorio = string.Empty;
        }
    }
}
