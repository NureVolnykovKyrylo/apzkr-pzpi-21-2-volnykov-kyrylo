using AquaTrack.Models;
using AquaTrack.Repositories.Interfaces;
using AquaTrack.Services.Interfaces;
using AquaTrack.Utils;
using AquaTrack.ViewModels;
using AutoMapper;

namespace AquaTrack.Services
{
    public class AuthentificationService : IAuthentificationService
    {
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AuthentificationService(
            IMapper mapper,
            IUserRepository userRepository,
            IHttpContextAccessor httpContextAccessor)
        {
            _mapper = mapper;
            _userRepository = userRepository;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<User?> GetCurrentUser()
        {
            var currentUserId = _httpContextAccessor.HttpContext?.Request.Cookies["userId"];
            if (currentUserId == null || !int.TryParse(currentUserId, out int userId))
            {
                return null;
            }

            var user = await _userRepository.GetUserByIdAsync(userId);
            return user;
        }

        public async Task<UserViewModel> Login(string email, string password)
        {
            var user = await _userRepository.GetUserByEmailAsync(email);

            if (user == null || !BCrypt.Net.BCrypt.Verify(password, user.Password))
            {
                return null;
            }

            var cookieOptions = new CookieOptions
            {
                Expires = DateTimeOffset.Now.AddDays(7)
            };

            _httpContextAccessor.HttpContext.Response.Cookies.Append("role", user.Role.ToString(), cookieOptions);
            _httpContextAccessor.HttpContext.Response.Cookies.Append("userId", user.UserId.ToString(), cookieOptions);

            var userViewModel = _mapper.Map<UserViewModel>(user);
            return userViewModel;
        }

        public async Task<bool> Logout()
        {
            if (_httpContextAccessor.HttpContext == null)
            {
                return false;
            }

            _httpContextAccessor.HttpContext.Response.Cookies.Delete("role");
            _httpContextAccessor.HttpContext.Response.Cookies.Delete("userId");
            return true;
        }

        public async Task<UserViewModel> RegisterUser(UserRegisterViewModel model)
        {
            await Validate(_mapper.Map<UserViewModel>(model), true);
            model.Password = BCrypt.Net.BCrypt.HashPassword(model.Password);

            var user = _mapper.Map<User>(model);
            user.Role = Role.User;

            var createUserResult = await _userRepository.AddUserAsync(user);

            var cookieOptions = new CookieOptions
            {
                Expires = DateTimeOffset.Now.AddDays(7)
            };

            _httpContextAccessor.HttpContext.Response.Cookies.Append("role", user.Role.ToString(), cookieOptions);
            _httpContextAccessor.HttpContext.Response.Cookies.Append("userId", user.UserId.ToString(), cookieOptions);

            var userViewModel = _mapper.Map<UserViewModel>(createUserResult);
            return userViewModel;
        }

        public async Task<UserViewModel> UpdateUserInfo(UserUpdateViewModel model)
        {
            var user = await GetCurrentUser();
            if (user == null)
            {
                return null;
            }

            _mapper.Map(model, user);

            await Validate(_mapper.Map<UserViewModel>(user), false);

            user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);

            var updateResult = await _userRepository.UpdateUserAsync(user);
            if (updateResult == null)
            {
                return null;
            }

            var updatedUserViewModel = _mapper.Map<UserViewModel>(updateResult);
            return updatedUserViewModel;
        }

        public async Task<UserViewModel?> GetCurrentUserInfo()
        {
            var user = await GetCurrentUser();
            if (user == null)
            {
                return null;
            }

            var userViewModel = _mapper.Map<UserViewModel>(user);
            return userViewModel;
        }

        public async Task Validate(UserViewModel user, bool isNewUser)
        {
            if (string.IsNullOrEmpty(user.Email))
            {
                throw new ArgumentException("Email is required.");
            }

            if (isNewUser)
            {
                var existingUser = await _userRepository.GetUserByEmailAsync(user.Email);
                if (existingUser != null)
                {
                    throw new ArgumentException("Email is already registered.");
                }

                if (string.IsNullOrEmpty(user.Password))
                {
                    throw new ArgumentException("Password is required.");
                }

                if (user.Password.Length < 6)
                {
                    throw new ArgumentException("Password should be at least 6 characters long.");
                }
            }

            if (string.IsNullOrEmpty(user.FirstName))
            {
                throw new ArgumentException("First name is required.");
            }

            if (!IsValidEmail(user.Email))
            {
                throw new ArgumentException("Invalid email format.");
            }
        }

        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        public async Task<List<UserViewModel>> GetAllUsers()
        {
            var userRole = UserAccessUtil.GetCurrentUserRole(_httpContextAccessor);
            //if (userRole != Role.Admin)
            //{
            //    return null;
            //}

            var users = await _userRepository.GetAllUsersAsync();
            var userViewModels = _mapper.Map<List<UserViewModel>>(users);
            return userViewModels;
        }

        public async Task<bool> DeleteUser(int userId)
        {
            var userRole = UserAccessUtil.GetCurrentUserRole(_httpContextAccessor);
            if (userRole != Role.Admin)
            {
                return false;
            }

            var user = await _userRepository.GetUserByIdAsync(userId);
            if (user == null)
            {
                return false;
            }

            await _userRepository.DeleteUserAsync(userId);
            return true;
        }

        public async Task<UserViewModel> AddUser(UserUtilityViewModel model)
        {
            var userRole = UserAccessUtil.GetCurrentUserRole(_httpContextAccessor);
            if (userRole != Role.Admin)
            {
                return null;
            }

            var existingUser = await _userRepository.GetUserByEmailAsync(model.Email);
            if (existingUser != null)
            {
                throw new ArgumentException("Email is already registered.");
            }

            var user = _mapper.Map<User>(model);
            user.Password = BCrypt.Net.BCrypt.HashPassword(model.Password);
            user.Role = Role.User;

            var createdUser = await _userRepository.AddUserAsync(user);
            var userViewModel = _mapper.Map<UserViewModel>(createdUser);
            return userViewModel;
        }

        public async Task<UserViewModel> UpdateUser(int userId, UserUtilityViewModel model)
        {
            var userRole = UserAccessUtil.GetCurrentUserRole(_httpContextAccessor);
            //if (userRole != Role.Admin)
            //{
            //    return null;
            //}

            var user = await _userRepository.GetUserByIdAsync(userId);
            if (user == null)
            {
                return null;
            }

            _mapper.Map(model, user);

            user.Password = BCrypt.Net.BCrypt.HashPassword(model.Password);

            var updatedUser = await _userRepository.UpdateUserAsync(user);
            var updatedUserViewModel = _mapper.Map<UserViewModel>(updatedUser);
            return updatedUserViewModel;
        }

        public async Task<UserViewModel> GetUserById(int userId)
        {
            var user = await _userRepository.GetUserByIdAsync(userId);
            if (user == null)
            {
                return null;
            }

            var userViewModel = _mapper.Map<UserViewModel>(user);
            return userViewModel;
        }
    }
}
