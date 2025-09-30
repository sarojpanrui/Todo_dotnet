using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using TodoApi.Models;
using TodoApi.Services;

namespace TodoApi.Controllers;

[ApiController]
[Route("api/[controller]")]

public class TodoController : ControllerBase
{
    private readonly TodoServices _todoService;

    public TodoController(TodoServices todoServices)
    {
        _todoService = todoServices;
    }

    [HttpGet]
    public async Task<ActionResult<ApiResponse<List<TodoItem>>>> GetAll()
    {
        var todos = await _todoService.GetAll();

        if (todos == null || !todos.Any())
        {
            return NotFound(new ApiResponse<List<TodoItem>>
            {
                Success = false,
                Message = "No todos found",
                Data = null
            });
        }

        return Ok(new ApiResponse<List<TodoItem>>
        {
            Success = true,
            Message = "Todos fetched successfully",
            Data = todos
        });
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<TodoItem>> GetById(string id)
    {
        try
        {
            var todo = await _todoService.GetById(id);
            return Ok(new ApiResponse<TodoItem>
            {
                Success = todo != null,
                Message = todo != null ? "Todo fetched" : "Todo not found",
                Data = todo

            });
        }
        catch (Exception e)
        {
            return BadRequest(new ApiResponse<TodoItem>
            {
                Success = false,
                Message = e.Message
            });
        }


    }

    [HttpPost]
    public async Task<ActionResult<TodoItem>> Add(TodoItem todo)
    {
        await _todoService.Add(todo);

        return Ok(new ApiResponse<TodoItem>
        {
            Success = todo != null,
            Message = todo != null ? "Todo fetched" : "Todo not found",
            Data = todo

        });
    }

    [HttpPatch("{id}")]
    public async Task<ActionResult> Update(string id, [FromBody]TodoItem todo)
    {
        try
        {

            
            await _todoService.Update(id, todo);
            return Ok(new ApiResponse<TodoItem>
            {
                Success = true,
                Message = "Todo updated",
                Data = todo
            });


        }

        catch (System.Exception e)
        {
            return BadRequest(new ApiResponse<TodoItem>
            {
                Success = false,
                Message = e.Message
            });
        }
    }


    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(string id)
    {
        try
        {
            await _todoService.Delete(id);
            return Ok(new ApiResponse<TodoItem>
            {
                Success = true,
                Message = "Todo deleted"
            });


        }
        catch (System.Exception e)
        {
            return BadRequest(new ApiResponse<TodoItem>
            {
                Success = false,
                Message = e.Message
            });
        }

    }




}