# Exploring Validation in DotNet

This project explores some possible solutions to customizing the response model on 400 BadRequest validations.

The main issue is being able to provide back more detailed information about the validation error.  In particular, an error code. But would like to have abilit to form the response as-needed.

## What's The Problem?
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
            /* You only get message strings here. Nothing more. */
            "The field Summary must be a string or array type with a minimum length of '1'.",
            "MyCustomValidationAttribute validation result"
        ]
    }
}
```
We would like to have a ***validation type code*** that can be used to better handle the response on the client side.  A basic example is to display internationlized message using Angular.

## Summary

While `DataAnnotations` is a nice feature of .NET they can actually make it trickier to have a consistent approach to validation across a project.

We decided to use the approach in the `my-fluent-api` project that uses Fluent Validation 
and the [IValidatorInterceptor](http://mvcpragiya.blogspot.com/2013/09/one-step-forward-from-fluent-validation.html) interface.  

> We should just use Fluent and NOT the native DataAnnotations so that we have a singular and consistent approach to validation

Custom `ValidationExceptions` can be used to add validation error information to the `ModelState`.

From there, the AspNetCore option `options.InvalidModelStateResponseFactory` is used to create a custom response by detecting the custom `ValidationException` and mapping to your own model

We settled on a validation response model that would look something like this...
```
{
    "message": "Bad Request: There were validation errors",
    "code": 400,
    "errors": {
        "Summary": [
            {
                "type": "FluentValidator-EqualError",
                "message": "Fluent: Summary should be Butter always"
            },
            {
                "type": "NotDefined",
                "message": "DotNetCore: Summary is required"
            }
        ],
        "TemperatureC": [
            {
                "type": "MyValidator-OUT_OF_RANGE",
                "message": "MyValidationResult V2: MyValidator-OUT_OF_RANGE The field TemperatureC must be between -20 and 120."
            }
        ]
    }
}
```

### Other Decisions Made w/ Team
Use Fluent
Don't use annotations or IValidatableObject
Acquire API and Acquire Link should use same validation approach
- consider a shared library or nuget package  

Add a Confluence doc on validation guidelines/practices  

Update reponse model to be grouped and keyed by Field Name
- similar to DotNet approach

Location of Validator classes
- Naming convention: use same name as class with a suffix of "Validator"
- The validator class should be located along side the class that it validates


> []  
> []  
> []  
> []  

## Notes/Tips/Tricks We Learned

### About Fluent

Fluent Validation is simply a library that provides a pattern for implementing validation using helper methods like `RuleFor()`

[Example Validator Using Fluent](https://www.carlrippon.com/fluentvalidation-in-an-asp-net-core-web-api/)


> Fluent DOES NOT solve the problem of the response model not having a type code,
> but it does make it easier to do "non-annotation" based validation

To customize the reponse model, Fluent does provide a "hook" via the [IValidatorInterceptor](http://mvcpragiya.blogspot.com/2013/09/one-step-forward-from-fluent-validation.html) interface.

### Data Annotation Resources
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
 
