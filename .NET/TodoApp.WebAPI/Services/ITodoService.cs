using TodoApp.WebAPI.Models;

namespace TodoApp.WebAPI.Services;

public interface ITodoService
{
    void Create(Todo todo);
    void Update(Todo todo);
    void Delete(Todo todo);
    List<Todo> GetAll();
    Todo? GetById(Guid id);
}
