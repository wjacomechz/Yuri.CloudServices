using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using YURI.CLOUD.APLICACION.Common.Validators;
using YURI.CLOUD.APLICACION.ManejoArchivos;
using YURI.CLOUD.APLICACION.PUERTOS.ManejoArchivos;
using YURI.CLOUD.PRESENTADORES;

namespace YURI.CLOUD.IoC
{
    public static class DependencyContainer
    {
        /// <summary>
        /// Centralizamos las dependencias del proyecto.
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static IServiceCollection AddYuriCloudServices(this IServiceCollection services, IConfiguration configuration)
        {

            #region Validadores para los casos de usos
            services.AddValidatorsFromAssembly(typeof(SubirArchivoValidator).Assembly);
            #endregion
            #region Puertos Entrada
            services.AddScoped<ISubirArchivoInputPort, SubirArchivoInteractor>();
            #endregion
            #region Puertos Salida
            services.AddScoped<ISubirArchivoOutputPort, SubirArchivoPresenter>();
            #endregion
            return services;
        }
    }
}