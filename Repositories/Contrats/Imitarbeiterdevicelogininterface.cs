using Shared.Project.DTOs;
using Shared.Project.Entities;
using Shared.Project.Responses;
namespace Server.Repositories.Contracts
{
    public interface Imitarbeiterdevicelogininterface
    {
        Task<LoginResponse> SignInAsync(Login user);
        Task<bool> UpdateProfile(UserProfile profile);
    }
}
