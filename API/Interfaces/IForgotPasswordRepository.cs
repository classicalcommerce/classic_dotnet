namespace API.Intefaces;

public interface IForgotPasswordRepository
{
    bool CheckIfEmailExists(string email);
}