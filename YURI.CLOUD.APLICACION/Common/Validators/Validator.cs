using FluentValidation;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YURI.CLOUD.APLICACION.Common.Validators
{
    public static class Validator<Model>
    {
        public static Task<List<ValidationFailure>> Validate(Model model,
            IEnumerable<IValidator<Model>> validators, bool causesException = true)
        {
            var Failrules = validators
                .Select(v => v.Validate(model))
                .SelectMany(r => r.Errors)
                .Where(f => f != null)
                .ToList();
            if (Failrules.Any() && causesException)
                throw new FluentValidation.ValidationException(Failrules);
            return Task.FromResult(Failrules);
        }

        

    }
}
