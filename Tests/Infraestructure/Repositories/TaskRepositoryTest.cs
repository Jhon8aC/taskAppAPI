using Core.Entities;
using Infraestructure.Repositories;

namespace Tests.Infraestructure.Repositories
{
    public class TaskRepositoryTest
    {
        [Fact]
        public async Task GetAllAsync_WithData_ShouldReturnsAllTasks()
        {
            // Arrange
            using var context = InMemoryDbContextFactory.CreateContext();
            context.Tasks.Add(new TaskEntity { ID = Guid.NewGuid(), Title = "Task1", Description = "Desc1", Completed = false });
            context.Tasks.Add(new TaskEntity { ID = Guid.NewGuid(), Title = "Task2", Description = "Desc2", Completed = true });
            await context.SaveChangesAsync();

            var repository = new TaskRepository(context);

            // Act
            var tasks = await repository.GetAllAsync();

            // Assert
            Assert.Equal(2, tasks.Count);
        }

        [Fact]
        public async Task GetByIdAsync_WithValidId_ShouldReturnsCorrectTask()
        {
            // Arrange
            using var context = InMemoryDbContextFactory.CreateContext();
            var task = new TaskEntity { ID = Guid.NewGuid(), Title = "Test Task", Description = "Test Desc", Completed = false };
            context.Tasks.Add(task);
            await context.SaveChangesAsync();

            var repository = new TaskRepository(context);

            // Act
            var result = await repository.GetByIdAsync(task.ID);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(task.ID, result.ID);
            Assert.Equal("Test Task", result.Title);
        }

        [Fact]
        public async Task AddAsync_WithValidData_ShouldAddsTaskToDatabase()
        {
            // Arrange
            using var context = InMemoryDbContextFactory.CreateContext();
            var repository = new TaskRepository(context);
            var task = new TaskEntity { ID = Guid.NewGuid(), Title = "New Task", Description = "New Desc", Completed = false };

            // Act
            await repository.AddAsync(task);
            await repository.SaveChangesAsync();

            // Assert
            var result = await repository.GetByIdAsync(task.ID);
            Assert.NotNull(result);
            Assert.Equal("New Task", result.Title);
        }

        [Fact]
        public async Task UpdateAsync_WithValidData_ShouldUpdatesTaskInDatabase()
        {
            // Arrange
            using var context = InMemoryDbContextFactory.CreateContext();
            var task = new TaskEntity { ID = Guid.NewGuid(), Title = "Old Title", Description = "Old Desc", Completed = false };
            context.Tasks.Add(task);
            await context.SaveChangesAsync();

            var repository = new TaskRepository(context);

            // Act
            task.Title = "Updated Title";
            task.Description = "Updated Desc";
            task.Completed = true;
            await repository.UpdateAsync(task);
            await repository.SaveChangesAsync();

            // Assert
            var updatedTask = await repository.GetByIdAsync(task.ID);
            Assert.Equal("Updated Title", updatedTask.Title);
            Assert.Equal("Updated Desc", updatedTask.Description);
            Assert.True(updatedTask.Completed);
        }

        [Fact]
        public async Task DeleteAsync_WithValidId_ShouldDeletesTaskFromDatabase()
        {
            // Arrange
            using var context = InMemoryDbContextFactory.CreateContext();
            var task = new TaskEntity { ID = Guid.NewGuid(), Title = "Task to Delete", Description = "Desc", Completed = false };
            context.Tasks.Add(task);
            await context.SaveChangesAsync();

            var repository = new TaskRepository(context);

            // Act
            await repository.DeleteAsync(task);
            await repository.SaveChangesAsync();

            // Assert
            var result = await repository.GetByIdAsync(task.ID);
            Assert.Null(result);
        }
    }
}
