using System.Runtime.InteropServices.JavaScript;
using Caliburn.Micro;
using System.Windows;
using System.Windows.Markup;
using Zurvan.Core.Interfaces;
using Zurvan.Core.TimeModels;
using Zurvan.Core.Projects;
using static System.Runtime.InteropServices.JavaScript.JSType;
using String = System.String;

namespace Zurvan.ClientApp.ViewModels
{
    public class UserReportProjectViewModel : Screen
    {
        private List<string> _selectedDates;

        private IDataBaseService _database;

        private HourReportData _monday;

        public int Monday
        {
            get => _monday.TimeUsed;
            set
            {
                _monday.TimeUsed = value;
                NotifyOfPropertyChange(() => Monday);
                UpdateDataBase(_monday);
            }
        }

        private HourReportData _tuesday;

        public int Tuesday
        {
            get => _tuesday.TimeUsed;
            set
            {
                _tuesday.TimeUsed = value;
                NotifyOfPropertyChange(() => Tuesday);
                UpdateDataBase(_tuesday);
            }
        }

        private HourReportData _wednesday;

        public int Wednesday
        {
            get => _wednesday.TimeUsed;
            set
            {
                _wednesday.TimeUsed = value;
                NotifyOfPropertyChange(() => Wednesday);
                UpdateDataBase(_wednesday);
            }
        }

        private HourReportData _thursday;

        public int Thursday
        {
            get => _thursday.TimeUsed;
            set
            {
                _thursday.TimeUsed = value;
                NotifyOfPropertyChange(() => Thursday);
                UpdateDataBase(_thursday);
            }
        }

        private HourReportData _friday;

        public int Friday
        {
            get => _friday.TimeUsed;
            set
            {
                _friday.TimeUsed = value;
                NotifyOfPropertyChange(() => Friday);
                UpdateDataBase(_friday);
            }
        }

  
        public int ProjectTotal
        {
            get => _project.UsedTime;
            set
            {
                NotifyOfPropertyChange(() => ProjectTotal);

            }
        }

        private IProject _project;
        private int _userId;

        public string ProjectName { get; set; }

        public UserReportProjectViewModel(IProject project, List<string> selectedDates, IDataBaseService dataBase, int userId)
        {
            _userId = userId;
            _database = dataBase;
            _project = project;
            _selectedDates = selectedDates;
            ProjectName = project.Name;

            project.UserReportedData = _database.GetReportedTimePerUser(project.id, userId, _selectedDates);

            InitializeWeekDays();
        }

        private void InitializeWeekDays()
        {
            _monday = new HourReportData();
            _tuesday = new HourReportData();
            _wednesday = new HourReportData();
            _thursday = new HourReportData();
            _friday = new HourReportData();
            ProjectTotal = _project.UsedTime;

            DateTime dateValue;
            foreach (string thisDate in _selectedDates)
            {
                dateValue = DateTime.Parse(thisDate);
                int timeUsed = _project.UserReportedData.Where(x => !string.IsNullOrEmpty(x.Date)).Where(x => x.Date == thisDate).Select(x => x.TimeUsed).SingleOrDefault();

                switch (dateValue.DayOfWeek.ToString())
                {
                    case "Monday":
                        _monday = new HourReportData(timeUsed: timeUsed, date: thisDate, weekDay: "Monday");
                        break;

                    case "Tuesday":
                        _tuesday = new HourReportData(timeUsed: timeUsed, date: thisDate, weekDay: "Tuesday");
                        break;

                    case "Wednesday":
                        _wednesday = new HourReportData(timeUsed: timeUsed, date: thisDate, weekDay: "Wednesday");
                        break;

                    case "Thursday":
                        _thursday = new HourReportData(timeUsed: timeUsed, date: thisDate, weekDay: "Thursday");
                        break;

                    case "Friday":
                        _friday = new HourReportData(timeUsed: timeUsed, date: thisDate, weekDay: "Friday");
                        break;
                }
            }

            //foreach (string date in selectedDates)
            //{
            //    DateTimeData dateData = project.UserDateTimeReported.SingleOrDefault(x => x.Date == date);
            //    if (dateData != null)
            //    {
            //        switch (dateData?.WeekDay)
            //        {
            //            case "Monday":
            //                _monday.TimeUsed = dateData.TimeUsed;
            //                break;
            //            case "Tuesday":
            //                _tuesday.TimeUsed = dateData.TimeUsed;
            //                break;
            //            case "Wednesday":
            //                _wednesday.TimeUsed = dateData.TimeUsed;
            //                break;
            //            case "Thursday":
            //                _thursday.TimeUsed = dateData.TimeUsed;
            //                break;
            //            case "Friday":
            //                _friday.TimeUsed = dateData.TimeUsed;
            //                break;
            //        }
            //    }

            //}
        }

        public void UpdateDataBase(HourReportData date)
        {
            _database.UpdateProjectByDateAndUser(_project.id, _userId, date.Date, date.TimeUsed);
            
        }
    }
}