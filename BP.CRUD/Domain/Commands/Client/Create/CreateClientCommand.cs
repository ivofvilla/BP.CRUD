using BP.CRUD.Domain.Models;
using MediatR;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BP.CRUD.Domain.Commands.Client.Create
{
    public class CreateClientCommand : IRequest<bool>
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public IEnumerable<PhoneCommand> Phones { get; set; }

        public IEnumerable<Phone>  PhoneCommandToPhone()
        {
            var phones = new List<Phone>();

            foreach (var p in Phones)
            {
                phones.Add(new Phone
                {
                    Type = p.Type,
                    DDD = p.DDD,
                    Number = p.Number
                });
            }
            return phones;
        }
    }

    public class PhoneCommand
    {
        public bool Type { get; set; }
        public int DDD { get; set; }
        public string Number { get; set; }
    }
}
