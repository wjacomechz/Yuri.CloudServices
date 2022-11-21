using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YURI.CLOUD.APLICACION.DTOs.ManejoArchivos;
using YURI.CLOUD.APLICACION.PUERTOS.ManejoArchivos;
using YURI.CLOUD.DOMINIO.Excepciones;
using YURI.CLOUD.TRANSVERSAL.IO.Interface;

namespace YURI.CLOUD.APLICACION.ManejoArchivos
{
    public class BuscarArchivoInteractor : IBuscarArchivoInputPort
    {
        readonly IBuscarArchivoOutputPort OutputPort;
        readonly IEnumerable<IValidator<BuscarArchivoParam>> Validators;
        readonly IIOService IOService;

        public BuscarArchivoInteractor(IEnumerable<IValidator<BuscarArchivoParam>> validators, 
            IBuscarArchivoOutputPort descargarArchivoOutputPort,
            IIOService iOService)
        {
            OutputPort = descargarArchivoOutputPort;
            Validators = validators;
            IOService = iOService;
        }

        public async Task Handle(BuscarArchivoParam datos_busqueda)
        {
            BuscarArchivoResult respuesta = new BuscarArchivoResult();
            string codigo = string.Empty, mensaje = string.Empty, mensaje_fileupload = string.Empty;
            try
            {
                MemoryStream ms_existe_archivo = IOService.ObtenerArchivo(datos_busqueda.Key, ref mensaje_fileupload);
                if (ms_existe_archivo != null)
                {
                    respuesta.ContenidoArchivo = ms_existe_archivo.ToArray();
                    respuesta.CodigoRetorno = "0000";
                    respuesta.MensajeRetorno = "OK";
                }
                else
                {
                    respuesta.CodigoRetorno = "1903";
                    respuesta.MensajeRetorno = "El archivo no existe en el directorio remoto ";
                }
            }
            catch (Exception ex)
            {
                throw new GeneralException("Error al descargar el archivo.", ex.Message);
            }
            await this.OutputPort.Handle(respuesta);
        }
    }
}
