using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using TransactionalTest.Models;
using TransactionalTest.Models.Constans;
using TransactionalTest.Repositories;
using TransactionalTest.Services;

namespace TransactionalTest.Controllers
{
    [ApiController]
    [Route("movimiento")]
    public class MovementsController : Controller
    {
        private IMovementRepository _movementRepository;
        private IAccountRepository _accountRepository;
        private ICompareServices _compareServices;
        private IValidateServices _validateServices;

        public MovementsController(
            IMovementRepository movementRepository,
            IAccountRepository accountRepository,
            ICompareServices compareServices, 
            IValidateServices validateServices)
        {
            _movementRepository = movementRepository;
            _accountRepository = accountRepository;
            _compareServices = compareServices;
            _validateServices = validateServices;
        }
        [HttpPost]
        public async Task<IActionResult> Post(MovementRequest movement)
        {
            var account = await _accountRepository.GetAccountByNumberAsync(movement.MovementAccount);
            if (account == null) return BadRequest("Cuenta no encontrada");
            if (movement.Value < 0)
            {
                // validate quota
                if (!(await _validateServices.ValidateMovementQuotaAsync(movement))) return BadRequest(ExceptionConstants.DAILY_QUOTA_EXCEEDED);
                // validate amount
                if (!(await _validateServices.ValidateAccountBalanceAsync(movement, account.Balance))) return BadRequest(ExceptionConstants.UNAVAILABLE_BALANCE);
            }
            account.Balance = account.Balance + movement.Value;
            var movementDB = new Movements()
            {
                Balance = account.Balance,
                account = account,
                MovementDate = movement.MovementDate,
                MovementType = movement.MovementType,
            };
            var res = await _movementRepository.CreateMovementsAsync(movementDB);
            if (res == null) return NotFound();
            return Ok(res);
        }
        [HttpPut("{Id}")]
        public async Task<IActionResult> PutAsync(Movements Movements, Guid Id)
        {
            if (Id != Movements.MovementId) BadRequest("Error en relacion");
            var MovementsDB = await _movementRepository.GetMovementsByIdAsync(Id);
            if (MovementsDB == null) return NotFound();
            // update
            if (_compareServices.CompareMovements(Movements, MovementsDB)) BadRequest("Movementse sin cambios");
            _movementRepository.UpdateMovementsById(Movements);
            return Ok(Movements);
        }
        [HttpDelete("{Id}")]
        public async Task<IActionResult> DeleteAsync(Guid Id)
        {
            var Movements = await _movementRepository.GetMovementsByIdAsync(Id);
            if (Movements == null) return NotFound();
            // delete
            _movementRepository.DeleteMovementById(Movements);
            return Ok(Id);
        }

        [HttpPatch("{Id}")]
        public async Task<IActionResult> PatchMovements(Guid id, [FromBody] JsonPatchDocument MovementsDocument)
        {
            var updatedMovements = await _movementRepository.UpdateMovementsPatchAsync(id, MovementsDocument);
            if (updatedMovements == null)
            {
                return NotFound();
            }
            return Ok(updatedMovements);
        }
    }
}
