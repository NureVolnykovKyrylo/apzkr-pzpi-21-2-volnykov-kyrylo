using AquaTrack.Models;
using AquaTrack.ViewModels;

namespace AquaTrack.Services.Interfaces
{
    public interface IAuthentificationService
    {
        Task<UserViewModel> Login(string email, string password);
        Task<bool> Logout();
        Task<UserViewModel> RegisterUser(UserRegisterViewModel model);
        Task<UserViewModel> UpdateUserInfo(UserUpdateViewModel model);
        Task<UserViewModel?> GetCurrentUserInfo();
        Task<User?> GetCurrentUser();
        Task<List<UserViewModel>> GetAllUsers();
        Task<bool> DeleteUser(int userId);
        Task<UserViewModel> AddUser(UserUtilityViewModel model);
        Task<UserViewModel> UpdateUser(int userId, UserUtilityViewModel model);
        Task<UserViewModel> GetUserById(int userId);
    }
}
