using Application.Features.TaskEntity.Handlers;
using Application.Features.TaskEntity.Queries;
using Core.Interfaces;
using Moq;

namespace Tests.Application.Features.TaskEntity.Handlers
{
    public class GetAllTasksQueryHandlerTest
    {
        [Fact]
        public async Task GetAllTasks_WithData_ShouldReturnAllTasks()
        {
            // Arrange
            var tasks = new List<Core.Entities.TaskEntity>
            {
                new Core.Entities.TaskEntity { ID = Guid.NewGuid(), Title = "Task 1", Description = "Description 1", Completed = false},
                new Core.Entities.TaskEntity { ID = Guid.NewGuid(), Title = "Task 2", Description = "Description 2", Completed = false},

            };
            var repositoryMock = new Mock<ITaskRepository>();
            repositoryMock.Setup(repo => repo.GetAllAsync()).ReturnsAsync(tasks);
            var handler = new GetAllTasksQueryHandler(repositoryMock.Object);

            // Act
            var result = await handler.Handle(new GetAllTasksQuery(), CancellationToken.None);

            // Assert
            Assert.Equal(2, result.Count);
            Assert.Equal("Task 1", result[0].Title);
            Assert.Equal("Task 2", result[1].Title);
        }

        [Fact]
        public async Task GetAllTasks_WithoutData_ShouldReturnEmptyList()
        {
            // Arrange
            var repositoryMock = new Mock<ITaskRepository>();
            repositoryMock.Setup(repo => repo.GetAllAsync()).ReturnsAsync(new List<Core.Entities.TaskEntity>());
            var handler = new GetAllTasksQueryHandler(repositoryMock.Object);

            // Act
            var result = await handler.Handle(new GetAllTasksQuery(), CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Empty(result);
        }

        [Fact]
        public async Task GetAllTasks_WithNullData_ShouldReturnEmptyList()
        {
            //Arrange
            var repositoryMock = new Mock<ITaskRepository>();
            repositoryMock.Setup(repo => repo.GetAllAsync()).ReturnsAsync((List<Core.Entities.TaskEntity>)null);
            var handler = new GetAllTasksQueryHandler(repositoryMock.Object);

            // Act
            var result = await handler.Handle(new GetAllTasksQuery(), CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Empty(result);
        }

        [Fact]
        public async Task GetAllTasks_WhenRepositoryThrowsException_ShouldHandleException()
        {
            // Arrange
            var repositoryMock = new Mock<ITaskRepository>();
            repositoryMock.Setup(repo => repo.GetAllAsync()).ThrowsAsync(new Exception("Database error"));
            var handler = new GetAllTasksQueryHandler(repositoryMock.Object);

            // Act
            var exception = await Assert.ThrowsAsync<Exception>(() => handler.Handle(new GetAllTasksQuery(), CancellationToken.None));

            // Assert
            Assert.Equal("Database error", exception.Message);
        }

    }
}
