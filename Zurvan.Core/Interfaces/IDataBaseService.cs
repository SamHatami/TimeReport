using Zurvan.Core.TimeModels;

namespace Zurvan.Core.Interfaces
{
    public interface IDataBaseService
    {
        List<IUser> GetAllUsers();

        List<IProject> GetAllProjects();

        IUser GetUser(int id);

        IProject GetProject(int id);

        List<IProject> GetUserProjects(int userId);

        IUser? Login(string name, string password);

        List<DateTimeData> GetReportedTimePerUser(int projectId, int userId, List<string> selectedDates);

        void UpdateProjectByDateAndUser(int projectId, int userId, string date, int updatedTime);
    }
}