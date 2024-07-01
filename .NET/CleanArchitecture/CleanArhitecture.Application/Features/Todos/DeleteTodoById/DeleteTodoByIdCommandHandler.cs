using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Repositories;
using MediatR;

namespace CleanArhitecture.Application.Features.Todos.DeleteTodo;

internal sealed class DeleteTodoByIdCommandHandler(
    ITodoRepository todoRepository) : IRequestHandler<DeleteTodoByIdCommand>
{
    public async Task Handle(DeleteTodoByIdCommand request, CancellationToken cancellationToken)
    {
        Todo? todo = await todoRepository.GetByIdAsync(request.Id, cancellationToken);
        if (todo is null)
        {
            throw new ArgumentException("Todo bulunamadı");
        }

        await todoRepository.DeleteAsync(todo, cancellationToken);
    }
}
