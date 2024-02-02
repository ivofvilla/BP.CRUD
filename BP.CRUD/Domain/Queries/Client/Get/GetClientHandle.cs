using BP.CRUD.Domain.Queries.Client.Gets;
using BP.CRUD.Repository.Interfaces;
using MediatR;

namespace BP.CRUD.Domain.Queries.Client.Get
{
    public class GetClientHandle : IRequestHandler<GetClientQuery, GetClientResult?>
    {
        private readonly IClientRepository _clientRepository;

        public GetClientHandle(IClientRepository clientRepository)
        {
            _clientRepository = clientRepository;
        }

        public async Task<GetClientResult?> Handle(GetClientQuery query, CancellationToken cancellationToken)
        {
            var result = await _clientRepository.GetAsync(query.Id, query.Name, query.Email, query.DDD, query.PhoneNumber, cancellationToken);
            var clients = new GetClientResult();
            clients.Clients = result;

            return clients;
        }
    }
}
