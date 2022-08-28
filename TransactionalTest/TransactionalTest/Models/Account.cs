using System.ComponentModel.DataAnnotations;

namespace TransactionalTest.Models
{
    public class AccountRequest
    {
        [Required]
        [RegularExpression(@"^[0-9]*$")]
        public long AccountNumber { get; set; }
        public AccountTypeEnum AccountType { get; set; }
        public double OpeningBalance { get; set; }
        public double Balance { get; set; }
        public StateEnum State { get; set; }
        public string ClientIdentification{ get; set; }
    }
    public class Account: AccountRequest
    {
        [Key]
        public Guid Id { get; set; }
        public Guid clientId { get; set; }
    }
    public class AccountInfoEqualityComparer : IEqualityComparer<Account>
    {
        public bool Equals(Account acc1, Account acc2)
        {
            if (acc1 == null && acc2 == null) return true;
            else if (acc1 == null || acc2 == null) return false;
            //TODO: Review key value comparison expression
            else if (acc1.AccountNumber == acc2.AccountNumber && acc1.AccountType == acc2.AccountType
                && acc1.OpeningBalance == acc2.OpeningBalance && acc1.State == acc2.State)
                return true;
            else
                return false;
        }
        public int GetHashCode(Account acc)
        {
            return acc.GetHashCode();
        }
    }
}
