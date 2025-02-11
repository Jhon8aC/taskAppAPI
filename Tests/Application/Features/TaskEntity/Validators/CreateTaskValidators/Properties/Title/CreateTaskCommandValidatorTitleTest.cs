using Application.Features.TaskEntity.Commands;
using Application.Features.TaskEntity.Validators;
using FluentValidation.TestHelper;

namespace Tests.Application.Features.TaskEntity.Validators.CreateTaskValidators.Properties.Title
{
    public class CreateTaskCommandValidatorTitleTest
    {
        private readonly CreateTaskCommandValidator _validator;
        public CreateTaskCommandValidatorTitleTest()
        {
            _validator = new CreateTaskCommandValidator();
        }

        [Fact]
        public void CreateTask_WithEmptyTitle_ShouldHaveValidationError()
        {
            // Arrange 
            var command = new CreateTaskCommand
            {
                Title = "",
                Description = "Test Description",
                Completed = false
            };

            // Act 
            var result = _validator.TestValidate(command);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Title);
        }

        [Fact]
        public void CreateTask_WithTooShortTitle_ShouldHaveValidationError()
        {
            // Arrange 
            var command = new CreateTaskCommand
            {
                Title = "abc",
                Description = "Test Description",
                Completed = false
            };

            // Act 
            var result = _validator.TestValidate(command);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Title);
        }

        [Fact]
        public void CreateTask_WithTooLongTitle_ShouldHaveValidationError()
        {
            // Arrange 
            var data = "abcdefghijklmnopqrstuvwxyz";
            var command = new CreateTaskCommand
            {
                Title = $"{data}{data}",
                Description = "Test Description",
                Completed = false
            };

            // Act 
            var result = _validator.TestValidate(command);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Title);
        }
    }
}
