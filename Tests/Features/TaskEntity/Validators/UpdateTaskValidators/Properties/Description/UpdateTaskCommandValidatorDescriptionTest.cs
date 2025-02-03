using Application.Features.TaskEntity.Commands;
using Application.Features.TaskEntity.Handlers;
using Application.Features.TaskEntity.Validators;
using Core.Interfaces;
using FluentValidation.TestHelper;
using Moq;

namespace Tests.Features.TaskEntity.Validators.UpdateTaskValidators.Properties.Description
{
    public class UpdateTaskCommandValidatorDescriptionTest
    {
        private readonly UpdateTaskCommandValidator _validator;
        public UpdateTaskCommandValidatorDescriptionTest()
        {
            _validator = new UpdateTaskCommandValidator();
        }

        [Fact]
        public void UpdateTask_WithEmptyDescription_ShouldHaveValidationError()
        {
            // Arrange
            var command = new UpdateTaskCommand
            {
                TaskId = Guid.NewGuid(),
                Title = "New Title",
                Description = "New Description",
                Completed = true
            };

            // Act
            var result = _validator.TestValidate(command);

            // Assert
            result.ShouldNotHaveValidationErrorFor(x => x.Description);
        }

        [Fact]
        public void UpdateTask_WithTooShortDescription_ShouldHaveValidationError()
        {
            // Arrange 
            var command = new UpdateTaskCommand
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
        public void UpdateTask_WithTooLongDescription_ShouldHaveValidationError()
        {
            // Arrange 
            var data = "abcdefghijklmnopqrstuvwxyz";
            var command = new UpdateTaskCommand
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
