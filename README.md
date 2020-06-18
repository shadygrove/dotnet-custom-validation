# Exploring Validation in DotNEt

The default model validation is great, but it lacks some useful information like a validation error type code.
This is what the default AspNetCore Model Validation result looks like...
```
{
    "type": "https://tools.ietf.org/html/rfc7231#section-6.5.1",
    "title": "One or more validation errors occurred.",
    "status": 400,
    "traceId": "|41fd7936-40c27cb61ebd8412.",
    "errors": {
        "Summary": [
            "The field Summary must be a string or array type with a minimum length of '1'.",
            "MyCustomValidationAttribute validation result"
        ],
        "TemperatureC": [
            "The field TemperatureC must be between -20 and 100.",
            "MyCustomValidationAttribute validation result"
        ]
    }
}
```
We would like to have a type code that can be used to display internationalized messages to the user.  You can do some level of internationalization of the message in .NET using `Resources` but it seems very limiting to take this approach.

## Resources
[SO: Bad Request w/ Custom Error Model](https://stackoverflow.com/questions/27439100/web-api-2-badrequest-with-custom-error-model)  
[Handling Errors in ASP.NET Web API](https://www.devtrends.co.uk/blog/handling-errors-in-asp.net-core-web-api)  

[Example Validator Using Fluent](https://www.carlrippon.com/fluentvalidation-in-an-asp-net-core-web-api/)

[Automatic Model State Validation in AspNet MVC](https://ben.onfabrik.com/blog/automatic-modelstate-validation-in-aspnet-mvc)

Creating an `InvalidModelStateResponseFactory` may be the way to go here.
[SO: Custom Model Validation Response](https://stackoverflow.com/questions/51145243/how-do-i-customize-asp-net-core-model-binding-errors)

[This article](https://www.strathweb.com/2018/02/exploring-the-apicontrollerattribute-and-its-features-for-asp-net-core-mvc-2-1/) also has an example of custom model state factory

THIS is what we likely need!...  
[Custom ModelValidation Provider](http://prideparrot.com/blog/archive/2012/9/creating_custom_modelvalidatorprovider)

I finally found this open issue on aspnetcore GitHub:  
[Extend Model Class w/ Ability to Have Custom Validation Properties and Values](https://github.com/dotnet/aspnetcore/issues/6661)  
and...  
[Improve Validation for Non-Interactive Consumption](https://github.com/dotnet/aspnetcore/issues/4840)  

This may be the answer...  
[Action Argument Validation in ActionFilter](https://damienbod.com/2016/09/09/asp-net-core-action-arguments-validation-using-an-actionfilter/)  
- referred from [this thread](https://github.com/aspnet/Mvc/issues/5260) on reading Request.Body in ActionFilter

# Proposed Solution
Don't use DataAnnotations.  Instead use IValidatableObject interface, but DISABLE default model validation framework using...
```
services.Configure<ApiBehaviorOptions>(options =>
{
    // Suppress the default model state validation so we can implement our own
    //options.SuppressModelStateInvalidFilter = true;
}
```

Then create a custom Action Filter to read action arguments and validate ones that implement `IValidatableObject`
```
public override void OnActionExecuting(ActionExecutingContext context)
        {
            List<ValidationResult> validationResults = new List<ValidationResult>();

            // Find any action arguments that implement ISpecialValidatableObject
            // and call ValidateSpecial()
            // if we get results, then set the context.Result to short-circuit the request pipeline
            foreach (var arg in context.ActionArguments)
            {
                if (arg.Value is IValidatableObject)
                {
                    IEnumerable<ValidationResult> argResults = (arg.Value as IValidatableObject).Validate(null);

                    validationResults.AddRange(argResults);
                }
            }

            if (validationResults.Count > 0)
            {
                SpecialValidationResultEnvelope envelope = new SpecialValidationResultEnvelope("The request was invalid", Guid.NewGuid().ToString());

                envelope.Errors = validationResults.Select(vr => (SpecialValidationResult)vr);

                context.Result = new BadRequestObjectResult(envelope);
            }
        }
    }
```

And of course you have to add the filter in your controller options...  
```
services.AddControllers(options =>
    {
        options.Filters.Add(typeof(SpecialActionFilter));
    });
```

# Findings
> Sumary: It gets complicated if we want to use the same validation approach that the framework uses 

We could sub-class `ValidationResult` and add new properties, but this will only get us so far.

The `ValidatableObjectAdapter` is the class that converts a ValidationResult to a `ModelValidationResult`.

This is where we get stuck because a `ModelValidationResult` can only have a `MemberName` and `Message`

Ultimately the the Model is updated by the `ValidationVisitor` ([src](https://github.com/dotnet/aspnetcore/blob/902e735b27dea536ad868d13aa74d0e12017fa0f/src/Mvc/Mvc.Core/src/ModelBinding/Validation/ValidationVisitor.cs#L185) which uses the `ModelValidationResult` to update the model.

So...  
**If we REALLY want to control the validation response**  
We would need to create a `CustomObjectValidator` similar to the `DefaultObjectValidator` [src](https://github.com/dotnet/aspnetcore/blob/2d33c321876294cf23ec3e268b4e3db754db350d/src/Mvc/Mvc.Core/src/ModelBinding/Validation/DefaultObjectValidator.cs)
AND make that to return a `CustomValidationVisitor` that overrides methods on `ValidationVisitor` that updates the Model with the properties of a `CustomModelValidationResult` 

## Debugging into AspNetCore Source
[SO: How To Debug Into AspNetCore Source](https://stackoverflow.com/questions/55626888/debug-net-core-source-visual-studio-2019)

## Project Info
Created project with

```
dotnet new api -n api
dotnet new sln
dotnet new gitignore
git init
```

## To run the API

`dotnet run -p api`  
Navigate to [https://localhost:5001/WeatherForecast](https://localhost:5001/WeatherForecast)

## Basic Default Validation Example

I have added a POST action to the weather API. Use PostMan to setup a post request...

```
POST https://localhost:5001/WeatherForecast
Content-Type = "application/json"
Body = RAW/json
{
	"Date": "2020-05-12T18:25:43.511Z",
	"TemperatureC": 130,
	"Summary": ""
}
```

Response...

```
400 Bad Request
{
    "type": "https://tools.ietf.org/html/rfc7231#section-6.5.1",
    "title": "One or more validation errors occurred.",
    "status": 400,
    "traceId": "|bb3cf9ee-468b7e2413b9313e.",
    "errors": {
        "Summary": [
            "The field Summary must be a string or array type with a minimum length of '1'."
        ],
        "TemperatureC": [
            "The field TemperatureC must be between -20 and 100."
        ]
    }
}
```

## Custom Validation Result Example

To make custom validation filter work you need to first **_disable default validation_**
```
services.Configure<ApiBehaviorOptions>(options =>
    {
        // Suppress the default model state validation so we can implement our own
        options.SuppressModelStateInvalidFilter = true;
    });
```




