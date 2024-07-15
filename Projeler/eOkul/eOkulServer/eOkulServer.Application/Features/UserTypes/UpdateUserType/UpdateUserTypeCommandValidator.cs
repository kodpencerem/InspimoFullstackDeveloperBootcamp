using FluentValidation;

namespace eOkulServer.Application.Features.UserTypes.UpdateUserType;

public sealed class UpdateUserTypeCommandValidator : AbstractValidator<UpdateUserTypeCommand>
{
    public UpdateUserTypeCommandValidator()
    {
        RuleFor(p => p.Name).MinimumLength(3).WithMessage("Name must be at least 3 characters");
    }
}
