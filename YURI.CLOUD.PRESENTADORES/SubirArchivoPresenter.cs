using YURI.CLOUD.APLICACION.DTOs.Common;
using YURI.CLOUD.APLICACION.PUERTOS.ManejoArchivos;
using YURI.CLOUD.TRANSVERSAL.COMMON;

namespace YURI.CLOUD.PRESENTADORES
{
    public class SubirArchivoPresenter : ISubirArchivoOutputPort, IPresenters<string>
    {
        public string Content { get; private set; }

        public SubirArchivoPresenter()
        {
            Content = string.Empty;
        }

        public Task Handle(AppResult resultado)
        {
            string mensaje = string.Empty;
            Content = Conversions.SerializeJson(resultado, ref mensaje);
            return Task.CompletedTask;
        }

    }
}
