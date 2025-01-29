using FluentValidation.Results;

namespace Application.Exceptions
{        
    public class ValidationException : Exception
    {
        // Default constructor, sets a default error message and initializes the Errors list
        public ValidationException() : base("One or more errors have occurred.")
        {
            Errors = new List<string>();

        }
        // Property to hold a list of validation error messages
        public List<string> Errors { get; }
        // Constructor that accepts a collection of ValidationFailure objects
        // It calls the default constructor and then adds each error message to the Errors list
        public ValidationException(IEnumerable<ValidationFailure> failures) : this()
        {
            foreach (var failure in failures)
            {
                Errors.Add(failure.ErrorMessage);
            }
        }
    }
}
