using BP.CRUD.Repository.Interfaces;
using FluentValidation;
using MediatR;

namespace BP.CRUD.Domain.Commands.Client.Update
{
    public class UpdateClientHandle : IRequestHandler<UpdateClientCommand, bool>
    {
        private readonly IClientRepository _clientRepository;
        private readonly IValidator<UpdateClientCommand> _validator;

        public UpdateClientHandle(IClientRepository clientRepository, IValidator<UpdateClientCommand> validator)
        {
            _clientRepository = clientRepository;
            _validator = validator;
        }

        public async Task<bool> Handle(UpdateClientCommand command, CancellationToken cancellationToken = default)
        {
            var validationResult = await _validator.ValidateAsync(command, cancellationToken);
            if (!validationResult.IsValid)
            {
                return false;
            }

            var client = await _clientRepository.GetByIdAsync(command.GetId(), cancellationToken);
            if (client == null)
            {
                return false;
            }

            client.Name = command.Name;
            client.Email = command.Email;
            client.Phones = command.PhoneCommandToPhone();

            await _clientRepository.UpdateAsync(client, cancellationToken);

            return true;
        }
    }
}
