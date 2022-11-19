using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using YURI.CLOUD.APLICACION.DTOs.Common;
using YURI.CLOUD.APLICACION.DTOs.ManejoArchivos;
using YURI.CLOUD.APLICACION.PUERTOS.ManejoArchivos;
using YURI.CLOUD.TRANSVERSAL.IO.Interface;

namespace YURI.CLOUD.APLICACION.ManejoArchivos
{
    /// <summary>
    /// Tiene la logica del negocio: Subida de archivos en la nube.
    /// </summary>
    public class SubirArchivoInteractor : ISubirArchivoInputPort
    {
        readonly IEnumerable<IValidator<SubirArchivoParam>> Validators;
        readonly ISubirArchivoOutputPort OutputPort;
        readonly IIOService IOService;

        public SubirArchivoInteractor(IEnumerable<IValidator<SubirArchivoParam>> validators, 
            ISubirArchivoOutputPort outputPort,
            IIOService iOService)
        {
            Validators = validators;
            OutputPort = outputPort;
            IOService = iOService;
        }

        public async Task Handle(SubirArchivoParam archivo)
        {
            AppResult respuesta = new AppResult();
            try
            {
                string ruta_archivo = "";
                string mensaje_fileupload = string.Empty;
                byte[] bytes = Convert.FromBase64String(archivo.ArrayByteContenido);
                using (MemoryStream memoryStream = new MemoryStream(bytes))
                {
                    if (IOService.GuardarArchivo(memoryStream, ruta_archivo, ref mensaje_fileupload))
                    {
                        respuesta.CodigoRetorno = "0000";
                        respuesta.MensajeRetorno = "Archivo subido con exito.";
                    }
                    else
                    {
                        respuesta.CodigoRetorno = "1903";
                        respuesta.MensajeRetorno = mensaje_fileupload;
                    }
                }

            }
            catch (Exception ex)
            {
                //throw new GeneralException("Error al registrar un nuevo usuario en el sistema.", ex.Message);
            }
            await this.OutputPort.Handle(respuesta);
        }
    }
}
