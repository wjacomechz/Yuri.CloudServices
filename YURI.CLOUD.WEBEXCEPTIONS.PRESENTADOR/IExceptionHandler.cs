using Microsoft.AspNetCore.Mvc.Filters;

namespace YURI.CLOUD.WEBEXCEPTIONS.PRESENTADOR
{
    public interface IExceptionHandler
    {
        Task Handle(ExceptionContext context);
    }
}
