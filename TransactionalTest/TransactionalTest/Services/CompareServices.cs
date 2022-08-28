using TransactionalTest.Models;

namespace TransactionalTest.Services
{
    public interface ICompareServices
    {
        bool CompareClient(Client client, Client client1);
        bool CompareAccount(Account account, Account account1);
        bool CompareMovements(Movements movements, Movements movements1);
    }
    public class CompareServices : ICompareServices
    {
        public bool CompareAccount(Account account, Account account1)
        {

            var comparer = new AccountInfoEqualityComparer();

            if (comparer.Equals(account, account1))
            {
                return false;
            }
            return true;
        }

        public bool CompareClient(Client client, Client client1)
        {
            var comparer = new ClientInfoEqualityComparer();

            if (comparer.Equals(client, client1))
            {
                return false;
            }
            return true;
        }

        public bool CompareMovements(Movements movements, Movements movements1)
        {
            throw new NotImplementedException();
        }
    }
}
