using BP.CRUD.Repository.Interfaces;
using FluentValidation;
using MediatR;

namespace BP.CRUD.Domain.Commands.Client.Create
{
    public class CreateClientHandle : IRequestHandler<CreateClientCommand, bool>
    {
        private readonly IClientRepository _clientRepository;
        private readonly IValidator<CreateClientCommand> _validator;

        public CreateClientHandle(IClientRepository clientRepository, IValidator<CreateClientCommand> validator)
        {
            _clientRepository = clientRepository;
            _validator = validator;
        }

        public async Task<bool> Handle(CreateClientCommand command, CancellationToken cancellationToken = default)
        {
            var validationResult = await _validator.ValidateAsync(command, cancellationToken);
            if (!validationResult.IsValid)
            {
                return false;
            }

            var client = new Models.Client(command.Name, command.Email, command.PhoneCommandToPhone());

            await _clientRepository.CreateAsync(client, cancellationToken);

            return true;
        }
    }
}
