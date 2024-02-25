using Zurvan.Core.Interfaces;

namespace Zurvan.Core.UserFactory.UserTypes
{
    internal class Employee : IUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public int UserId { get; set; }
        public UserType Type { get; set; }
        public List<IProject> GetProjects { get; }
    }
}