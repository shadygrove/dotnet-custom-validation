using FluentValidation;
using my_hybrid_provider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace my_hybrid_provider.Validation
{
    public static class Helpers
    {

        public static Dictionary<Type, Type> ExtractValidatorsFromAssembly()
        {
            Dictionary<Type, Type> validators = typeof(Startup).Assembly.ExportedTypes.Where(
                x => typeof(IValidator).IsAssignableFrom(x) &&
                !x.IsInterface &&
                !x.IsAbstract).ToDictionary(x => x.BaseType.GetGenericArguments().FirstOrDefault(), x => x);

            return validators;
        }
    }
}
