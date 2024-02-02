using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BP.CRUD.Domain.Models
{
    [Table(name: "Clientes", Schema = "public")]
    public class Client
    {
        [Key, Column("Id")]
        public Guid Id { get; set; }
        [Column("Nome")]
        public string Name { get; set; } 
        [Column("Email")]
        public string Email { get; set; }
        [Column("Ativo")]
        public bool Active { get; set; }

        public IEnumerable<Phone> Phones { get; set; }



        public Client(string nome, string email, IEnumerable<Phone> Phones)
        {
            this.Id = Guid.NewGuid();
            this.Name = nome;
            this.Email = email;
            this.Active = true;
            this.Phones = Phones;
        }
    }
}
