using BP.CRUD.Domain.Models;
using MediatR;

namespace BP.CRUD.Domain.Commands.Client.Update
{
    public class UpdateClientCommand : IRequest<bool>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public bool Active { get; set; }
        public IEnumerable<Phone> Phones { get; set; }

        public void SetId(Guid id) => Id = id;
    }
}
