using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace my_fluent_provider.Validation.ResonseModels
{
    public class ErrorResponse
    {
        /// <summary>
        /// the list of Errors that caused an invalid model state.
        /// </summary>
        public List<ErrorModel> Errors { get; set; } = new List<ErrorModel>();
    }
}
