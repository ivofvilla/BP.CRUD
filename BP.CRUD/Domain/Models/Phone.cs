using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BP.CRUD.Domain.Models
{
    [Table(name: "Telefones", Schema = "public")]
    public class Phone
    {
        [Key, Column("Id")]
        public Guid Id { get; set; }

        [Key, Column("IdCliente")]
        public Guid IdCliente { get; set; }

        [Column("Tipo")]
        public bool Type { get; set; }

        [Column("DDD")]
        public int DDD { get; set; }

        [Column("Numero")]
        public string Number { get; set; }

        public Client Client { get; set; }
    }
}
