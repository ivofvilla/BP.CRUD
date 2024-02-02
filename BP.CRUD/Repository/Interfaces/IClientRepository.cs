using BP.CRUD.Domain.Models;
using System.Reflection;

namespace BP.CRUD.Repository.Interfaces
{
    public interface IClientRepository
    {
        Task<Client?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task<IEnumerable<Client>?> GetAsync(Guid? id, string? name, string? email, int? ddd, string? phoneNumber, CancellationToken cancellationToken = default);
        Task<IEnumerable<Client>?> GetsAsync(CancellationToken cancellationToken = default);
        Task CreateAsync(Client client, CancellationToken cancellationToken = default);
        Task UpdateAsync(Client client, CancellationToken cancellationToken = default);
        Task DeleteAsync(Client client, CancellationToken cancellationToken = default);
        Task DeleteLogicAsync(Client client, CancellationToken cancellationToken = default);
    }
}
