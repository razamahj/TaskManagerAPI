using Moq;
using TaskManagerAPI.Tests.DTO;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using TaskManagerAPI.Data;

public class TasksControllerTests
{
    private readonly TasksController _controller;
    private readonly Mock<ITaskRepository> _taskRepositoryMock;
    private readonly Mock<ISubTaskRepository> _subTaskRepositoryMock;
    private readonly Mock<IMapper> _mapperMock;

    public TasksControllerTests()
    {
        _taskRepositoryMock = new Mock<ITaskRepository>();
        _subTaskRepositoryMock = new Mock<ISubTaskRepository>();
        _mapperMock = new Mock<IMapper>();
        _controller = new TasksController(_taskRepositoryMock.Object, _subTaskRepositoryMock.Object, _mapperMock.Object);
    }

    [Fact]
    public async System.Threading.Tasks.Task CreateTask_ReturnsCreatedAtActionResult()
    {
        var taskDto = new TaskDTO { Id = 2, Name = "New Test Task", IsComplete = false };
        var task = new TaskManagerAPI.Models.Task { Id = 2, Name = "New Test Task", IsComplete = false };

        _mapperMock.Setup(m => m.Map<TaskManagerAPI.Models.Task>(taskDto)).Returns(task);
        _taskRepositoryMock.Setup(repo => repo.AddTask(task)).ReturnsAsync(taskDto);

        var result = await _controller.CreateTask(taskDto) as CreatedAtActionResult;

        Assert.NotNull(result);
        var createdTask = Assert.IsType<TaskDTO>(result.Value);
        Assert.Equal(taskDto.Id, createdTask.Id);
        Assert.Equal(taskDto.Name, createdTask.Name);
    }

    [Fact]
    public async System.Threading.Tasks.Task GetTask_ReturnsOkResult_WithTask()
    {
        var taskDto = new TaskDTO { Id = 1, Name = "Test Task", IsComplete = false };
        var task = new TaskManagerAPI.Models.Task { Id = 1, Name = "Test Task", IsComplete = false };

        _taskRepositoryMock.Setup(repo => repo.GetTaskByIdAsync(1)).ReturnsAsync(taskDto);
        _mapperMock.Setup(m => m.Map<TaskManagerAPI.Models.Task>(taskDto)).Returns(task);

        var result = await _controller.GetTask(1) as OkObjectResult;

        Assert.NotNull(result);
        var returnedTask = Assert.IsType<TaskDTO>(result.Value);
        Assert.Equal(taskDto.Id, returnedTask.Id);
        Assert.Equal(taskDto.Name, returnedTask.Name);
    }

    [Fact]
    public async System.Threading.Tasks.Task UpdateTask_ReturnsNoContent()
    {
        var taskDto = new TaskDTO { Id = 1, Name = "Updated Task", IsComplete = true };
        var task = new TaskManagerAPI.Models.Task { Id = 1, Name = "Updated Task", IsComplete = true };

        _mapperMock.Setup(m => m.Map<TaskManagerAPI.Models.Task>(taskDto)).Returns(task);
        _taskRepositoryMock.Setup(repo => repo.UpdateTask(task)).ReturnsAsync(task);

        var result = await _controller.UpdateTask(1, taskDto) as NoContentResult;

        Assert.NotNull(result);

        _taskRepositoryMock.Verify(repo => repo.UpdateTask(task), Times.Once);
    }

    [Fact]
    public async System.Threading.Tasks.Task DeleteTask_ReturnsConflict_WhenSubTasksExist()
    {
        _taskRepositoryMock.Setup(repo => repo.DeleteTask(1)).ThrowsAsync(new InvalidOperationException("Cannot delete task with sub-tasks."));

        var result = await _controller.DeleteTask(1) as ConflictObjectResult;

        Assert.NotNull(result);
        Assert.Equal("Cannot delete task with sub-tasks.", result.Value);
    }
}