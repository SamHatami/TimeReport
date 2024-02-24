using Zurvan.Core.Interfaces;
using Zurvan.Core.UserTypes;

namespace Zurvan.Core
{
    internal class UserCreator : IUserCreator
    {
        public IUser NewUser(UserType type)
        {
            switch (type)
            {
                case UserType.Employee:
                    return new Employee();

                case UserType.Lead:
                    return new Lead();

                case UserType.Administrator:
                    return new Administrator();

                case UserType.Manager:
                    return new Manager();
            }

            return null;
        }
    }
}