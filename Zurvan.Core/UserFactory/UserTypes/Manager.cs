using Zurvan.Core.Interfaces;

namespace Zurvan.Core.UserFactory.UserTypes
{
    internal class Manager : IManager
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public int UserId { get; set; }
        public UserType Type { get; set; }
        public List<IProject> Projects { get; set; }
        public string DepartmentId { get; set; }
        public string Department { get; set; }
        public List<IUser> DepartmentMembers { get; set; }
    }
}