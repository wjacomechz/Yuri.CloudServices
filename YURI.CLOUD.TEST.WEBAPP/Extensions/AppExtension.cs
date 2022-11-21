using YURI.CLOUD.APLICACION.DTOs.ManejoArchivos;
using YURI.CLOUD.TEST.WEBAPP.ViewModels.Shared;
using AutoMapper;
using YURI.CLOUD.DOMINIO.POCOEntidades;

namespace YURI.CLOUD.TEST.WEBAPP.Extensions
{
    public static class AppExtension
    {
        internal static List<ArchivoGridViewModel> MapToGridArchivos(this List<Archivo> obj)
        {
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.AllowNullCollections = true;
                cfg.AllowNullDestinationValues = true;
                cfg.CreateMap<List<Archivo>, List<ArchivoGridViewModel>>();
            });
            var mapper = configuration.CreateMapper();
            return mapper.Map<List<ArchivoGridViewModel>>(obj);
        }

    }
}
