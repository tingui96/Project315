using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Project315.BasicResponses
{
    public class ApiBadRequestResponse : ApiResponse

    {
        public ApiBadRequestResponse(ModelStateDictionary modelState)
            : base(400)
        {
            if (modelState.IsValid)
            {
                throw new ArgumentException("ModelState must be invalid", nameof(modelState));
            }

            Errors = modelState.SelectMany(x => x.Value?.Errors)
                .Select(x => x.ErrorMessage).ToArray();
        }

        public IEnumerable<string> Errors { get; }
    }
}