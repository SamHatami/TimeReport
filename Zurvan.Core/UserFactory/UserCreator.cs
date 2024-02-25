using Zurvan.Core.Interfaces;
using Zurvan.Core.UserFactory.UserTypes;

namespace Zurvan.Core.UserFactory
{
    public class UserCreator : IUserCreator
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
                default:
                    throw new ArgumentException("Unsupported UserType", nameof(type));
            }
            
        }
    }
}