using System;
using System.Collections.Generic;
using System.Linq;
using FluentValidation.Results;

namespace Application.Exceptions
{
    public class ValidationException : Exception
    {
        public IReadOnlyCollection<string> Errors { get; }

        // Default constructor
        public ValidationException() : base("One or more validation errors occurred.")
        {
            Errors = Array.Empty<string>();
        }

        // Constructor accepting a list of error messages
        public ValidationException(IEnumerable<string> errors)
            : base("One or more validation errors occurred.")
        {
            Errors = errors.ToArray();
        }

        // Constructor accepting FluentValidation failures
        public ValidationException(IEnumerable<ValidationFailure> failures)
            : base("One or more validation errors occurred.")
        {
            Errors = failures.Select(f => f.ErrorMessage).ToArray();
        }
    }
}