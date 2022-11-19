using FluentValidation;
using YURI.CLOUD.APLICACION.DTOs.ManejoArchivos;

namespace YURI.CLOUD.APLICACION.Common.Validators
{
    public class SubirArchivoValidator : AbstractValidator<SubirArchivoParam>
    {
        public SubirArchivoValidator()
        {
            RuleFor(dmu => dmu.Nombre).NotEmpty()
             .WithMessage("Debe proporcionar el nombre del archivo");
            RuleFor(dmu => dmu.Formato).NotEmpty()
             .WithMessage("Debe proporcionar el formato del archivo");
        }
    }
}
