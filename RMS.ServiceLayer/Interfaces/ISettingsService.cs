using RMS.ServiceLayer.DTOs;

namespace RMS.ServiceLayer
{
    public interface ISettingsService
    {
        Task<bool> ChangeEmailAsync(ChangeEmailDto dto);

        Task<bool> ChangePasswordAsync(ChangePasswordDto dto);
    }
}