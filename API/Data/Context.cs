using API.Intefaces;

namespace API.Data;
public class Context: IContext
{
    private const string _email = "naren000000000@gmail.com";
    
    public bool ValidateEmail(string email) => _email == email;
}