using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using TransactionalTest.Models;
using TransactionalTest.Repositories;
using TransactionalTest.Services;

namespace TransactionalTest.Controllers
{
    [ApiController]
    [Route("cuentas")]
    public class AccountsController : Controller
    {
        private IAccountRepository _accountRepository;
        private ICompareServices _compareServices;

        public AccountsController(IAccountRepository accountRepository, ICompareServices compareServices)
        {
            _accountRepository = accountRepository;
            _compareServices = compareServices;
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
        public async Task<IActionResult> Post(Account account)
        {
            var res = await _accountRepository.CreateAccountAsync(account);
            if (res == null) return NotFound();
            return Ok(res);
        }
        [HttpPut("{Id}")]
        public async Task<IActionResult> PutAsync(Account account, Guid Id)
        {
            if (Id != account.Id) BadRequest("Error en relacion");
            var accountDB = await _accountRepository.GetAccountByIdAsync(Id);
            if (accountDB == null) return NotFound();
            // update
            if (_compareServices.CompareAccount(account, accountDB)) BadRequest("cuenta sin cambios");
            _accountRepository.UpdateAccountById(account);
            return Ok(account);
        }
        [HttpDelete("{Id}")]
        public async Task<IActionResult> DeleteAsync(Guid Id)
        {
            var account = await _accountRepository.GetAccountByIdAsync(Id);
            if (account == null) return NotFound();
            // delete
            _accountRepository.DeleteAccountById(account);
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
