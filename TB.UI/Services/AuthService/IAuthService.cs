namespace TB.UI.Services.AuthService
{
    public interface IAuthService
    {
        Task Login(string token);
        Task Logout();
    }
}
