namespace Application.Interfaces
{
    public interface IRegister
    {
        bool Register(string[] fields);
        bool DoesEmailExist(string emailAddress);
    }
}
