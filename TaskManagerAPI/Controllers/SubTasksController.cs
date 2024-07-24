using Microsoft.AspNetCore.Mvc;
using TaskManagerAPI.DTO;
using TaskManagerAPI.Services;

[Route("api/[controller]")]
[ApiController]
public class SubTasksController : ControllerBase
{
    private readonly ISubTaskService _subTaskService;

    public SubTasksController(ISubTaskService subTaskService)
    {
        _subTaskService = subTaskService;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetSubTask(int id)
    {
        var subTask = await _subTaskService.GetSubTaskAsync(id);
        if (subTask == null)
        {
            return NotFound();
        }
        return Ok(subTask);
    }

    [HttpPost]
    public async Task<IActionResult> CreateSubTask([FromBody] SubTaskDTO subTaskDto)
    {
        await _subTaskService.CreateSubTaskAsync(subTaskDto);
        return CreatedAtAction(nameof(GetSubTask), new { id = subTaskDto.Id }, subTaskDto);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateSubTask(int id, [FromBody] SubTaskDTO subTaskDto)
    {
        try
        {
            await _subTaskService.UpdateSubTaskAsync(id, subTaskDto);
            return NoContent();
        }
        catch (InvalidOperationException ex)
        {
            return Conflict(ex.Message);
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteSubTask(int id)
    {
        await _subTaskService.DeleteSubTaskAsync(id);
        return NoContent();
    }
}