using Zurvan.Core.Interfaces;
using Zurvan.Core.UserFactory.UserTypes;

namespace Zurvan.Core.UserFactory
{
    public class UserCreator : IUserCreator
    {
        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();
        public IUser NewUser(string type)
        {
            switch (type)
            {
                case "Employee":
                    Logger.Info("Created a new Employee");
                    return new Employee();

                case "Lead":
                    Logger.Info("Created a new Lead");
                    return new Lead();

                case "Administrator":
                    Logger.Info("Created a new Adminstrator");
                    return new Administrator();

                case "Manager":
                    Logger.Info("Created a new Manager");
                    return new Manager();
                default:
                    throw new ArgumentException("Unsupported UserType: ", type);
            }
            
        }
    }
}