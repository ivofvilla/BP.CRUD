using BP.CRUD.Repository.Interfaces;
using FluentValidation;
using MediatR;

namespace BP.CRUD.Domain.Commands.Client.Delete
{
    public class DeleteClientHandle : IRequestHandler<DeleteClientCommand, bool>
    {
        private readonly IClientRepository _clientRepository;
        private readonly IValidator<DeleteClientCommand> _validator;

        public DeleteClientHandle(IClientRepository clientRepository, IValidator<DeleteClientCommand> validator)
        {
            _clientRepository = clientRepository;
            _validator = validator;
        }

        public async Task<bool> Handle(DeleteClientCommand command, CancellationToken cancellationToken = default)
        {
            var validationResult = await _validator.ValidateAsync(command, cancellationToken);
            if (!validationResult.IsValid)
            {
                return false;
            }

            var client = await _clientRepository.GetByIdAsync(command.Id, cancellationToken);
            if (client == null)
            {
                return false;
            }

            await _clientRepository.DeleteAsync(client, cancellationToken);

            return true;
        }
    }
}
