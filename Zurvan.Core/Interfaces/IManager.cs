namespace Zurvan.Core.Interfaces;

public interface IManager :IUser
{
    string DepartmentId { get; set; }
    string Department { get; set; }
    public List<IUser> DepartmentMembers { get; set; }
}