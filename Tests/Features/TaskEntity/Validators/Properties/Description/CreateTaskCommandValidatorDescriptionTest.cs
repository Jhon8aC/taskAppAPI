using Application.Features.TaskEntity.Commands;
using Application.Features.TaskEntity.Validators;
using FluentValidation.TestHelper;

namespace Tests.Features.TaskEntity.Validators.Properties.Description
{
    public class CreateTaskCommandValidatorDescriptionTest
    {
        private readonly CreateTaskCommandValidator _validator;
        public CreateTaskCommandValidatorDescriptionTest()
        {
            _validator = new CreateTaskCommandValidator();
        }
        [Fact]
        public void CreateTask_WithEmptyDescription_ShouldHaveValidationError()
        {
            // Arrange 
            var command = new CreateTaskCommand
            {
                Title = "Test Title",
                Description = "",
                Completed = false
            };

            // Act 
            var result = _validator.TestValidate(command);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Description);
        }
        [Fact]
        public void CreateTask_WithTooShortDescription_ShouldHaveValidationError()
        {
            // Arrange 
            var command = new CreateTaskCommand
            {
                Title = "Test Title",
                Description = "abc",
                Completed = false
            };

            // Act 
            var result = _validator.TestValidate(command);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Description);
        }
        [Fact]
        public void CreateTask_WithTooLongDescription_ShouldHaveValidationError()
        {
            // Arrange 
            var data = "abcdefghijklmnopqrstuvwxyz";
            var command = new CreateTaskCommand
            {
                Title = "Test Title",
                Description = $"{data}{data}{data}{data}{data}{data}{data}{data}",
                Completed = false
            };

            // Act 
            var result = _validator.TestValidate(command);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Description);
        }
    }
}
