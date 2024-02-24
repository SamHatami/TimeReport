using Zurvan.Core.Interfaces;

namespace Zurvan.Core.UserTypes
{
    internal class Administrator : IAdministrator
    {
        public string FirstName { get; }
        public string LastName { get; }
        public string Email { get; set; }
        public int UserId { get; set; }
        public UserType Type { get; set; }

        public void SomethingUniqueToAmin()
        {
            throw new NotImplementedException();
        }
    }
}