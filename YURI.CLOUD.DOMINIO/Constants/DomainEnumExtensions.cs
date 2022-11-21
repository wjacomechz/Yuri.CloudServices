using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YURI.CLOUD.DOMINIO.Constants
{
    public static class DomainEnumExtensions
    {
        public static string DenialGetString(this DenialLevel me)
        {
            switch (me)
            {
                case DenialLevel.ErrorControlado:
                    return "9996";
                case DenialLevel.ErrorTecnicoSQL:
                    return "9997";
                case DenialLevel.ErrorNoDefinido:
                    return "9998";
                default:
                    return "9999";
            }
        }
    }
}
