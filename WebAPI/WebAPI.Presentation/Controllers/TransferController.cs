using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Claims;
using System.Threading.Tasks;
using WebAPI.Application.DTOs;
using WebAPI.Application.Interfaces;
using WebAPI.Application.Services;
using WebAPI.Domain.Exceptions;

namespace WebAPI.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TransferController : ControllerBase
    {
        private readonly ITransferService _transferService;
        private readonly IUserAccessor _userAccessor;

        public TransferController(ITransferService transferService, IUserAccessor userAccessor)
        {
            _transferService = transferService;
            _userAccessor = userAccessor;
        }

        [HttpPost("transfer")]
        [CustomExceptionFilter]
        public async Task<IActionResult> Transfer([FromBody] TransferDto request)
        {
            var userId = _userAccessor.GetCurrentUserId();
            if (string.IsNullOrEmpty(userId))
            {
                throw new UnauthorizedAccessException("User ID claim is missing.");
            }

            var transferRequest = new TransferRequest
            {
                FromAccountId = request.FromAccountId,
                ToAccountId = request.ToAccountId,
                Amount = request.Amount,
                UserId = userId,
                Currency = request.Currency,
                PaymentPurpose = request.PaymentPurpose
            };

            await _transferService.TransferAsync(transferRequest);

            return Ok(new { message = "Transfer successful" });
        }
    }
}
