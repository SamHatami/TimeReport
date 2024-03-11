using Caliburn.Micro;
using System.Windows.Controls;
using System.Windows.Data;
using Zurvan.ClientApp.Views;
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

        public List<string> SelectedDates;

        public BindableCollection<DateTimeData> DateTimeData { get; set; }

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

            SelectedDates = new List<string>()
            {
                "2024-01-02", "2024-01-01", "2024-01-03", "2024-01-04", "2024-01-05"
            };

            SortDate(SelectedDates);
            //Update(selecteDates);
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

        protected override void OnViewAttached(object view, object context)
        {
            base.OnViewAttached(view, context);

            if (view is not UserView userView)
                return;

            // Building up columns
            foreach (var project in Projects)
            {
                foreach (var reportedTime in project.UserDateTimeReported)
                {
                    if (SelectedDates.Contains(reportedTime.Key))
                    {
                        var column = new DataGridTextColumn()
                        {
                            Header = reportedTime.Key,
                            Binding = new Binding(nameof(project.UserDateTimeReported) + "[" + reportedTime.Key + "]"),
                            IsReadOnly = false
                        };

                        userView.TimeReport.Columns.Add(column);
                    }
                }

            }
        }

        private void UpdateColumns()
        {

        }
    }
}