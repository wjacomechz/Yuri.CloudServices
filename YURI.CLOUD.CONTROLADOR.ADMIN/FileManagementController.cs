using Microsoft.AspNetCore.Mvc;
using YURI.CLOUD.APLICACION.DTOs.ManejoArchivos;
using YURI.CLOUD.APLICACION.PUERTOS.ManejoArchivos;
using YURI.CLOUD.PRESENTADORES;

namespace YURI.CLOUD.CONTROLADOR.ADMIN
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileManagementController
    {
        readonly ISubirArchivoInputPort InputPortSubirArchivo;
        readonly ISubirArchivoOutputPort OutputPortSubirArchivo;

        public FileManagementController(ISubirArchivoInputPort inputPortSubirArchivo,
            ISubirArchivoOutputPort outputPortSubirArchivo)
        {
            this.InputPortSubirArchivo = inputPortSubirArchivo;
            this.OutputPortSubirArchivo = outputPortSubirArchivo;
        }

        [HttpPost("subir-archivo")]
        public async Task<string> SubirArchivo(SubirArchivoParam subirArchivoParam)
        {
            await this.InputPortSubirArchivo.Handle(subirArchivoParam);
            var presentador = OutputPortSubirArchivo as SubirArchivoPresenter;
            return presentador.Content;
        }
    }
}