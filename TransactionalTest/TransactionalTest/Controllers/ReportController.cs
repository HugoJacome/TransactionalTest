using Microsoft.AspNetCore.Mvc;
using TransactionalTest.Models;
using TransactionalTest.Models.Constans;
using TransactionalTest.Repositories;
using TransactionalTest.Services;

namespace TransactionalTest.Controllers
{
    [ApiController]
    [Route("reportes")]
    public class ReportController : Controller
    {
        private IReportRepository _reportRepository;
        private IValidateServices _validateServices;

        public ReportController(
            IReportRepository reportRepository,
            IValidateServices validateServices)
        {
            _reportRepository = reportRepository;
            _validateServices = validateServices;
        }
        [HttpGet]
        public async Task<IActionResult> Get([FromBody] ReportRequest reportData)
        {
            var trxs = await _reportRepository.GetMovementsByDateAndClientAsync(reportData);
            if (trxs == null) return BadRequest(ExceptionConstants.ERROR_DATA_NOT_FOUND);
            // Service to map trx to report model
            return Ok(trxs);
        }
    }
}
