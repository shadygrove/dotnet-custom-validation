
using MyValidation.Core.V2.Common;
using MyValidation.Core.V2.ValidationResults;
using System.ComponentModel.DataAnnotations;

namespace my_hybrid_provider.Validation.Annotations
{
    public class MyMinLengthAttribute : MinLengthAttribute
    {
        public MyMinLengthAttribute(int length) :
            base(length)
        {
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            ValidationResult baseValidation = base.IsValid(value, validationContext);

            if (baseValidation == null)
            {
                return baseValidation;
            }

            MyValidationResult result = MyValidationResultFactory.Create(MyFluentCodes.MinimumLengthError, baseValidation);

            return result;
        }

        public override string FormatErrorMessage(string name)
        {
            return base.FormatErrorMessage(name);
        }
        public override bool IsValid(object value)
        {
            return base.IsValid(value);
        }
    }
}
