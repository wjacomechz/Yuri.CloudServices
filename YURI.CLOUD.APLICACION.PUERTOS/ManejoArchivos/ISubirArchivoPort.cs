using YURI.CLOUD.APLICACION.DTOs.Common;
using YURI.CLOUD.APLICACION.DTOs.ManejoArchivos;

namespace YURI.CLOUD.APLICACION.PUERTOS.ManejoArchivos
{
    public interface ISubirArchivoInputPort
    {
        Task Handle(SubirArchivoParam archivo);
    }

    public interface ISubirArchivoOutputPort
    {
        Task Handle(AppResult resultado);
    }
}
