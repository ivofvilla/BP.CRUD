using BP.CRUD.Data;
using BP.CRUD.Domain.Models;
using BP.CRUD.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BP.CRUD.Repository
{
    public class ClientRepository : IClientRepository
    {
        private readonly DbContextClass _context;

        public ClientRepository(DbContextClass context)
        {
            _context = context;
        }

        public async Task CreateAsync(Client client, CancellationToken cancellationToken = default)
        {
            _context.Clients.Add(client);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteAsync(Client client, CancellationToken cancellationToken = default)
        {
            _context.Clients.Remove(client);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteLogicAsync(Client client, CancellationToken cancellationToken = default)
        {
            _context.Clients.Update(client);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<IEnumerable<Client>?> GetAsync(Guid? id, string? name, string? email, bool? type, int? ddd, string? phoneNumber, CancellationToken cancellationToken = default)
        {
            if (id != null)
            {
                return await _context.Clients.Include(c => c.Phones).Where(w => w.Id == id).ToListAsync();
            }

            IQueryable<Client>? clients = _context.Clients.Include(c => c.Phones);

            if (name != null)
            {
                clients = clients.Where(w => w.Name.Contains(name));
            }

            if (email != null)
            {
                clients = clients.Where(w => w.Email.Equals(email));
            }
            if (type != null)
            {
                clients = clients.Where(w => w.Phones.Any(a => a.Type == type.Value));
            }

            if (ddd != null)
            {
                clients = clients.Where(w => w.Phones.Any(a => a.DDD == ddd.Value));
            }

            if (phoneNumber != null)
            {
                clients = clients.Where(w => w.Phones.Any(a => a.Number == phoneNumber));
            }

            return await clients.ToListAsync(cancellationToken);
        }

        public async Task<Client?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _context.Clients.Include(c => c.Phones).FirstOrDefaultAsync(w => w.Id == id, cancellationToken);
        }

        public async Task<IEnumerable<Client>?> GetsAsync(CancellationToken cancellationToken = default)
        {
            return await _context.Clients
                                 .Include(c => c.Phones)
                                 .ToListAsync();
        }

        public async Task UpdateAsync(Client client, CancellationToken cancellationToken = default)
        {
            _context.Clients.Update(client);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
