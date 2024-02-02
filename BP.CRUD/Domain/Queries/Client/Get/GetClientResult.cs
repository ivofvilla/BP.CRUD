
namespace BP.CRUD.Domain.Queries.Client.Get
{
    public class GetClientResult
    {
        public IEnumerable<Models.Client>? Clients { get; set; }

        public GetClientResult()
        {
            Clients = new List<Models.Client>();
        }
    }
}
