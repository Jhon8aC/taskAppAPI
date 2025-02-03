
using Application.Features.TaskEntity.Commands;
using Application.Features.TaskEntity.Handlers;
using Core.Interfaces;
using Moq;
using System;

namespace Tests.Features.TaskEntity.Handlers
{
    public class UpdateTaskCommandHandlerTest
    {
        [Fact]
        public async Task UpdateTask_WithValidData_ShouldUpdateTask()
        {
            // Arrange 
            var taskId = Guid.NewGuid();
            var existingTask = new Core.Entities.TaskEntity
            {
                ID = taskId,
                Title = "Old Title",
                Description = "Old Description",
                Completed = false
            };
            var repositoryMock = new Mock<ITaskRepository>();
            repositoryMock.Setup(r => r.GetByIdAsync(taskId)).ReturnsAsync(existingTask);
            repositoryMock.Setup(r => r.UpdateAsync(It.IsAny<Core.Entities.TaskEntity>())).Verifiable();
            var handler = new UpdateTaskCommandHandler(repositoryMock.Object);
            var command = new UpdateTaskCommand
            {
                TaskId = taskId,
                Title = "New Title",
                Description = "New Description",
                Completed = true
            };

            // Act
            await handler.Handle(command, CancellationToken.None);

            // Assert
            repositoryMock.Verify(r => r.GetByIdAsync(taskId), Times.Once);
            repositoryMock.Verify(r => r.UpdateAsync(It.IsAny<Core.Entities.TaskEntity>()), Times.Once);
            repositoryMock.Verify(r => r.SaveChangesAsync(), Times.Once);
        }

        [Fact]
        public async Task UpdateTask_WithInvalidId_ShouldReturnNotFound()
        {
            // Arrange 
            var taskId = Guid.NewGuid();
            var existingTask = new Core.Entities.TaskEntity
            {
                ID = taskId,
                Title = "Old Title",
                Description = "Old Description",
                Completed = false
            };
            var repositoryMock = new Mock<ITaskRepository>();
            repositoryMock.Setup(r => r.GetByIdAsync(taskId)).ReturnsAsync(existingTask);
            repositoryMock.Setup(r => r.UpdateAsync(It.IsAny<Core.Entities.TaskEntity>())).Verifiable();
            var handler = new UpdateTaskCommandHandler(repositoryMock.Object);
            var command = new UpdateTaskCommand
            {
                TaskId = Guid.NewGuid(),
                Title = "New Title",
                Description = "New Description",
                Completed = true
            };

            // Act
            var exception = await Assert.ThrowsAsync<KeyNotFoundException>(
                () => handler.Handle(command, CancellationToken.None)
            );

            // Assert
            Assert.IsType<KeyNotFoundException>(exception);
            Assert.Contains("Task with ID", exception.Message); 
        }
    }
}

