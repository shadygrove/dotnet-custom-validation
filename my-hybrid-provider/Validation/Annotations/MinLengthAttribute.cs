using my_hybrid_provider.Validation.ResponseModels;

namespace my_hybrid_provider.Validation.Annotations
{
    public class MinLengthAttribute : System.ComponentModel.DataAnnotations.MinLengthAttribute
    {
        public MinLengthAttribute(int length) :
            base(length)
        {
        }

        protected override System.ComponentModel.DataAnnotations.ValidationResult IsValid(object value, System.ComponentModel.DataAnnotations.ValidationContext validationContext)
        {
            MyValidationResult result = new MyValidationResult(base.IsValid(value, validationContext));
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
