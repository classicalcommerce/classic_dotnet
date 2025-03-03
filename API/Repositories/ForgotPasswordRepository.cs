using API.Intefaces;

namespace API.Repositories;

/// <summary>
/// Repository for handling forgot password-related database operations.
/// </summary>
/// <param name="logger">Logger instance for logging information and errors.</param>
/// <param name="context">Database context for accessing email validation data.</param>
public class ForgotPasswordRepository(ILogger<ForgotPasswordRepository> logger, IContext context) : IForgotPasswordRepository
{
    private readonly ILogger<ForgotPasswordRepository> _logger = logger;
    private readonly IContext _context = context;

    /// <summary>
    /// Checks if the given email exists in the database.
    /// </summary>
    /// <param name="email">The email address to check.</param>
    /// <returns>
    /// <c>true</c> if the email exists in the database; otherwise, <c>false</c>.
    /// </returns>
    /// <exception cref="Exception">Throws an exception if an error occurs while accessing the database.</exception>
    public bool CheckIfEmailExists(string email)
    {
        _logger.LogInformation("Controller: {Controller} - Action: {Action}", nameof(ForgotPasswordRepository), nameof(CheckIfEmailExists));
        try
        {
            bool emailExist = _context.ValidateEmail(email);
            _logger.LogInformation("Email validation result for {Email}: {Result}", email, emailExist);
            return emailExist;
        }
        catch(Exception e)
        {
            _logger.LogError(e, "Controller: {Controller} - Action: {Action}", nameof(ForgotPasswordRepository), nameof(CheckIfEmailExists));
            throw;
        }
    }
}