﻿using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace my_fluent_provider.Validation
{
    //private static readonly Dictionary<Type, Type> validatorDictionary = ExtractValidators.ExtractValidatorsFromAssembly();

    public class MyValidatorProvider : IModelValidatorProvider
    {
        private static readonly Dictionary<Type, Type> validatorDictionary = Helpers.ExtractValidatorsFromAssembly();

        public void CreateValidators(ModelValidatorProviderContext context)
        {
            if (context.ModelMetadata.ContainerType == null)
            {
                return;
            }

            Type validatorType;
            
            if (validatorDictionary.TryGetValue(context.ModelMetadata.ContainerType, out validatorType))
            {
                context.Results.Add(new ValidatorItem
                {
                    Validator = new FluentValidation((global::FluentValidation.IValidator)Activator.CreateInstance(validatorType)),
                    IsReusable = true
                });
            }
            //if (context.ModelMetadata.ContainerType == typeof(User))
            //{
            //    context.Results.Add(new ValidatorItem
            //    {
            //        Validator = new UserModelValidator(),
            //        IsReusable = true
            //    });
            //}
        }
    }

}
