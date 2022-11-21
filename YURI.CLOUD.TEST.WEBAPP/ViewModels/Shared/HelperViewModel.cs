using Microsoft.AspNetCore.Mvc.Rendering;

namespace YURI.CLOUD.TEST.WEBAPP.ViewModels.Shared
{
    public class HelperViewModel
    {
        public bool Success { get; set; }
        public bool ErrorFatal { get; set; }
        public string MensajeCabecera { get; set; }
        public string Mensaje { get; set; }

        public HelperViewModel()
        {
            Success = true;
            ErrorFatal = false;
            MensajeCabecera = string.Empty;
            Mensaje = string.Empty;
        }

        public List<SelectListItem> GetAllDirectoriosDefaultS3()
        {
            var list = new List<SelectListItem>();
            list.Add(new SelectListItem() { Text = "wjacomechz", Value = "files_users/wjacomechz/" });
            return list;
        }

        public string ConvFechaHoraLogin(DateTime fecha)
        {
            if (fecha.Year > 1990)
                return TRANSVERSAL.COMMON.YCConversions.DBNullDateToString(fecha, "yyyy-MM-dd HH:mm:ss");
            else
                return "No ha iniciado una sesión";
        }

        public string ConvFechaHora(DateTime fecha, string formato)
        {
            if (fecha.Year > 1990)
                return TRANSVERSAL.COMMON.YCConversions.DBNullDateToString(fecha, formato);
            else
                return "Error transformar fecha";
        }


    }
}
