using BP.CRUD.Repository.Interfaces;
using MediatR;

namespace BP.CRUD.Domain.Queries.Client.Gets
{
    public class GetClientsHandle : IRequestHandler<GetClientsQuery, GetClientsResult>
    {
        private readonly IClientRepository _clientRepository;

        public GetClientsHandle(IClientRepository clientRepository)
        {
            _clientRepository = clientRepository;
        }

        public async Task<GetClientsResult> Handle(GetClientsQuery request, CancellationToken cancellationToken)
        {
            var result = await _clientRepository.GetsAsync(cancellationToken);

            var clients = new GetClientsResult();

            clients.Clients = result;

            return clients;
        }
    }
}
