using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using TransactionalTest.Models;
using TransactionalTest.Models.Constans;
using TransactionalTest.Repositories;
using TransactionalTest.Services;

namespace TransactionalTest.Controllers
{
    [ApiController]
    [Route("cuentas")]
    public class AccountsController : Controller
    {
        private IAccountRepository _accountRepository;
        private IClientRepository _clientRepository;
        private ICompareServices _compareServices;

        public AccountsController(IAccountRepository accountRepository,
            ICompareServices compareServices,
            IClientRepository clientRepository)
        {
            _accountRepository = accountRepository;
            _compareServices = compareServices;
            _clientRepository = clientRepository;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllAccountsAsync()
        {
            var res = await _accountRepository.GetAccountsAsync();
            if (res == null) return NotFound();
            return Ok(res);
        }
        [HttpGet("{accountNumber}")]
        public async Task<IActionResult> GetAsync(long accountNumber)
        {
            var res = await _accountRepository.GetAccountByNumberAsync(accountNumber);
            if (res == null) return NotFound();
            return Ok(res);
        }
        [HttpPost]
        public async Task<IActionResult> Post(AccountRequest account)
        {
            var client = await _clientRepository.GetClientByIdentificationAsync(account.ClientIdentification);
            if (client == null) return NotFound("Cliente no encontrado");
            Account acc = new Account()
            {
                Id = new Guid(),
                AccountNumber = account.AccountNumber,
                clientId = client.Id,
                ClientIdentification = account.ClientIdentification,
                AccountType = account.AccountType,
                Balance = account.Balance > 0 ? account.Balance : account.OpeningBalance,
                OpeningBalance = account.OpeningBalance,
                State = account.State
            };
            var res = await _accountRepository.CreateAccountAsync(acc);
            if (res == null) return NotFound();
            return Ok(res);
        }
        [HttpPut("{Id}")]
        public async Task<IActionResult> PutAsync(Account account, Guid Id)
        {
            if (Id != account.Id) return BadRequest("Error en relacion");
            var accountDB = await _accountRepository.GetAccountByIdAsync(Id);
            if (accountDB == null) return NotFound();
            // update
            if (_compareServices.CompareAccount(account, accountDB)) return BadRequest("cuenta sin cambios");
            var result = await _accountRepository.UpdateAccountById(account);
            if (!result) return BadRequest(ExceptionConstants.ERROR_UPDATE);
            return Ok(account);
        }
        [HttpDelete("{Id}")]
        public async Task<IActionResult> DeleteAsync(Guid Id)
        {
            var account = await _accountRepository.GetAccountByIdAsync(Id);
            if (account == null) return NotFound();
            // delete
            var result = await _accountRepository.DeleteAccountById(account);
            if (!result) return BadRequest(ExceptionConstants.ERROR_DELETE);
            return Ok(Id);
        }

        [HttpPatch("{Id}")]
        public async Task<IActionResult> Patchaccount(Guid id, [FromBody] JsonPatchDocument accountDocument)
        {
            var updatedaccount = await _accountRepository.UpdateAccountPatchAsync(id, accountDocument);
            if (updatedaccount == null)
            {
                return NotFound();
            }
            return Ok(updatedaccount);
        }
    }
}
