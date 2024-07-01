using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Repositories;
using MediatR;

namespace CleanArhitecture.Application.Features.Todos.UpdateTodo;

internal sealed class UpdateTodoCommandHandler(
    ITodoRepository todoRepository) : IRequestHandler<UpdateTodoCommand>
{
    public async Task Handle(UpdateTodoCommand request, CancellationToken cancellationToken)
    {
        Todo? todo = await todoRepository.GetByIdAsync(request.Id, cancellationToken);
        if(todo is null)
        {
            throw new ArgumentException("Todo kaydı bulunamadı");
        }

        todo.Work = request.Work;
        todo.DeadLine = request.DeadLine;
        todo.Note = request.Note;
        todo.IsCompleted = request.IsCompleted;

        await todoRepository.UpdateAsync(todo, cancellationToken);
    }
}
