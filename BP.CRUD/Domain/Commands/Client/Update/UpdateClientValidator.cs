using FluentValidation;

namespace BP.CRUD.Domain.Commands.Client.Update
{
    public class UpdateClientValidator : AbstractValidator<UpdateClientCommand>
    {
        public UpdateClientValidator()
        {
            RuleFor(command => command.Id)
                .NotEmpty().WithMessage("O Código é obrigatório.");

            RuleFor(command => command.Name)
                .NotEmpty().WithMessage("O campo Mome é obrigatório.");

            RuleFor(command => command.Email)
                .NotEmpty().WithMessage("O campo E-mail é obrigatória.");

        }
    }
}
