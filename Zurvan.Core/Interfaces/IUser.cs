namespace Zurvan.Core.Interfaces
{
    public interface IUser
    {
        string FirstName { get; }
        string LastName { get; }
        string Email { get; set; }
        int UserId { get; set; }

        UserType Type { get; set; }
    }
}