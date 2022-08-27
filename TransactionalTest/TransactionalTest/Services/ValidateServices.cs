using TransactionalTest.Models;
using TransactionalTest.Models.Constans;
using TransactionalTest.Repositories;

namespace TransactionalTest.Services
{
    public interface IValidateServices
    {
        Task<bool> ValidateMovementQuotaAsync(MovementRequest movement);
        Task<bool> ValidateAccountBalanceAsync(MovementRequest movement, double balance);
    }
    public class ValidateServices : IValidateServices
    {
        private IMovementRepository _movementRepository;
        private IAccountRepository _accountRepository;
        public ValidateServices(IMovementRepository movementRepository, IAccountRepository accountRepository)
        {
            _movementRepository = movementRepository;
            _accountRepository = accountRepository;
        }

        public async Task<bool> ValidateAccountBalanceAsync(MovementRequest movement, double balance)
        {
            if ((balance + movement.Value) < 0) return false;
            return true;
        }

        public async Task<bool> ValidateMovementQuotaAsync(MovementRequest movement)
        {
            var movements = await _movementRepository.GetMovementsByDateAsync(movement.MovementDate);
            if (movements == null) return false;
            double dailyMovement = movements.Where(m => m.Value < 0).Sum(m => m.Value);
            if ((dailyMovement + movement.Value) > GlobalConfig.DAILY_AMOUNT_LIMIT) return false;
            return true;
        }
    }
}
