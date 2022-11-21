using YURI.CLOUD.DOMINIO.POCOEntidades;

namespace YURI.CLOUD.APLICACION.DTOs.ManejoArchivos
{
    public class ListaArchivosResult : Common.AppResult
    {
        public List<Archivo> Data { get; set; }
        public ListaArchivosResult()
        {
            Data = new List<Archivo>();
        }
    }
}
