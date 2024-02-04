using BP.CRUD.Domain.Models;
using MediatR;

namespace BP.CRUD.Domain.Commands.Client.Update
{
    public class UpdateClientCommand : IRequest<bool>
    {
        private Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public bool Active { get; set; }
        public IEnumerable<UpdatePhoneCommand> Phones { get; set; }

        public void SetId(Guid id) => Id = id;
        public Guid GetId() => Id;

        public IEnumerable<Phone> PhoneCommandToPhone()
        {
            var phones = new List<Phone>();

            foreach (var p in Phones)
            {
                phones.Add(new Phone
                {
                    IdCliente = Id,
                    Type = p.Type,
                    DDD = p.DDD,
                    Number = p.Number
                });
            }
            return phones;
        }
    }

    public class UpdatePhoneCommand 
    {
        public bool Type { get; set; }
        public int DDD { get; set; }
        public string Number { get; set; }
    }
}
