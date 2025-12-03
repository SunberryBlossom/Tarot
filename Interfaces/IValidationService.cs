namespace TheSeer.Interfaces
{
    public interface IValidationService
    {
        bool IsValidUsername(string username, out string? errorMessage);
        bool IsValidPassword(string password, out string? errorMessage);
        bool IsValidEmail(string email, out string? errorMessage);
    }
}