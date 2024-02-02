using BP.CRUD.Domain.Models;
using MediatR;

namespace BP.CRUD.Domain.Commands.Client.Create
{
    public class CreateClientCommand : IRequest<bool>
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public IEnumerable<Phone> Phones { get; set; }
    }
}
