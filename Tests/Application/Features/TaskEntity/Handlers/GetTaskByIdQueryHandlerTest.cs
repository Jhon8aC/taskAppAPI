using Application.Features.TaskEntity.Handlers;
using Application.Features.TaskEntity.Queries;
using Core.Interfaces;
using Application.Exceptions;
using Moq;

namespace Tests.Application.Features.TaskEntity.Handlers
{
    public class GetTaskByIdQueryHandlerTest
    {
        [Fact]
        public async Task GetTaskById_WithValidId_ShouldReturnTask()
        {
            // Arrange
            var taskId = Guid.NewGuid();
            var task = new Core.Entities.TaskEntity { ID = taskId, Title = "Test Task", Description = "Test Desc", Completed = false };
            var repositoryMock = new Mock<ITaskRepository>();
            repositoryMock.Setup(repo => repo.GetByIdAsync(taskId)).ReturnsAsync(task);

            var handler = new GetTaskByIdQueryHandler(repositoryMock.Object);

            // Act
            var result = await handler.Handle(new GetTaskByIdQuery { TaskId = taskId }, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(taskId, result.ID);
        }
        [Fact]
        public async Task GetTaskById_WithInvalidId_ShouldReturnKeyException()
        {
            // Arrange
            var taskId = Guid.NewGuid();
            var task = new Core.Entities.TaskEntity { ID = taskId, Title = "Test Task", Description = "Test Desc", Completed = false };
            var repositoryMock = new Mock<ITaskRepository>();
            repositoryMock.Setup(repo => repo.GetByIdAsync(taskId)).ReturnsAsync(task);

            var handler = new GetTaskByIdQueryHandler(repositoryMock.Object);

            // Act
            var exception = await Assert.ThrowsAsync<KeyNotFoundException>(
                () => handler.Handle(new GetTaskByIdQuery { TaskId = Guid.NewGuid() }, CancellationToken.None));

            // Assert
            Assert.IsType<KeyNotFoundException>(exception);
            Assert.Matches(@"^Task with ID .+ not found$", exception.Message);
        }

        [Fact]
        public async Task GetTaskById_WithNullRequest_ShouldThrowValidationException()
        {
            // Arrange
            var repositoryMock = new Mock<ITaskRepository>();
            var handler = new GetTaskByIdQueryHandler(repositoryMock.Object);

            // Act
            var exception = await Assert.ThrowsAsync<ValidationException>(() =>
                handler.Handle(null, CancellationToken.None));

            // Assert
            Assert.IsType<ValidationException>(exception);
            Assert.Contains("cannot be null", exception.Errors.FirstOrDefault(), StringComparison.OrdinalIgnoreCase);
        }

    }
}
