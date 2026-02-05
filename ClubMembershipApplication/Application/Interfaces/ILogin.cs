using Domain.Models;

namespace Application.Interfaces
{
    public interface ILogin
    {
        User Login(string emailAddress, string password);
    }
}