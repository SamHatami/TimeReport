namespace Zurvan.Core.Interfaces
{
    internal interface IUserController
    {
        IUserCreator NewUser(UserType type);

        IUser GetUser(int userId);

        IUserEdit EditUser(IUser user);

        void DeleteUser(int userId);
    }
}