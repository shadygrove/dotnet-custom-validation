using Microsoft.AspNetCore.Mvc.ModelBinding.Metadata;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace my_hybrid_provider.Validation
{

    public class MyHybridValidatorProvider : IModelValidatorProvider
    {
        private static readonly Dictionary<Type, Type> validatorDictionary = Helpers.ExtractValidatorsFromAssembly();

        public void CreateValidators(ModelValidatorProviderContext context)
        {
            if (context.ModelMetadata.MetadataKind == ModelMetadataKind.Parameter)
            {
                Type fluentValidatorType;

                //does the model have a corresponding fluent validator?
                if (validatorDictionary.TryGetValue(context.ModelMetadata.ModelType, out fluentValidatorType))
                {
                    context.Results.Add(new ValidatorItem
                    {
                        Validator = new MyFluentModelValidator((global::FluentValidation.IValidator)Activator.CreateInstance(fluentValidatorType)),
                        IsReusable = true
                    });
                }
                else
                {
                    context.Results.Add(new ValidatorItem
                    {
                        Validator = new MvcModelValidator(),
                        IsReusable = true
                    });
                }
            }
        }
    }

}
