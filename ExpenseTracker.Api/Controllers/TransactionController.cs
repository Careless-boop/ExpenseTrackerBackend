using AutoMapper;
using ExpenseTracker.Application.DTOs.Transaction;
using ExpenseTracker.Application.Mediator.Transactions.Commands;
using ExpenseTracker.Application.Mediator.Transactions.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ExpenseTracker.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<TransactionController> _logger;
        private readonly IMapper _mapper;

        public TransactionController(IMediator mediator, ILogger<TransactionController> logger, IMapper mapper)
        {
            _mediator = mediator;
            _logger = logger;
            _mapper = mapper;
        }

        [Authorize]
        [HttpPost("create")]
        public async Task<IActionResult> CreateTransaction([FromBody] CreateTransactionDto dto)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
            {
                return Unauthorized();
            }

            var command = new CreateTransactionCommand(
                UserId: userId,
                CategoryId: dto.CategoryId,
                Amount: dto.Amount,
                IsExpense: dto.IsExpense,
                Date: dto.Date,
                Note: dto.Note ?? string.Empty
            );

            var transactionId = await _mediator.Send(command);

            return Ok(new { transactionId = transactionId });
        }

        [Authorize]
        [HttpGet("getAll")]
        public async Task<IActionResult> GetAllTransactions()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
            {
                return Unauthorized();
            }

            var transactions = await _mediator.Send(new GetAllTransactionsQuery(userId));

            return Ok(new { transactions = transactions });
        }

        [Authorize]
        [HttpPut("update")]
        public async Task<IActionResult> UpdateTransaction([FromBody] UpdateTransactionDto dto)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null) 
            { 
                return Unauthorized();
            }

            var command = new UpdateTransactionCommand(
                UserId: userId,
                TransactionId: dto.TransactionId,
                CategoryId: dto.CategoryId,
                Amount: dto.Amount,
                Date: dto.Date,
                IsExpense: dto.IsExpense,
                Note: dto.Note
            );

            var transactionId = await _mediator.Send(command);
            return Ok(new { transactionId = transactionId });
        }

        [Authorize]
        [HttpDelete("delete/{transactionId}")]
        public async Task<IActionResult> DeleteTransaction(Guid transactionId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null) 
            { 
                return Unauthorized(); 
            }

            var command = new DeleteTransactionCommand(userId, transactionId);
            await _mediator.Send(command);

            return Ok();
        }

        [Authorize]
        [HttpGet("get/{transactionId}")]
        public async Task<IActionResult> GetTransactionById(Guid transactionId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null) 
            { 
                return Unauthorized(); 
            }

            var query = new GetTransactionByIdQuery(userId, transactionId);
            var transaction = await _mediator.Send(query);

            return Ok(new {transaction = transaction});
        }
    }
}
