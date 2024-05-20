using TodoApp.WebAPI.Context;
using TodoApp.WebAPI.Models;

namespace TodoApp.WebAPI.Services;

public sealed class TodoService(ApplicationDbContext context) : ITodoService
{
    public void Create(Todo todo)
    {
        context.Add(todo);
        context.SaveChanges();
    }

    public void Delete(Todo todo)
    {
        context.Remove(todo);
        context.SaveChanges();
    }

    public List<Todo> GetAll()
    {
        return context.Todos.ToList();
    }

    public Todo? GetById(Guid id)
    {
        return context.Todos.Find(id);
    }

    public void Update(Todo todo)
    {
        context.Update(todo);
        context.SaveChanges();
    }
}
