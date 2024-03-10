using Caliburn.Micro;
using Zurvan.Core.Interfaces;
using Zurvan.Core.TimeModels;

namespace Zurvan.ClientApp.ViewModels
{
    public class UserViewModel : Screen
    {
        private IDataBaseService _dataBaseService;

        public IUser User { get; }

        private BindableCollection<IProject> _projects;

        public BindableCollection<UserProjectViewModel> ProjectViewModels { get; }

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
            ProjectViewModels = new BindableCollection<UserProjectViewModel>();

            foreach (var project in Projects)
            {
                project.UserDateTimeReported = _dataBaseService.GetReportedTimePerUser(project.id, userId);
            }

            List<string> selecteDates = new List<string>()
            {
                "2024-01-02", "2024-01-01","2024-01-03","2024-01-04","2024-01-05"
            };

            SortDate(selecteDates);
            Update(selecteDates);
        }

        public void SortDate(List<string> SelectedDates)
        {
            SelectedDates.Sort();
        }

        public void Update(List<string> showDates)
        {
            foreach (var project in Projects)
            {
                List<DateTimeData> TimeReportedAtSelectedDates = new List<DateTimeData>();

                foreach (string date in showDates)
                {
                    int hours;
                    project.UserDateTimeReported.TryGetValue(date, out hours);
                    TimeReportedAtSelectedDates.Add(new DateTimeData(date, hours));
                }
                UserProjectViewModel pvm = new UserProjectViewModel(project.Name, TimeReportedAtSelectedDates);
                ProjectViewModels.Add(pvm);
            }
        }
    }
}