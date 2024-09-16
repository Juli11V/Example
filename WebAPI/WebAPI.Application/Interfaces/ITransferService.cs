using WebAPI.Application.DTOs;
using WebAPI.Application.Model;

namespace WebAPI.Application.Interfaces;
public interface ITransferService
{
    Task<Result<bool>> TransferAsync(TransferRequest request);
}

