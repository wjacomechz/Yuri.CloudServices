using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Diagnostics;
using YURI.CLOUD.APLICACION.DTOs.ManejoArchivos;
using YURI.CLOUD.TEST.WEBAPP.Extensions;
using YURI.CLOUD.TEST.WEBAPP.Models;
using YURI.CLOUD.TEST.WEBAPP.ViewModels.Shared;

namespace YURI.CLOUD.TEST.WEBAPP.Controllers
{
    public class HomeController : BaseController
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IOptions<Settings.WebAppSettings> _settings;
        private readonly IHttpContextAccessor _accessor;

        public HomeController(ILogger<HomeController> logger, IOptions<Settings.WebAppSettings> settings, IHttpContextAccessor accessor)
        {
            this._logger = logger;
            this._settings = settings;
            this._accessor = accessor;
        }

      
        public IActionResult Index()
        {
            string? actionName = this.ControllerContext.RouteData.Values["action"]?.ToString();
            string? controllerName = this.ControllerContext.RouteData.Values["controller"]?.ToString();
            ViewModels.FileManagement.ArchivosViewModel vm_archivo = new ViewModels.FileManagement.ArchivosViewModel();
            ArchivoModel model_archivo = new ArchivoModel(this._settings);
            if(model_archivo.ConsultarArchivos(new ListaArchivoParam() { 
                BuscarTodo = true, Directorio = "files_users/wjacomechz/"
            }, controllerName, actionName))
            {
                vm_archivo.Success = true;
                List<ArchivoGridViewModel> grid = new List<ArchivoGridViewModel>();
                foreach (var item in model_archivo.Archivos)
                {
                    grid.Add(new ArchivoGridViewModel()
                    {
                        Extension = item.Extension,
                        FechaRegistro = item.FechaRegistro,
                        Nombre = item.Nombre,
                        Url = item.Url 
                    });
                }
                vm_archivo.Archivos = grid;
            }
            return View(vm_archivo);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}