using System;
using FluentValidation.Results;

namespace Core.Extensions
{
    public class ValidationErrorDetails : ErrorHandlerDetails
    {
        public IEnumerable<ValidationFailure> Errors { get; set; }
    }
}

