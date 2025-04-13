using System.Security.Claims;
using dotnetTest.Repositories;

namespace dotnetTest.Controllers;

using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

public class BaseController : Controller
{
    protected readonly UserRepository _userRepository;

    public BaseController(UserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    protected async Task<string> GetUserProfilePhoto()
    {
        if (User?.Identity?.IsAuthenticated != true || string.IsNullOrEmpty(User.Identity.Name))
        {
            return string.Empty;
        }

        var user = await _userRepository.GetByUsernameAsync(User.Identity.Name);
        return user?.ProfilePhotoUrl ?? string.Empty;
    }
} 