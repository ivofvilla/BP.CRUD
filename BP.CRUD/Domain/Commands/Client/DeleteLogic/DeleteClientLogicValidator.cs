using FluentValidation;

namespace BP.CRUD.Domain.Commands.Client.DeleteLogic
{
    public class DeleteClientLogicValidator : AbstractValidator<DeleteClientLogicCommand>
    {
        public DeleteClientLogicValidator()
        {
            RuleFor(command => command.Id)
                .NotEmpty().WithMessage("O Código é obrigatório.");

        }
    }
}
