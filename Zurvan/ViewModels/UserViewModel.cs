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

        private BindableCollection<UserReportProjectViewModel> _projectViewModels;
        public BindableCollection<UserReportProjectViewModel> ProjectViewModels 
        { get => _projectViewModels;
            set
            {
                _projectViewModels = value;
                NotifyOfPropertyChange(() => ProjectViewModels);
            }
        }

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
        

        public BindableCollection<HourReportData> DateTime { get; set; }

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
            _projectViewModels = new BindableCollection<UserReportProjectViewModel>();

            foreach (var project in Projects)
            {
                
            }
            
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
                List<HourReportData> timeReportedAtSelectedDates = new List<HourReportData>();

                UserReportProjectViewModel uwms = new UserReportProjectViewModel(project, SelectedDates, _dataBaseService, User.UserId);
                _projectViewModels.Add(uwms);
     
            }
        }

        public void LogOut()
        {
            
        }


    }
}