using Microsoft.EntityFrameworkCore;
using TransactionalTest.Interfaces;
using TransactionalTest.Models;

namespace TransactionalTest.Repositories
{
    public interface IReportRepository
    {
        Task<List<Movements>> GetMovementsByDateAndClientAsync(ReportRequest data);
    }
    public class ReportRepository : IReportRepository
    {
        private IAppDBContext _context;
        private IClientRepository _clientRepository;
        private IAccountRepository _accountRepository;

        public ReportRepository(
            IAppDBContext context,
            IAccountRepository accountRepository,
            IClientRepository clientRepository
            )
        {
            _context = context;
            _accountRepository = accountRepository;
            _clientRepository = clientRepository;
        }

        public async Task<List<Movements>> GetMovementsByDateAndClientAsync(ReportRequest data)
        {
            var client = await _clientRepository.GetClientByIdentificationAsync(data.ClientIdentification);
            if (client == null) return null;
            var accounts = await _accountRepository.GetAccountsByClientIdAsync(client.Id);
            var accountList = accounts.Select(x => x.AccountNumber).ToList();

            var entity = await _context.Account.FirstOrDefaultAsync(c => c.Id == new Guid());
            var trxs = await _context.Movements.Where(
                trx => trx.MovementDate >= data.StartDate
                && trx.MovementDate <= data.EndDate
                && accountList.Contains(trx.MovementAccount)
            ).ToListAsync();
            return trxs;
        }
    }
}
