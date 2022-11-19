namespace YURI.CLOUD.TRANSVERSAL.IO.Interface
{
    public interface IIOService
    {
        /// <summary>
        /// Descripción: Envia a guardar memorystream en S3.
        /// </summary>
        /// <param name="ms">Contenido del archivo</param>
        /// <param name="RutaArchivo">Ruta directorio S3</param>
        /// <param name="mensaje">REF. respuesta del registro del archivo.</param>
        /// <returns>Returna True si se guardo exitosamente el archivo.</returns>
        bool GuardarArchivo(MemoryStream ms, string RutaArchivo, ref string mensaje);
        bool EliminarArchivo(string RutaArchivo, ref string mensaje);
        bool ValidarRutaArchivo(string RutaArchivo, ref string mensaje);
        bool GenerarDirectorio(string RutaDirectorio, ref string mensaje);
        MemoryStream ObtenerArchivo(string RutaArchivo, ref string mensaje);
    }
}
