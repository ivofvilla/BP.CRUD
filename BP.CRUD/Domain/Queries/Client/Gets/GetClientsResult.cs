using BP.CRUD.Domain.Models;

namespace BP.CRUD.Domain.Queries.Client.Gets
{
    public class GetClientsResult
    {
        public IEnumerable<Models.Client>? Clients { get; set; }

        public GetClientsResult()
        {
            Clients = new List<Models.Client>();
        }
    }
}
