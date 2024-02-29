using Zurvan.Core.Interfaces;
using Zurvan.Core.UserFactory.UserTypes;

namespace Zurvan.Core.UserFactory
{
    public class UserCreator : IUserCreator
    {
        public IUser NewUser(string type)
        {
            switch (type)
            {
                case "Employee":
                    return new Employee();

                case "Lead":
                    return new Lead();

                case "Administrator":
                    return new Administrator();

                case "Manager":
                    return new Manager();
                default:
                    throw new ArgumentException("Unsupported UserType: ", type);
            }
            
        }
    }
}