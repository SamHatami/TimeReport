namespace Zurvan.Core.Interfaces
{
    public interface IProject
    {
        string Name { get; }
        string Description { get; }
        int id { get; set; }
        List<IUser> Members { get; }

        int ShowTotalTimeUsed();
    }
}