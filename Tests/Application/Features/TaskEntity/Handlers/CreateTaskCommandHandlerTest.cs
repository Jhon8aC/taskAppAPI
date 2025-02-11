using Core.Interfaces;
using Moq;
using Application.Features.TaskEntity.Handlers;
using Application.Features.TaskEntity.Commands;
using Application.Exceptions;

namespace Tests.Application.Features.TaskEntity.Handlers
{
    public class CreateTaskCommandHandlerTest
    {
        [Fact]
        public async Task CreateTask_WithValidData_ShouldCreateTask()
        {
            // Arrange
            var repositoryMock = new Mock<ITaskRepository>();
            repositoryMock.Setup(r => r.AddAsync(It.IsAny<Core.Entities.TaskEntity>())).Verifiable();
            var handler = new CreateTaskCommandHandler(repositoryMock.Object);
            var command = new CreateTaskCommand
            {
                Title = "Test Title",
                Description = "Test Description",
                Completed = false
            };

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.NotEqual(Guid.Empty, result);
            repositoryMock.Verify(r => r.AddAsync(It.IsAny<Core.Entities.TaskEntity>()), Times.Once);
            repositoryMock.Verify(r => r.SaveChangesAsync(), Times.Once);
        }

        [Fact]
        public async Task CreateTask_WithNullRequest_ShouldThrowValidationException()
        {
            // Arrange
            var repositoryMock = new Mock<ITaskRepository>();
            var handler = new CreateTaskCommandHandler(repositoryMock.Object);

            // Act 
            var exception = await Assert.ThrowsAsync<ValidationException>(
                () => handler.Handle(null, CancellationToken.None)
            );

            // Assert
            Assert.IsType<ValidationException>(exception);
            Assert.Contains("cannot be null", exception.Errors.FirstOrDefault(), StringComparison.OrdinalIgnoreCase);
        }
    }
}
