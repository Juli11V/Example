namespace WebAPI.Application.DTOs;

public record TransferMessageDto(
    Guid SenderId,
    Guid ReceiverId,
    decimal Amount,
    DateTime Timestamp
);