using Zurvan.Core.UserFactory.UserTypes;

namespace Zurvan.Core.Interfaces
{
    internal interface IUserCreator
    {
        IUser NewUser(UserType type);
    }
}