using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using YURI.CLOUD.DOMINIO.Excepciones;

namespace YURI.CLOUD.WEBEXCEPTIONS.PRESENTADOR
{
    public static class Filters
    {
        /// <summary>
        /// Registrar las excepciones.
        /// </summary>
        /// <param name="options"></param>
        public static void Register(MvcOptions options)
        {
            options.Filters.Add(new ApiExceptionFilterAttribute(
               new Dictionary<Type, IExceptionHandler>
               {
                    { typeof(GeneralException), new GeneralExceptionHandler() },
                    { typeof(ValidationException), new ValidationExceptionHandler() }
               }
            ));
        }
    }
}
