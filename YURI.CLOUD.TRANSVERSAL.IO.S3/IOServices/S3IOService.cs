using YURI.CLOUD.TRANSVERSAL.IO.Interface;
using YURI.CLOUD.TRANSVERSAL.IO.S3.Utilities;

namespace YURI.CLOUD.TRANSVERSAL.IO.S3.IOServices
{
    public sealed class S3IOService : IIOService
    {
        private readonly S3IOUtilities s3IOUtilities;

        public S3IOService(string BucketName, string AccessKey, string SecretKey)
        {
            s3IOUtilities = new S3IOUtilities(BucketName, AccessKey, SecretKey);
        }

        /// <summary>
        /// Descripción: Envia a guardar memorystream en S3.
        /// </summary>
        /// <param name="ms">Contenido del archivo</param>
        /// <param name="RutaArchivo">Ruta directorio S3</param>
        /// <param name="mensaje">REF. respuesta del registro del archivo.</param>
        /// <returns>Returna True si se guardo exitosamente el archivo.</returns>
        public bool GuardarArchivo(MemoryStream ms, string RutaArchivo, ref string mensaje)
        {
            try
            {
                var (IsSucceed, Message) = s3IOUtilities.UpLoadFileAsync(RutaArchivo, ms).Result;
                if (!IsSucceed) throw new Exception(Message);
                return IsSucceed;
            }
            catch (Exception ex)
            {
                mensaje = "GuardarArchivo (S3) => " + ex.Message;
                return false;
            }
        }
        /// <summary>
        /// Elimina Archivo del S3
        /// </summary>
        /// <param name="RutaArchivo">Ruta Archivo a eliminar</param>
        /// <param name="mensaje"></param>
        /// <returns></returns>
        public bool EliminarArchivo(string RutaArchivo, ref string mensaje)
        {
            try
            {
                var (IsSucceed, Message) = s3IOUtilities.DeleteFileAsync(RutaArchivo).Result;
                if (!IsSucceed) throw new Exception(Message);
                return IsSucceed;
            }
            catch (Exception ex)
            {
                mensaje = "EliminarArchivo (S3) => " + ex.Message;
                return false;
            }
        }
        
        /// <summary>
        /// Valida si esxiste archivo en s3 por la ruta
        /// </summary>
        /// <param name="RutaArchivo">Ruta Archivo</param>
        /// <param name="mensaje"></param>
        /// <returns></returns>
        public bool ValidarRutaArchivo(string RutaArchivo, ref string mensaje)
        {
            try
            {
                var (IsSucceed, Message) = s3IOUtilities.FileExistsAsync(RutaArchivo).Result;
                if (!IsSucceed) throw new Exception(Message);
                return IsSucceed;
            }
            catch (Exception ex)
            {
                mensaje = "ValidarRutaArchivo (S3) => " + ex.Message;
                return false;
            }
        }
        
        /// <summary>
        /// Crear carpeta en s3
        /// </summary>
        /// <param name="RutaDirectorio">ruta directorio a crear</param>
        /// <param name="mensaje"></param>
        /// <returns></returns>
        public bool GenerarDirectorio(string RutaDirectorio, ref string mensaje)
        {
            try
            {
                var (IsSucceed, Message) = s3IOUtilities.UpLoadFolderAsync(RutaDirectorio).Result;
                if (!IsSucceed) throw new Exception(Message);
                return IsSucceed;
            }
            catch (Exception ex)
            {
                mensaje = "GenerarDirectorio (S3) => " + ex.Message;
                return false;
            }
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="RutaArchivo">ruta archivo en s3</param>
        /// <param name="mensaje"></param>
        /// <returns></returns>
        public MemoryStream ObtenerArchivo(string RutaArchivo, ref string mensaje)
        {
            try
            {
                var (Stream, Message) = s3IOUtilities.DownLoadFileAsync(RutaArchivo).Result;
                {
                    if (Stream == null) throw new Exception(Message);
                    using (var ms = new MemoryStream())
                    {
                        Stream.CopyTo(ms);
                        Stream.Dispose();
                        return ms;
                    };
                };
            }
            catch (Exception ex)
            {
                mensaje = "ObtenerArchivo (S3) => " + ex.Message;
                return null;
            }
        }

    }
}
