using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Application.DTOs;
using WebAPI.Application.Interfaces;
using WebAPI.Application.Services;
using WebAPI.Domain.Entities;

namespace WebAPI.Presentation.Controllers;
[Route("api/[controller]")]
[ApiController]
public class TransactionsController : ControllerBase
{
    private readonly ITransactionService _transactionService;

    public TransactionsController(ITransactionService transactionService)
    {
        _transactionService = transactionService;
    }

    [HttpGet]
    public async Task<IActionResult> FilterTransactions([FromQuery] TransactionFilter filter, int page = 1, int pageSize = 10)
    {
        var result = await _transactionService.GetTransactionsAsync(filter, page, pageSize);
        return Ok(result);
    }
}

