using MVCApp.Interfaces;

namespace MVCApp.Services;

using Microsoft.AspNetCore.Http;

/*
 * Student name: Carlos Sucapuca
 * ID: 71930
 * Project: Banking MVCApp
 * Lecture: John Rowley
 */

public class CurrentUserService : Interfaces.CurrentUserService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CurrentUserService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public int? GetUserId()
    {
        return _httpContextAccessor.HttpContext!.Session.GetInt32("UserId");
    }
}