using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Elwark.Extensions.AspNet
{
    public static class ModelStateExtensions
    {
        public static void AddProblemDetails(this ModelStateDictionary modelsState,
            ValidationProblemDetails problemDetails)
        {
            foreach (var error in problemDetails.Errors)
                foreach (var value in error.Value)
                    modelsState.AddModelError(error.Key, value);
        }

        public static void AddProblemDetails(this ModelStateDictionary modelsState,
            ValidationProblemDetails problemDetails, string key)
        {
            foreach (var error in problemDetails.Errors)
                foreach (var value in error.Value)
                    modelsState.AddModelError(key, value);
        }

        public static void AddProblemDetails(this ModelStateDictionary modelsState,
            ValidationProblemDetails problemDetails, Func<string, string> converter)
        {
            foreach (var error in problemDetails.Errors)
                foreach (var value in error.Value)
                    modelsState.AddModelError(converter(error.Key ?? string.Empty), value);
        }
    }
}