using FluentValidation;
using System.Text;
using YURI.CLOUD.APLICACION.DTOs.ManejoArchivos;
using YURI.CLOUD.APLICACION.PUERTOS.ManejoArchivos;
using YURI.CLOUD.DOMINIO.Excepciones;
using YURI.CLOUD.DOMINIO.POCOEntidades;
using YURI.CLOUD.TRANSVERSAL.COMMON;
using YURI.CLOUD.TRANSVERSAL.IO.Interface;

namespace YURI.CLOUD.APLICACION.ManejoArchivos
{
    public class ListaArchivosInteractor : IListaArchivosInputPort
    {
        readonly IEnumerable<IValidator<ListaArchivoParam>> Validators;
        readonly IListaArchivosOutputPort OutputPort;
        readonly IIOService IOService;

        public ListaArchivosInteractor(IEnumerable<IValidator<ListaArchivoParam>> validators, IListaArchivosOutputPort outputPort, IIOService iOService)
        {
            Validators = validators;
            OutputPort = outputPort;
            IOService = iOService;
        }

        public async Task Handle(ListaArchivoParam datos_busqueda)
        {
            ListaArchivosResult respuesta = new ListaArchivosResult();
            try
            {
                string mensaje = string.Empty;
                List<string> archivos_alojados = IOService.ArchivosDirectorio(datos_busqueda.Directorio, ref mensaje);
                if (archivos_alojados != null && archivos_alojados.Count() > 0)
                {
                    respuesta.CodigoRetorno = "0000";
                    respuesta.MensajeRetorno = "Consulta exitosa.";
                    respuesta.Data = FillListArchivos(archivos_alojados);
                }
                else
                {
                    respuesta.CodigoRetorno = "0997";
                    respuesta.MensajeRetorno = mensaje;
                    respuesta.Data = null;
                }
            }
            catch (Exception ex)
            {
                throw new GeneralException("Error al consultar la lista de archivos alojados en la nube.", ex.Message);
            }
            await this.OutputPort.Handle(respuesta);
        }

        private List<Archivo> FillListArchivos(List<string> archivos_alojados)
        {
            List<Archivo> archivos = new List<Archivo>();
            string msj_error = string.Empty;
            string jsonretorno = string.Empty;
            StringBuilder builder = new StringBuilder("[");
            foreach (string item in archivos_alojados)
            {
                builder.Append(item+",");
            }
            jsonretorno = builder.ToString();
            jsonretorno = jsonretorno.Substring(0, jsonretorno.Length - 1) + "]";
            archivos = YCConversions.DeserializeJsonObject<List<Archivo>>(jsonretorno, ref msj_error);
            return archivos;
        }

    }
}
