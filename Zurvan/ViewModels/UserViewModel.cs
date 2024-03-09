using System.Windows.Documents;
using Caliburn.Micro;
using Zurvan.Core.Interfaces;
using Zurvan.Core.Projects;

namespace Zurvan.ClientApp.ViewModels
{
    public class UserViewModel : Screen
    {
        private IDataBaseService _dataBaseService;

        public IUser User { get;}

        private BindableCollection<IProject> _projects;

        public BindableCollection<IProject> Projects
        {

            get => _projects;

            set
            {
                _projects = value;
                NotifyOfPropertyChange(() => Projects);
            }
        }
        public UserViewModel(IDataBaseService dataBaseService, int userId)
        {
            _dataBaseService = dataBaseService;
            User = _dataBaseService.GetUser(userId);
            _projects = new BindableCollection<IProject>(dataBaseService.GetUserProjects(userId));

            foreach (var project in Projects)
            {
                project.UserDateTimeReported = _dataBaseService.GetReportedTimePerUser(project.id, userId);
            }
        }


    }
}