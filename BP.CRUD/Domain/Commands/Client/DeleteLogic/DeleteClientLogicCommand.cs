using MediatR;

namespace BP.CRUD.Domain.Commands.Client.DeleteLogic
{
    public class DeleteClientLogicCommand : IRequest<bool>
    {
        public Guid Id { get; set; }
    }
}
