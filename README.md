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

## Notes

#### Suppressing Default Validation Response
```
services.Configure<ApiBehaviorOptions>(options =>
{
    // Suppress the default model state validation so we can implement our own
    options.SuppressModelStateInvalidFilter = true;
});
```

#### Disabling Default Validator
```
services.AddSingleton<IObjectModelValidator, NullObjectModelValidator>();
```
 

## Fluent Resources

[Example Validator Using Fluent](https://www.carlrippon.com/fluentvalidation-in-an-asp-net-core-web-api/)

[IValidatorInterceptor](http://mvcpragiya.blogspot.com/2013/09/one-step-forward-from-fluent-validation.html)


## Data Annotation Resources
[SO: Bad Request w/ Custom Error Model](https://stackoverflow.com/questions/27439100/web-api-2-badrequest-with-custom-error-model)  
[Handling Errors in ASP.NET Web API](https://www.devtrends.co.uk/blog/handling-errors-in-asp.net-core-web-api)  

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
