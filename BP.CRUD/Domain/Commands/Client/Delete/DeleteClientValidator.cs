using FluentValidation;

namespace BP.CRUD.Domain.Commands.Client.Delete
{
    public class DeleteClientValidator : AbstractValidator<DeleteClientCommand>
    {
        public DeleteClientValidator()
        {
            RuleFor(command => command.Id)
                .NotEmpty().WithMessage("O Código é obrigatório.");

        }
    }
}
