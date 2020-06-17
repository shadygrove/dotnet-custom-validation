using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace my_hybrid_provider.Validation.ResponseModels
{

    /// <summary>
    /// Codes that represents the requirements that are violated.
    /// </summary>
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum ErrorCode
    {
        /// <summary>
        /// No ErrorCode Defined
        /// </summary>
        NotDefined = 0,
        /// <summary>
        /// validation against EmailValidator failed 
        /// </summary>
        EmailError,
        /// <summary>
        /// validation against GreaterThanOrEqualValidator failed 
        /// </summary>
        GreaterThanOrEqualError,
        /// <summary>
        /// validation against GreaterThanValidator failed 
        /// </summary>
        GreaterThanError,
        /// <summary>
        /// validation against LengthValidator failed 
        /// </summary>
        LengthError,
        /// <summary>
        /// validation against MinimumLengthValidator failed 
        /// </summary>
        MinimumLengthError,
        /// <summary>
        /// validation against MaximumLengthValidator failed 
        /// </summary>
        MaximumLengthError,
        /// <summary>
        /// validation against LessThanOrEqualValidator failed 
        /// </summary>
        LessThanOrEqualError,
        /// <summary>
        /// validation against LessThanValidator failed 
        /// </summary>
        LessThanError,
        /// <summary>
        /// validation against NotEmptyValidator failed 
        /// </summary>
        NotEmptyError,
        /// <summary>
        /// validation against NotEqualValidator failed 
        /// </summary>
        NotEqualError,
        /// <summary>
        /// validation against NotNullValidator failed 
        /// </summary>
        NotNullError,
        /// <summary>
        /// validation against PredicateValidator failed 
        /// </summary>
        PredicateError,
        /// <summary>
        /// validation against AsyncPredicateValidator failed 
        /// </summary>
        AsyncPredicateError,
        /// <summary>
        /// validation against RegularExpressionValidator failed 
        /// </summary>
        RegularExpressionError,
        /// <summary>
        /// validation against EqualValidator failed 
        /// </summary>
        EqualError,
        /// <summary>
        /// validation against ExactLengthValidator failed 
        /// </summary>
        ExactLengthError,
        /// <summary>
        /// validation against InclusiveBetweenValidator failed 
        /// </summary>
        InclusiveBetweenError,
        /// <summary>
        /// validation against ExclusiveBetweenValidator failed 
        /// </summary>
        ExclusiveBetweenError,
        /// <summary>
        /// validation against CreditCardValidator failed 
        /// </summary>
        CreditCardError,
        /// <summary>
        /// validation against ScalePrecisionValidator failed 
        /// </summary>
        ScalePrecisionError,
        /// <summary>
        /// validation against EmptyValidator failed 
        /// </summary>
        EmptyError,
        /// <summary>
        /// validation against NullValidator failed 
        /// </summary>
        NullError,
        /// <summary>
        /// validation against EnumValidator failed 
        /// </summary>
        EnumError,
        /// <summary>
        /// validation against RangeValidator failed 
        /// </summary>
        RangeError
    }
}
