using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using YURI.CLOUD.DOMINIO.Excepciones;

namespace YURI.CLOUD.WEBEXCEPTIONS.PRESENTADOR
{
    public class GeneralExceptionHandler : ExceptionHandlerBase, IExceptionHandler
    {
        public Task Handle(ExceptionContext context)
        {
            var Exception = context.Exception as GeneralException;
            return SetResult(context, StatusCodes.Status500InternalServerError,
                Exception.Message, Exception.Detalle);
        }
    }
}
