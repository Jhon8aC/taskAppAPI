using Application.Features.TaskEntity.Commands;
using Application.Features.TaskEntity.Validators;
using FluentValidation.TestHelper;

namespace Tests.Features.TaskEntity.Validators.UpdateTaskValidators
{
    public class UpdateTaskCommandValidatorTest
    {
        private readonly UpdateTaskCommandValidator _validator;

        public UpdateTaskCommandValidatorTest()
        {
            _validator = new UpdateTaskCommandValidator();
        }

        [Fact]
        public void UpdateTask_WithValidData_ShouldPassValidation()
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
            result.ShouldNotHaveValidationErrorFor(x => x.Title);
            result.ShouldNotHaveValidationErrorFor(x => x.Description);
            result.ShouldNotHaveValidationErrorFor(x => x.Completed);
        }
    }
}
