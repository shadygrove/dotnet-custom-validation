using System;
using System.Collections.Generic;
using System.Text;

namespace MyValidation.Core.V2.Common
{
    public class ValidatorType
    {
        public MyValidatorType MyValidationType { get; private set; }
        public FluentValidatorType FluentValidationType { get; private set; }

        public Validator Validator { get; private set; }

        public static Common.ValidatorType Unknown { get => new Common.ValidatorType(); }

        public override string ToString()
        {
            string validationType = string.Empty;
            switch (Validator)
            {
                case Validator.NotDefined: validationType = $"{Validator}"; break;
                case Validator.MyValidator: validationType = $"MyValidator-{MyValidationType}"; break;
                case Validator.FluentValidator: validationType = $"FluentValidator-{FluentValidationType}"; break;
            }
            return validationType;
        }


        public ValidatorType(FluentValidatorType validationType):this()
        {
            FluentValidationType = validationType;
            Validator = Validator.FluentValidator;
        }
        public ValidatorType(MyValidatorType validationType) : this()
        {
            MyValidationType = validationType;
            Validator = Validator.MyValidator;
        }

        private ValidatorType()
        {
            MyValidationType = MyValidatorType.OTHER;
            FluentValidationType = FluentValidatorType.NotDefined;

            Validator = Validator.NotDefined;
        }

    }
}
