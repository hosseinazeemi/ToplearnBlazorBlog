namespace TB.UI.Services.AuthService
{
    public interface IJWTAuthService
    {
        Task Login(string token);
        Task Logout();
    }
}
