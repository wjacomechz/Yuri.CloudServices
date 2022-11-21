using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YURI.CLOUD.APLICACION.DTOs.Common;
using YURI.CLOUD.APLICACION.DTOs.ManejoArchivos;
using YURI.CLOUD.APLICACION.PUERTOS.ManejoArchivos;
using YURI.CLOUD.TRANSVERSAL.COMMON;

namespace YURI.CLOUD.PRESENTADORES.ManejoArchivos
{
    public class ListaArchivosPresenter : IListaArchivosOutputPort, IPresenters<string>
    {
        public string Content { get; private set; }

        public ListaArchivosPresenter()
        {
            Content = string.Empty;
        }

        public Task Handle(ListaArchivosResult resultado)
        {
            string mensaje = string.Empty;
            Content = YCConversions.SerializeJson(resultado, ref mensaje);
            return Task.CompletedTask;
        }
    }
}
