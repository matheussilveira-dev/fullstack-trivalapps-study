using Microsoft.AspNetCore.Mvc;
using TrivalTasks.Api.Models;
using TrivalTasks.Api.Services;

namespace TrivalTasks.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TasksController : ControllerBase
{
    private readonly TaskService _taskService;

    public TasksController(TaskService taskService)
    {
        _taskService = taskService;
    }

    [HttpGet]
    public async Task<ActionResult<List<TaskItem>>> GetAll()
    {
        var tasks = await _taskService.GetAllAsync();

        return Ok(tasks);
    }

    [HttpPost]
    public async Task<ActionResult<TaskItem>> Create(TaskItem task)
    {
        if (string.IsNullOrWhiteSpace(task.Title))
        {
            return BadRequest(new
            {
                message = "Informe o título da tarefa."
            });
        }

        task.Id = null;
        task.Title = task.Title.Trim();
        task.Completed = false;

        var createdTask = await _taskService.CreateAsync(task);

        return StatusCode(
            StatusCodes.Status201Created,
            createdTask
        );
    }
}