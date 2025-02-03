using Application.Features.TaskEntity.Commands;
using Application.Features.TaskEntity.Validators;
using FluentValidation.TestHelper;

namespace Tests.Features.TaskEntity.Validators.UpdateTaskValidators.Properties.Title
{
    public class UpdateTaskCommandValidatorTitleTest
    {
        private readonly UpdateTaskCommandValidator _validator;
        public UpdateTaskCommandValidatorTitleTest()
        {
            _validator = new UpdateTaskCommandValidator();   
        }

        [Fact]
        public void UpdateTask_WithEmptyTitle_ShouldHaveValidationError()
        {
            // Arrange 
            var command = new UpdateTaskCommand
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
        public void UpdateTask_WithTooShortTitle_ShouldHaveValidationError()
        {
            // Arrange 
            var command = new UpdateTaskCommand
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
        public void UpdateTask_WithTooLongTitle_ShouldHaveValidationError()
        {
            // Arrange 
            var data = "abcdefghijklmnopqrstuvwxyz";
            var command = new UpdateTaskCommand
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
