using Application.Features.TaskEntity.Commands;
using Application.Features.TaskEntity.Validators;
using FluentValidation.TestHelper;

namespace Tests.Features.TaskEntity.Validators
{

        public class CreateTaskCommandValidatorTest
        {
            private readonly CreateTaskCommandValidator _validator;
            public CreateTaskCommandValidatorTest()
            {
                _validator = new CreateTaskCommandValidator();
            }
            [Fact]
            public void CreateTask_WithValidData_ShouldPassValidation()
            {
                // Arrange 
                var command = new CreateTaskCommand
                {
                    Title = "Test Title",
                    Description = "Test Description",
                    Completed = false
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

