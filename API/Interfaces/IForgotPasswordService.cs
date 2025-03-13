namespace API.Intefaces;

public interface IForgotPasswordService
{
    bool CheckIfEmailExists(string email);
}