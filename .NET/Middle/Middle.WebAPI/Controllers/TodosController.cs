using Microsoft.AspNetCore.Mvc;
using Middle.WebAPI.DTOs;
using Middle.WebAPI.Models;
using System.Text;
using System.Text.Json;

namespace Middle.WebAPI.Controllers;
[Route("api/[controller]/[action]")]
[ApiController]
public class TodosController : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        HttpClient http = new();

        string uri = "https://jsonplaceholder.typicode.com/todos";

        HttpResponseMessage httpResponseMessage = await http.GetAsync(uri);
        if (httpResponseMessage.IsSuccessStatusCode)
        {
            List<Todo>? todos = await httpResponseMessage.Content.ReadFromJsonAsync<List<Todo>>();

            if (todos is not null)
            {
                return Ok(todos);
            }
        }

        return NotFound();
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateTodoDto request)
    {
        string uri = "https://jsonplaceholder.typicode.com/todos";

        string content = JsonSerializer.Serialize(request);

        StringContent stringContent = new(content, Encoding.UTF8, "application/json");

        HttpClient http = new();

        HttpResponseMessage message = await http.PostAsync(uri, stringContent);
        if (message.IsSuccessStatusCode)
        {
            Todo? todo = await message.Content.ReadFromJsonAsync<Todo>();
            if (todo is not null)
            {
                return Ok(todo);
            }
        }

        return BadRequest(new { Message = "Something went wrong..." });
    }

    [HttpPut]
    public async Task<IActionResult> Update(UpdateTodoDto request)
    {
        string uri = $"https://jsonplaceholder.typicode.com/todos/{request.Id}";

        string content = JsonSerializer.Serialize(request);

        StringContent stringContent = new(content, Encoding.UTF8, "application/json");

        HttpClient http = new();

        HttpResponseMessage message = await http.PutAsync(uri, stringContent);
        if (message.IsSuccessStatusCode)
        {
            Todo? todo = await message.Content.ReadFromJsonAsync<Todo>();
            if (todo is not null)
            {
                return Ok(todo);
            }
        }

        return BadRequest(new { Message = "Something went wrong" });
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteById(int id)
    {
        string uri = $"https://jsonplaceholder.typicode.com/todos/{id}";

        HttpClient http = new();

        HttpResponseMessage message = await http.DeleteAsync(uri);
        if (message.IsSuccessStatusCode)
        {
            return Ok(new { Message = "Delete is successful" });
        }

        return BadRequest("Something went wrong");
    }

    [HttpGet]
    public async Task<IActionResult> GetById(int id)
    {
        string uri = $"https://jsonplaceholder.typicode.com/todos/{id}";

        HttpClient http = new();

        HttpResponseMessage message = await http.GetAsync(uri);
        if (message.IsSuccessStatusCode)
        {
            Todo? todo = await message.Content.ReadFromJsonAsync<Todo>();

            if (todo is not null)
            {
                return Ok(todo);
            }
        }

        return BadRequest(new { Message = "Something went wrong" });
    }
}
