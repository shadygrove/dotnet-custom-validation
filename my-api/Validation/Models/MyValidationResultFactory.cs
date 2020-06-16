using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace my_api.Validation.Models
{
    public class MyValidationResultFactory
    {
        public static MyValidationResult Create(MyValidationTypes type, string memberName)
        {
            return new MyValidationResult(type, new string[] { memberName });
        }

        public static MyValidationResult Create(MyValidationTypes type, IEnumerable<string> memberNames)
        {
            return new MyValidationResult(type, memberNames);
        }
    }
}
