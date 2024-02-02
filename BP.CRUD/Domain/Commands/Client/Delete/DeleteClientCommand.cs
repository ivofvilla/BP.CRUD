using MediatR;

namespace BP.CRUD.Domain.Commands.Client.Delete
{
    public class DeleteClientCommand : IRequest<bool>
    {
        public Guid Id { get; set; }
    }
}
