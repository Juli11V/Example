using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Application.DTOs;
using WebAPI.Application.Interfaces;
using WebAPI.Application.Services;
using WebAPI.Domain.Entities;

namespace WebAPI.Presentation.Controllers;
[Route("api/[controller]")]
[ApiController]
public class AccountsController : ControllerBase
{
    private readonly IAccountService _accountService;

    public AccountsController(IAccountService accountService)
    {
        _accountService = accountService;
    }
    [HttpPost]
    public async Task<IActionResult> CreateAccount([FromBody] CreateAccountRequest request )
    {
        var account = await _accountService.CreateAccountAsync(request.UserId, request.Currency);
        return Ok(new { UserId = request.UserId });
    }
    [HttpPost("{accountId}/deposit")]
    public async Task<IActionResult> Deposit(string userId, Guid accountId, [FromBody] TransactionRequest request)
    {
        await _accountService.DepositAsync(userId, request.Amount, accountId);
        return Ok();
    }

    [HttpPost("{accountId}/withdraw")]
    public async Task<IActionResult> Withdraw(string userId, Guid accountId, [FromBody] TransactionRequest request)
    {
        await _accountService.WithdrawAsync(userId, request.Amount, accountId);
        return Ok();
    }

}

