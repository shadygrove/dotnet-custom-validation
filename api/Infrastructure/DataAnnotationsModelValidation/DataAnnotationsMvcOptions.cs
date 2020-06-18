﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.DataAnnotations;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;
using System;

namespace api.Infrastructure.DataAnnotationsModelValidation
{
    /// <summary>
    /// Sets up default options for <see cref="MvcOptions"/>.
    /// </summary>
    internal class MvcDataAnnotationsMvcOptionsSetup : IConfigureOptions<MvcOptions>
    {
        private readonly IStringLocalizerFactory _stringLocalizerFactory;
        private readonly IValidationAttributeAdapterProvider _validationAttributeAdapterProvider;
        private readonly IOptions<MvcDataAnnotationsLocalizationOptions> _dataAnnotationLocalizationOptions;

        public MvcDataAnnotationsMvcOptionsSetup(
            IValidationAttributeAdapterProvider validationAttributeAdapterProvider,
            IOptions<MvcDataAnnotationsLocalizationOptions> dataAnnotationLocalizationOptions)
        {
            if (validationAttributeAdapterProvider == null)
            {
                throw new ArgumentNullException(nameof(validationAttributeAdapterProvider));
            }

            if (dataAnnotationLocalizationOptions == null)
            {
                throw new ArgumentNullException(nameof(dataAnnotationLocalizationOptions));
            }

            _validationAttributeAdapterProvider = validationAttributeAdapterProvider;
            _dataAnnotationLocalizationOptions = dataAnnotationLocalizationOptions;
        }

        public MvcDataAnnotationsMvcOptionsSetup(
            IValidationAttributeAdapterProvider validationAttributeAdapterProvider,
            IOptions<MvcDataAnnotationsLocalizationOptions> dataAnnotationLocalizationOptions,
            IStringLocalizerFactory stringLocalizerFactory)
            : this(validationAttributeAdapterProvider, dataAnnotationLocalizationOptions)
        {
            _stringLocalizerFactory = stringLocalizerFactory;
        }

        public void Configure(MvcOptions options)
        {
            if (options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }

            options.ModelMetadataDetailsProviders.Add(new DataAnnotationsMetadataProvider(
                options,
                _dataAnnotationLocalizationOptions,
                _stringLocalizerFactory));

            options.ModelValidatorProviders.Add(new DataAnnotationsModelValidatorProvider(
                _validationAttributeAdapterProvider,
                _dataAnnotationLocalizationOptions,
                _stringLocalizerFactory));
        }
    }
}
