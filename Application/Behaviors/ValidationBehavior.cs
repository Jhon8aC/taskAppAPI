using FluentValidation;
using MediatR;

namespace Application.Behaviors
{
    // ValidationBehavior is a pipeline behavior that validates requests before they are handled
    public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>  // List of validators for the request
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;
        public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators; 
        }
        // The Handle method is responsible for validating the request before passing it to the next handler in the pipeline
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            // Creating a validation context for the request to pass it to the validators
            var context = new ValidationContext<TRequest>(request);
            // Running each validator on the request and collecting validation errors
            var failures = _validators
                .Select(v => v.Validate(context))
                .SelectMany(result => result.Errors)
                .Where(f => f != null)
                .ToList();
            // If there are any validation failures, throw a ValidationException
            if (failures.Count != 0)
            {
                throw new ValidationException(failures);
            }
            // If validation is successful, proceed to the next handler in the pipeline
            return await next();
        }
    }
}
