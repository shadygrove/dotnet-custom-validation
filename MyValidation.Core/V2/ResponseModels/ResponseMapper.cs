using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Linq;
using MyValidation.Core.V2.ValidationResults;

namespace MyValidation.Core.V2.ResponseModels
{
    public class ResponseMapper
    {
        public static ValidationResponseEnvelope MapValidationResult(IEnumerable<ValidationResult> validationResults)
        {
            ValidationResponseEnvelope envelope = new ValidationResponseEnvelope();
            envelope.Message = "Bad Request: There were validation errors";

            var memberNames = validationResults.SelectMany(v => v.MemberNames);

            // for each member name, find all it's errors
            foreach (string member in memberNames)
            {
                if (envelope.Errors.ContainsKey(member))
                {
                    continue;
                }

                var memberValidations = validationResults.Where(vr => vr.MemberNames.Contains(member))
                                            .Select(vr =>
                                            {
                                                var myVr = (MyValidationResult)vr;
                                                return new ValidationResponseModel(myVr.ValidationType, myVr.ErrorMessage);
                                            });

                envelope.Errors.Add(member, memberValidations);
            }

            return envelope;
        }
    }
}
