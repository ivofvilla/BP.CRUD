using BP.CRUD.Repository.Interfaces;
using FluentValidation;
using MediatR;

namespace BP.CRUD.Domain.Commands.Client.DeleteLogic
{
    public class DeleteLogiClientHandle : IRequestHandler<DeleteClientLogicCommand, bool>
    {
        private readonly IClientRepository _clientRepository;
        private readonly IValidator<DeleteClientLogicCommand> _validator;

        public DeleteLogiClientHandle(IClientRepository clientRepository, IValidator<DeleteClientLogicCommand> validator)
        {
            _clientRepository = clientRepository;
            _validator = validator;
        }

        public async Task<bool> Handle(DeleteClientLogicCommand command, CancellationToken cancellationToken = default)
        {
            var validationResult = await _validator.ValidateAsync(command, cancellationToken);
            if (!validationResult.IsValid)
            {
                return false;
            }

            var client = await _clientRepository.GetByIdAsync(command.Id,cancellationToken);
            if (client == null)
            {
                return false;
            }

            client.Active = false;

            await _clientRepository.DeleteLogicAsync(client, cancellationToken);

            return true;
        }
    }
}
