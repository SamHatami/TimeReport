using System.ComponentModel;
using System.Net.Mime;
using System.Windows;
using Caliburn.Micro;
using System.Windows.Controls;
using System.Windows.Data;
using Zurvan.ClientApp.Views;
using Zurvan.Core.Interfaces;
using Zurvan.Core.TimeModels;
using System.Xml.Linq;
using System.Linq;
using Zurvan.Core.Projects;

namespace Zurvan.ClientApp.ViewModels
{
    public class UserViewModel : Screen
    {
        private IDataBaseService _dataBaseService;

        public IUser User { get;}

        private BindableCollection<IProject> _projects;

        public BindableCollection<UserProjectViewModel> ProjectViewModels { get; set; }

        public BindableCollection<IProject> Projects
        {
            get => _projects;

            set
            {
                _projects = value;
                NotifyOfPropertyChange(() => Projects);
                Projects.Refresh();
                SomethingWasChanged();
            }
        }
        

        public BindableCollection<DateTimeData> DateTime { get; set; }

        private string _something;
        public string Something
        {
            get => _something;
            set
            {
                _something = value;
                NotifyOfPropertyChange(() => Something);
                SomethingWasChanged();
            }
        }

        public List<string> SelectedDates;

        public UserViewModel(IDataBaseService dataBaseService, int userId)
        {
            SelectedDates = new List<string>()
            {
                "2024-01-02", "2024-01-01", "2024-01-03", "2024-01-04", "2024-01-05"
            };

            _dataBaseService = dataBaseService;
            User = _dataBaseService.GetUser(userId);
            _projects = new BindableCollection<IProject>(dataBaseService.GetUserProjects(userId));
            ProjectViewModels = new BindableCollection<UserProjectViewModel>();
            
            
            SortDate(SelectedDates);
            Update(SelectedDates);
        }

        public void SortDate(List<string> SelectedDates)
        {
            SelectedDates.Sort();
        }

        public void SomethingWasChanged()
        {
            MessageBox.Show("Somthing was changed to :" + Something);
        }
        public void Update(List<string> showDates)
        {
            foreach (var project in Projects)
            {
                List<DateTimeData> timeReportedAtSelectedDates = new List<DateTimeData>();

                UserProjectViewModel uwms = new UserProjectViewModel(project, SelectedDates, _dataBaseService, User.UserId);
                ProjectViewModels.Add(uwms);
            }
        }

        public void LogOut()
        {
           
            
        }

    }
}