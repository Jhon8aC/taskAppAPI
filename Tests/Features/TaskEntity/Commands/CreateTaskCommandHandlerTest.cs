using Core.Interfaces;
using Moq;
using Application.Features.TaskEntity.Handlers;
using Application.Features.TaskEntity.Commands;

namespace Tests.Features.TaskEntity.Commands
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
    }
}
