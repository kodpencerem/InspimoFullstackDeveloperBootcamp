using FluentValidation;

namespace eOkulServer.Application.Features.UserTypes.CreateUserType;

public sealed class CreateUserTypeCommandValidator : AbstractValidator<CreateUserTypeCommand>
{
    public CreateUserTypeCommandValidator()
    {
        RuleFor(p => p.Name).MinimumLength(3).WithMessage("Name must be at least 3 characters");
    }
}