using API.Intefaces;

namespace API.Services;

/// <summary>
/// Service for handling forgot password-related operations.
/// </summary>
/// <param name="logger">Logger instance for logging information and errors.</param>
/// <param name="repository">Repository instance for accessing data operations.</param>
public class ForgotPasswordService(ILogger<ForgotPasswordService> logger, IForgotPasswordRepository repository) : IForgotPasswordService
{
    private readonly ILogger<ForgotPasswordService> _logger = logger;
    private readonly IForgotPasswordRepository _repository = repository;

    /// <summary>
    /// Checks if the given email exists in the system.
    /// </summary>
    /// <param name="email">The email address to check.</param>
    /// <returns>
    /// <c>true</c> if the email exists; otherwise, <c>false</c>.
    /// </returns>
    /// <exception cref="Exception">Throws an exception if an error occurs while accessing the repository.</exception>
    public bool CheckIfEmailExists(string email)
    {
        _logger.LogInformation("Controller: {Controller} - Action: {Action}", nameof(ForgotPasswordService), nameof(CheckIfEmailExists));
        try
        {
            bool emailExist = _repository.CheckIfEmailExists(email);
            return emailExist;
        }
        catch(Exception e)
        {
            _logger.LogError(e, "Controller: {Controller} - Action: {Action}", nameof(ForgotPasswordService), nameof(CheckIfEmailExists));
            throw;
        }
    }
}