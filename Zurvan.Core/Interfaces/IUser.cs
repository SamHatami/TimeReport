using Zurvan.Core.UserFactory.UserTypes;

namespace Zurvan.Core.Interfaces
{
    public interface IUser
    {
        string FirstName { get; set; }
        string LastName { get; set; }
        string Email { get; set; }
        int UserId { get; set; }

        UserType Type { get; set; }
    }
}