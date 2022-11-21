using Microsoft.AspNetCore.Mvc.Rendering;
using YURI.CLOUD.TEST.WEBAPP.ViewModels.Shared;

namespace YURI.CLOUD.TEST.WEBAPP.ViewModels.FileManagement
{
    public class ArchivosViewModel : HelperViewModel
    {
        public string IdDirectorioS3 { get; set; }
        public List<SelectListItem> Directorios { get; set; }
        public IEnumerable<ArchivoGridViewModel> Archivos { get; set; }
        public ArchivosViewModel()
        {
            Directorios = new List<SelectListItem>();
            IdDirectorioS3 = string.Empty;
            Archivos = new List<ArchivoGridViewModel>();   
        }
    }
}
