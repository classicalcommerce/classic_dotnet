using API.Filters;
using API.Intefaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

/// <summary>
/// Controller for handling forgot password-related requests.
/// </summary>
/// <param name="service">The forgot password service for processing requests.</param>
[ApiController]
[Route("[controller]")]
public class ForgotPasswordController(IForgotPasswordService service) : ControllerBase
{
    private readonly IForgotPasswordService _service = service;

     /// <summary>
    /// Checks if the given email exists in the system.
    /// </summary>
    /// <param name="email">The email address to check.</param>
    /// <returns>
    /// Returns <c>200 OK</c> if the email exists, 
    /// <c>404 Not Found</c> if the email does not exist, 
    /// and <c>500 Internal Server Error</c> in case of an exception.
    /// </returns>
    /// <response code="200">Email exists.</response>
    /// <response code="404">Email not found.</response>
    /// <response code="500">Internal server error.</response>
    [ServiceFilter(typeof(LoggingActionFilter))]
    [HttpGet("check-email", Name = "Validate Email")]
    public IActionResult CheckIfEmailExists([FromQuery] string email)
    {
        try
        {
            bool emailExist = _service.CheckIfEmailExists(email);
            return emailExist ? Ok(emailExist) : NotFound(emailExist);
        }
        catch(Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, e);
        }
    }
}