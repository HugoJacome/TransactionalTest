using System.ComponentModel.DataAnnotations;

namespace TransactionalTest.Models
{
    public class Account
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        [RegularExpression(@"^[0-9]*$")]
        public string AccountNumber { get; set; }
        public AccountTypeEnum AccountType { get; set; }
        public double OpeningBalance { get; set; }
        public StateEnum State { get; set; }
    }
}
