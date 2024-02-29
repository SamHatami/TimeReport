namespace Zurvan.Core.Interfaces
{
    public interface ILead : IUser
    {
        List<IUser> TeamMembers { get; }
    }
}