using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using BankingAPI.DTOs;
using BankingAPI.Services;

namespace BankingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TransactionsController : ControllerBase
    {
        private readonly TransactionService _transactionService;

        public TransactionsController(TransactionService transactionService)
        {
            _transactionService = transactionService;
        }

        [HttpGet]
        public async Task<ActionResult> GetAllTransactions()
        {
            try
            {
                var transactions = await _transactionService.GetAllTransactions();
                return Ok(transactions);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while fetching transactions", error = ex.Message });
            }
        }

        [HttpGet("client/{clientId}")]
        public async Task<ActionResult> GetTransactionsByClientId(int clientId)
        {
            try
            {
                var transactions = await _transactionService.GetTransactionsByClientId(clientId);
                return Ok(transactions);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while fetching transactions", error = ex.Message });
            }
        }

        [HttpGet("client/{clientId}/summary")]
        public async Task<ActionResult> GetClientTransactionSummary(int clientId)
        {
            try
            {
                var summary = await _transactionService.GetClientTransactionSummary(clientId);
                return Ok(summary);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while fetching transaction summary", error = ex.Message });
            }
        }

        [HttpPost]
        public async Task<ActionResult> RecordTransaction([FromBody] CreateTransactionDto transactionDto)
        {
            try
            {
                var transaction = await _transactionService.RecordTransaction(transactionDto);
                return Ok(transaction);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "An error occurred while recording transaction", error = ex.Message });
            }
        }
    }
}
