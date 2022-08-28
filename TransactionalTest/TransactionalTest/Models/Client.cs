using System.ComponentModel.DataAnnotations;

namespace TransactionalTest.Models
{
    public class ClientRequest
    {
        [Required]
        [RegularExpression(@"^[a-zA-Z0-9 ]*$")]
        public string Name { get; set; }
        public GenderEnum Gender { get; set; }
        public string? Age { get; set; }
        public string? Identification { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        [RegularExpression(@"^[0-9]*$")]
        public string Phone { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public StateEnum State { get; set; }
    }
    public class Client : Person
    {
        //[Key]
        //public Guid ClientId { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public StateEnum State { get; set; }
    }
    public class ClientInfoEqualityComparer : IEqualityComparer<Client>
    {
        public bool Equals(Client cl1, Client cl2)
        {
            if (cl1 == null && cl2 == null) return true;
            else if (cl1 == null || cl2 == null) return false;
            //TODO: Review key value comparison expression
            else if (cl1.Address == cl2.Address && cl1.Age == cl2.Age && cl1.Gender == cl2.Gender &&
                cl1.Identification == cl2.Identification && cl1.Name == cl2.Name && cl1.Password == cl2.Password
                && cl1.State == cl2.State && cl1.Phone == cl2.Phone)
                return true;
            else
                return false;
        }
        public int GetHashCode(Client cli)
        {
            return cli.GetHashCode();
        }
    }
}
