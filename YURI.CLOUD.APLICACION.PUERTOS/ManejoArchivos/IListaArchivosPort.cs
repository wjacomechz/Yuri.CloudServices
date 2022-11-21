using YURI.CLOUD.APLICACION.DTOs.Common;
using YURI.CLOUD.APLICACION.DTOs.ManejoArchivos;

namespace YURI.CLOUD.APLICACION.PUERTOS.ManejoArchivos
{
    public interface IListaArchivosInputPort
    {
        Task Handle(ListaArchivoParam datos_busqueda);
    }

    public interface IListaArchivosOutputPort
    {
        Task Handle(ListaArchivosResult resultado);
    }
}
