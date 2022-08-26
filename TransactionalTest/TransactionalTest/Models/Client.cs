using System.ComponentModel.DataAnnotations;

namespace TransactionalTest.Models
{
    public class Client: Person
    {
        //[Key]
        //public Guid ClientId { get; set; }
        public string Password { get; set; }
        public StateEnum State { get; set; }
        public Account account { get; set; }
    }
}
