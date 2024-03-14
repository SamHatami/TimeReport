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

            foreach (var project in Projects)
            {
                project.UserDateTimeReported = _dataBaseService.GetReportedTimePerUser(project.id, userId, SelectedDates); //detta ska inte viewmodel hantera
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
                List<DateTimeData> TimeReportedAtSelectedDates = new List<DateTimeData>();

                //foreach (string date in showDates)
                //{
                //    int hours;
                //    project.UserDateTimeReported.Find();
                //    TimeReportedAtSelectedDates.Add(new DateTimeData(date, hours));
                //}

                UserProjectViewModel uwms = new UserProjectViewModel(project.Name, project.UserDateTimeReported);
                ProjectViewModels.Add(uwms);
            }
        }

        protected override void OnViewAttached(object view, object context)
        {
            base.OnViewAttached(view, context);

            //if (view is not UserView userView)
            //    return;

            ////Building up columns
            //foreach (var project in Projects)
            //{
            //    foreach (var reportedTime in project.UserDateTimeReported)
            //    {
            //            var column = new DataGridTextColumn()
            //            {

            //                Header = reportedTime.Date,
            //                Binding = new Binding(nameof(project.UserDateTimeReported) + "."+ reportedTime.TimeUsed)
            //                {
            //                    Mode = BindingMode.TwoWay
            //                },
            //                IsReadOnly = false,
            //                Width = 100,
            //            };

            //            userView.TimeReport.Columns.Add(column);

            //    }
                

            //}
        }

        public void UpdateDataBase(object t)
        {
            MessageBox.Show(t.ToString());
        }
    }
}