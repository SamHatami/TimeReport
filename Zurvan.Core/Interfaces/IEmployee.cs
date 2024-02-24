namespace Zurvan.Core.Interfaces
{
    public interface IEmployee : IUser
    {
        List<IProject> GetProjects { get; }
    }
}