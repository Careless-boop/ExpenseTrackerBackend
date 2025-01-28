namespace ExpenseTracker.Application.Common.Interfaces.Jwt
{
    public interface IJwtTokenService
    {
        Task<string?> GenerateTokenAsync(string userId, IList<string> roles);
    }
}
