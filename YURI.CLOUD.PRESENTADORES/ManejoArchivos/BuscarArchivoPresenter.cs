using YURI.CLOUD.APLICACION.DTOs.ManejoArchivos;
using YURI.CLOUD.APLICACION.PUERTOS.ManejoArchivos;
using YURI.CLOUD.TRANSVERSAL.COMMON;

namespace YURI.CLOUD.PRESENTADORES.ManejoArchivos
{
    public class BuscarArchivoPresenter : IBuscarArchivoOutputPort, IPresenters<string>
    {
        public string Content { get; private set; }

        public BuscarArchivoPresenter()
        {
            Content = string.Empty;
        }

        public Task Handle(BuscarArchivoResult resultado)
        {
            string mensaje = string.Empty;
            Content = YCConversions.SerializeJson(resultado, ref mensaje);
            return Task.CompletedTask;
        }
    }
}
