using MediatR;

namespace BP.CRUD.Domain.Queries.Client.Get
{
    public class GetClientQuery : IRequest<GetClientResult?>
    {
        public Guid? Id { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public bool? Type { get; set; }
        public int? DDD { get; set; }
        public string? PhoneNumber { get; set; }
    }
}
