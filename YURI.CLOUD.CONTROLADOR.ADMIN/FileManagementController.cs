using Microsoft.AspNetCore.Mvc;
using YURI.CLOUD.APLICACION.DTOs.ManejoArchivos;
using YURI.CLOUD.APLICACION.PUERTOS.ManejoArchivos;
using YURI.CLOUD.PRESENTADORES.ManejoArchivos;

namespace YURI.CLOUD.CONTROLADOR.ADMIN
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileManagementController
    {
        readonly ISubirArchivoInputPort InputPortSubirArchivo;
        readonly IListaArchivosInputPort InputPortListaArchivos;
        readonly IBuscarArchivoInputPort InputPortBuscarArchivo;
        readonly ISubirArchivoOutputPort OutputPortSubirArchivo;
        readonly IListaArchivosOutputPort OutputPortListaArchivos;
        readonly IBuscarArchivoOutputPort OutputPortBuscarArchivo;

        public FileManagementController(ISubirArchivoInputPort inputPortSubirArchivo,
            ISubirArchivoOutputPort outputPortSubirArchivo,
            IListaArchivosInputPort inputPortListaArchivos,
            IListaArchivosOutputPort outputPortListaArchivos,
            IBuscarArchivoInputPort inputPortBuscarArchivo,
            IBuscarArchivoOutputPort outputPortBuscarArchivo)
        {
            InputPortSubirArchivo = inputPortSubirArchivo;
            OutputPortSubirArchivo = outputPortSubirArchivo;
            InputPortListaArchivos = inputPortListaArchivos;
            OutputPortListaArchivos = outputPortListaArchivos;
            InputPortBuscarArchivo = inputPortBuscarArchivo;
            OutputPortBuscarArchivo = outputPortBuscarArchivo;
        }

        [HttpPost("SubirArchivo/{rqtapidto}")]
        public async Task<string> SubirArchivo(SubirArchivoParam rqtapidto)
        {
            await this.InputPortSubirArchivo.Handle(rqtapidto);
            var presentador = OutputPortSubirArchivo as SubirArchivoPresenter;
            return presentador.Content;
        }

        [HttpPost("ListaArchivos/{rqtapidto}")]
        public async Task<string> ListaArchivo(ListaArchivoParam rqtapidto)
        {
            await this.InputPortListaArchivos.Handle(rqtapidto);
            var presentador = OutputPortListaArchivos as ListaArchivosPresenter;
            return presentador.Content;
        }

        [HttpPost("BuscarArchivo/{rqtapidto}")]
        public async Task<string> BuscarArchivo(BuscarArchivoParam rqtapidto)
        {
            await this.InputPortBuscarArchivo.Handle(rqtapidto);
            var presentador = OutputPortBuscarArchivo as BuscarArchivoPresenter;
            return presentador.Content;
        }

    }
}