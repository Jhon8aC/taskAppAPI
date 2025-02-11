using Application.Features.TaskEntity.Commands;
using FluentValidation;

namespace Application.Features.TaskEntity.Validators
{
    public class UpdateTaskCommandValidator : AbstractValidator<UpdateTaskCommand>
    {
        public UpdateTaskCommandValidator()
        {
            // Handle the rules for each property
            RuleFor(x => x.Title)
                    .NotEmpty().WithMessage("You must have a title.")
                    .MaximumLength(50).WithMessage("Title cannot be longer than 50 characters.")
                    .MinimumLength(4).WithMessage("Title cannot be least than 4 characters.")
                    .When(x => x.Title != null); 

            RuleFor(x => x.Description)
                    .NotEmpty().WithMessage("You must have a description.")
                    .MaximumLength(200).WithMessage("Description cannot be longer than 200 characters.")
                    .MinimumLength(4).WithMessage("Description cannot be least than 4 characters.")
                    .When(x => x.Description != null);

            RuleFor(x => x.Completed)
                .NotNull().WithMessage("You must define the status of the task")
                .When(x => x.Completed.HasValue);

        }
    }
}
