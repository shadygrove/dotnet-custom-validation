using MyValidation.Core.ResponseModels;
using System.ComponentModel.DataAnnotations;

namespace my_hybrid_provider.Validation.Annotations
{
    public class MyMinLengthAttribute : System.ComponentModel.DataAnnotations.MinLengthAttribute
    {
        public MyMinLengthAttribute(int length) :
            base(length)
        {
        }

        protected override System.ComponentModel.DataAnnotations.ValidationResult IsValid(object value, System.ComponentModel.DataAnnotations.ValidationContext validationContext)
        {
            ValidationResult baseValidation = base.IsValid(value, validationContext);

            if (baseValidation == null)
            {
                return baseValidation;
            }

            MyValidationResult result = new MyValidationResult(baseValidation);
            result.ErrorCode = ErrorCode.MinimumLengthError;

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
