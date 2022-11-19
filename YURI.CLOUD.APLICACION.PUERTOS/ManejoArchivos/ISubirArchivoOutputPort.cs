using YURI.CLOUD.APLICACION.DTOs.Common;

namespace YURI.CLOUD.APLICACION.PUERTOS.ManejoArchivos
{
    public interface ISubirArchivoOutputPort
    {
        Task Handle(AppResult resultado);
    }
}
