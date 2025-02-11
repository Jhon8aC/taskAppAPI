
using Application.Features.TaskEntity.Commands;
using Application.Features.TaskEntity.Handlers;
using Core.Interfaces;
using Application.Exceptions;
using Moq;

namespace Tests.Application.Features.TaskEntity.Handlers
{
    public class DeleteTaskCommandHandlerTest
    {
        [Fact]
        public async Task DeleteTask_WithValidId_ShouldDeleteTask()
        {
            // Arrange
            var taskId = Guid.NewGuid();
            var existingTask = new Core.Entities.TaskEntity { ID = taskId, Title = "Test Title", Description = "Test Description", Completed = false };
            var repositoryMock = new Mock<ITaskRepository>();
            repositoryMock.Setup(r => r.GetByIdAsync(taskId)).ReturnsAsync(existingTask);
            repositoryMock.Setup(r => r.DeleteAsync(existingTask)).Returns(Task.CompletedTask);
            repositoryMock.Setup(r => r.SaveChangesAsync()).Returns(Task.CompletedTask);
            var handler = new DeleteTaskCommandHandler(repositoryMock.Object);
            var command = new DeleteTaskCommand { TaskId = taskId };

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.Equal(taskId, result);
            repositoryMock.Verify(r => r.GetByIdAsync(taskId), Times.Once);
            repositoryMock.Verify(r => r.DeleteAsync(existingTask), Times.Once);
            repositoryMock.Verify(r => r.SaveChangesAsync(), Times.Once);
        }

        [Fact]
        public async Task DeleteTask_WithInvalidId_ShouldReturnKeyException()
        {
            // Arrange
            var taskId = Guid.NewGuid();
            var existingTask = new Core.Entities.TaskEntity { ID = taskId, Title = "Test Title", Description = "Test Description", Completed = false };
            var repositoryMock = new Mock<ITaskRepository>();
            repositoryMock.Setup(r => r.GetByIdAsync(taskId)).ReturnsAsync(existingTask);
            repositoryMock.Setup(r => r.DeleteAsync(existingTask)).Returns(Task.CompletedTask);
            repositoryMock.Setup(r => r.SaveChangesAsync()).Returns(Task.CompletedTask);
            var handler = new DeleteTaskCommandHandler(repositoryMock.Object);

            // Act
            var exception = await Assert.ThrowsAsync<KeyNotFoundException>(
                () => handler.Handle(new DeleteTaskCommand { TaskId = Guid.NewGuid() }, CancellationToken.None));

            // Assert
            Assert.IsType<KeyNotFoundException>(exception);
            Assert.Matches(@"^Task with ID .+ not found$", exception.Message);

        }

        [Fact]
        public async Task DeleteTask_WithNullRequest_ShouldThrowValidationException()
        {
            // Arrange
            var repositoryMock = new Mock<ITaskRepository>();
            var handler = new DeleteTaskCommandHandler(repositoryMock.Object);

            // Act 
            var exception = await Assert.ThrowsAsync<ValidationException>(() =>
                handler.Handle(null, CancellationToken.None));

            // Assert
            Assert.IsType<ValidationException>(exception);
            Assert.Contains("cannot be null", exception.Errors.FirstOrDefault(), StringComparison.OrdinalIgnoreCase);
        }

    }
}
