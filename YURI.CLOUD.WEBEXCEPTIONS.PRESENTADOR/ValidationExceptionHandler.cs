using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Text;

namespace YURI.CLOUD.WEBEXCEPTIONS.PRESENTADOR
{
    public class ValidationExceptionHandler : ExceptionHandlerBase, IExceptionHandler
    {
        public Task Handle(ExceptionContext context)
        {
            var Exception = context.Exception as ValidationException;
            StringBuilder Builder = new StringBuilder();
            foreach (var Failure in Exception.Errors)
            {
                Builder.AppendLine(String.Format("Propiedad: {0}. Error: {1}",
                    Failure.PropertyName, Failure.ErrorMessage));
            }
            return SetResult(context, StatusCodes.Status400BadRequest,
                "Error en los datos de entrada.", Builder.ToString());
        }
    }
}
