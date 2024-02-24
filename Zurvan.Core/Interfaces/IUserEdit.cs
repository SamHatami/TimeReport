namespace Zurvan.Core.Interfaces
{
    internal interface IUserEdit
    {
        void EditName(string firstName, string LastName);

        void AddProjectsMembership(int[] projects);
    }
}