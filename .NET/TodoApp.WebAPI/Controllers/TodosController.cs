using Microsoft.AspNetCore.Mvc;
using TodoApp.WebAPI.Models;
using TodoApp.WebAPI.Services;

namespace TodoApp.WebAPI.Controllers;
[Route("api/[controller]/[action]")]
[ApiController]
public sealed class TodosController(ITodoService todoService) : ControllerBase
{
    [HttpPost]
    public IActionResult Create(string work)
    {
        Todo todo = new(Guid.NewGuid(), work, false, DateTime.Now);

        todoService.Create(todo);

        return Ok(new { Message = "Create is successful" });
    }

    [HttpPut]
    public IActionResult Update(Todo todo)
    {
        todoService.Update(todo);

        return Ok(new { Message = "Update is successful" });
    }

    [HttpDelete]
    public IActionResult DeleteById(Guid id)
    {
        Todo? todo = todoService.GetById(id);
        if (todo is null)
        {
            return NotFound();
        }

        todoService.Delete(todo);

        return Ok(new { Message = "Delete is successful" });
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        return Ok(todoService.GetAll());
    }
}
