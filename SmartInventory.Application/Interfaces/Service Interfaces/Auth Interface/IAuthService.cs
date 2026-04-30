using SmartInventory.Application.Common.Responses;
using SmartInventory.Application.DTOs.AuthDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartInventory.Application.Interfaces.Service_Interfaces.Auth_Interface
{
    public interface IAuthService
    {
        Task<ServiceResponse<string>> RegisterAsync(RegisterDto dto);
        Task<AuthResponseDto?> LoginAsync(LoginDto dto);
    }
}
