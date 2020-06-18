
using MyValidation.Core.V2.Common;
using MyValidation.Core.V2.ValidationResults;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace my_hybrid_provider.Validation.Annotations
{
    public class MyRangeAttribute : RangeAttribute
    {
        public MyRangeAttribute(int min, int max) : base(min, max) { }

        public MyRangeAttribute(double min, double max) : base(min, max) { }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            ValidationResult baseValidation = base.IsValid(value, validationContext);

            if (baseValidation == null)
            {
                return baseValidation;
            }

            
            MyValidationResult result = MyValidationResultFactory.Create(MyValidatorType.OUT_OF_RANGE, baseValidation);

            return result;
        }
    }
}
