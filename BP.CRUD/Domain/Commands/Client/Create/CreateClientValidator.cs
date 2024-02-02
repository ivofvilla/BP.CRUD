using FluentValidation;

namespace BP.CRUD.Domain.Commands.Client.Create
{
    public class CreateClientValidator : AbstractValidator<CreateClientCommand>
    {
        public CreateClientValidator()
        {
            RuleFor(command => command.Name)
                .NotEmpty().WithMessage("O campo Mome é obrigatório.");

            RuleFor(command => command.Email)
                .NotEmpty().WithMessage("O campo E-mail é obrigatória.");
        }
    }
}
