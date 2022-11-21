using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using YURI.CLOUD.APLICACION.Common.Validators;
using YURI.CLOUD.APLICACION.ManejoArchivos;
using YURI.CLOUD.APLICACION.PUERTOS.ManejoArchivos;
using YURI.CLOUD.PRESENTADORES.ManejoArchivos;
using YURI.CLOUD.TRANSVERSAL.COMMON;
using YURI.CLOUD.TRANSVERSAL.IO.Interface;
using YURI.CLOUD.TRANSVERSAL.IO.S3.IOServices;

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
            services.AddScoped<IListaArchivosInputPort, ListaArchivosInteractor>();
            services.AddScoped<IBuscarArchivoInputPort, BuscarArchivoInteractor>();
            #endregion
            #region Puertos Salida
            services.AddScoped<ISubirArchivoOutputPort, SubirArchivoPresenter>();
            services.AddScoped<IListaArchivosOutputPort, ListaArchivosPresenter>();
            services.AddScoped<IBuscarArchivoOutputPort, BuscarArchivoPresenter>();
            #endregion
            var BucketName = configuration.GetSection("IO:S3:BucketName").Value;
            var AccessKey = configuration.GetSection("IO:S3:AccessKey").Value;
            var SecreteKey = configuration.GetSection("IO:S3:SecreteKey").Value;
            services.AddSingleton<IIOService>(_ => new S3IOService(BucketName.ToString(), AccessKey.ToString(), SecreteKey.ToString()));
            return services;
        }
    }
}