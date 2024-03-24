using Caliburn.Micro;
using Zurvan.Core.Interfaces;
using Zurvan.Core.TimeModels;

namespace Zurvan.ClientApp.ViewModels
{
    public class UserReportProjectViewModel : Screen
    {
        private IDataBaseService _database;
        private IProject _project;
        private int _userId;

        public List<string> SelectedDates;
        public string ProjectName { get; set; }
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

        public UserReportProjectViewModel(IProject project, List<string> selectedDates, IDataBaseService dataBase, int userId)
        {
            _userId = userId;
            _database = dataBase;
            _project = project;
            SelectedDates = selectedDates;
            ProjectName = project.Name;

            _monday = new HourReportData();
            _tuesday = new HourReportData();
            _wednesday = new HourReportData();
            _thursday = new HourReportData();
            _friday = new HourReportData();

            UpdateWeek();
        }

        public void UpdateWeek()
        {
            _project.UserReportedData = _database.GetReportedTimePerUser(_project.id, _userId, SelectedDates);
            ProjectTotal = _project.UsedTime;

            DateTime dateValue;
            foreach (string thisDate in SelectedDates)
            {
                dateValue = DateTime.Parse(thisDate);
                int timeUsed = _project.UserReportedData.Where(x => !string.IsNullOrEmpty(x.Date)).Where(x => x.Date == thisDate).Select(x => x.TimeUsed).SingleOrDefault();

                switch (dateValue.DayOfWeek.ToString())
                {
                    case "Monday":
                        _monday = new HourReportData(timeUsed: timeUsed, date: thisDate, weekDay: "Monday");
                        Monday = _monday.TimeUsed;
                        break;

                    case "Tuesday":
                        _tuesday = new HourReportData(timeUsed: timeUsed, date: thisDate, weekDay: "Tuesday");
                        Tuesday = _tuesday.TimeUsed;
                        break;

                    case "Wednesday":
                        _wednesday = new HourReportData(timeUsed: timeUsed, date: thisDate, weekDay: "Wednesday");
                        Wednesday = _wednesday.TimeUsed;
                        break;

                    case "Thursday":
                        _thursday = new HourReportData(timeUsed: timeUsed, date: thisDate, weekDay: "Thursday");
                        Thursday = _thursday.TimeUsed;
                        break;

                    case "Friday":
                        _friday = new HourReportData(timeUsed: timeUsed, date: thisDate, weekDay: "Friday");
                        Friday = _friday.TimeUsed;
                        break;
                }
            }
        }

        public void UpdateDataBase(HourReportData date)
        {
            _database.UpdateProjectByDateAndUser(_project.id, _userId, date.Date, date.TimeUsed); // TODO Hanteras på något annat sätt?
            
        }

        public void GetTotalTime()
        {
            //TODO gör en ny klass som hanterar detta istället för totaltimeviewModel
        }
    }
}