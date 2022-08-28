using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;
using TransactionalTest.Interfaces;
using TransactionalTest.Models;

namespace TransactionalTest.Repositories
{
    public interface IAccountRepository
    {
        Task<List<Account>> GetAccountsAsync();
        Task<List<Account>> GetAccountsByClientIdAsync(Guid clientId);
        Task<Account?> GetAccountByNumberAsync(long accountNumber);
        Task<Account?> GetAccountByIdAsync(Guid id);
        Task<Account> CreateAccountAsync(Account account);
        Task<bool> UpdateAccountById(Account account);
        Task<Account> UpdateAccountPatchAsync(Guid id, JsonPatchDocument accountDocument);

        Task<bool> DeleteAccountById(Account account);
    }
    public class AccountRepository : IAccountRepository
    {
        private IAppDBContext _context;

        public AccountRepository(IAppDBContext context)
        {
            _context = context;
        }
        public async Task<Account> CreateAccountAsync(Account account)
        {
            _context.Account.Add(account);
            await _context.SaveChangesAsync();
            return account;
        }

        public async Task<bool> DeleteAccountById(Account account)
        {
            try
            {
                _context.Account.Remove(account);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<Account?> GetAccountByIdAsync(Guid id)
        {
            return await _context.Account.FirstOrDefaultAsync(acc => acc.Id == id);
        }

        public async Task<Account?> GetAccountByNumberAsync(long accountNumber)
        {
            return await _context.Account.FirstOrDefaultAsync(acc => acc.AccountNumber == accountNumber);
        }

        public async Task<List<Account>> GetAccountsAsync()
        {
            return await _context.Account.ToListAsync();
        }

        public async Task<List<Account>> GetAccountsByClientIdAsync(Guid clienId)
        {
            return await _context.Account.Where(acc => acc.clientId == clienId).ToListAsync();
        }

        public async Task<bool> UpdateAccountById(Account account)
        {
            try
            {
                var entity = await _context.Account.FirstOrDefaultAsync(c => c.Id == account.Id);
                if (entity != null)
                {
                    entity.AccountType = account.AccountType;
                    entity.State = account.State;
                }
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<Account> UpdateAccountPatchAsync(Guid id, JsonPatchDocument accountDocument)
        {
            var accountQuery = await GetAccountByIdAsync(id);
            if (accountQuery == null)
            {
                return accountQuery;
            }
            accountDocument.ApplyTo(accountQuery);
            await _context.SaveChangesAsync();

            return accountQuery;
        }
    }
}
