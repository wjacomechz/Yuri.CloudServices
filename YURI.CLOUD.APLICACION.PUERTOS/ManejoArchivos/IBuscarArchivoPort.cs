using YURI.CLOUD.APLICACION.DTOs.ManejoArchivos;

namespace YURI.CLOUD.APLICACION.PUERTOS.ManejoArchivos
{
    public interface IBuscarArchivoInputPort
    {
        Task Handle(BuscarArchivoParam datos_busqueda);
    }

    public interface IBuscarArchivoOutputPort
    {
        Task Handle(BuscarArchivoResult resultado);
    }
}
