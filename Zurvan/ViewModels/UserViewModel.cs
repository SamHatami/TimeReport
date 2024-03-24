using Caliburn.Micro;
using Zurvan.ClientApp.Models;
using Zurvan.Core.Interfaces;
using Zurvan.Core.TimeModels;

namespace Zurvan.ClientApp.ViewModels
{
    public class UserViewModel : Screen
    {
        private IDataBaseService _dataBaseService;
        private WeekDateCreator _weekDayCreator;
        public IUser User { get; }

        private UserTotalTimeViewModel _userTotalTimeViewModel;

        private BindableCollection<IProject> _projects;

        private BindableCollection<UserReportProjectViewModel> _projectViewModels;

        public BindableCollection<UserReportProjectViewModel> ProjectViewModels
        {
            get => _projectViewModels;
            set
            {
                _projectViewModels = value;
                NotifyOfPropertyChange(() => ProjectViewModels);
                UpdateTotals();
            }
        }

        private BindableCollection<UserTotalTimeViewModel> _totalWeek;

        public BindableCollection<UserTotalTimeViewModel> TotalWeek
        {
            get => _totalWeek;
            set
            {
                _totalWeek = value;
                NotifyOfPropertyChange(() => TotalWeek);
            }
        }

        public List<string> SelectedDates;

        private int _currentWeek;

        public int CurrentWeek
        {
            get => _currentWeek;
            set
            {
                _currentWeek = value;
                NotifyOfPropertyChange(() => CurrentWeek);
            }
        }

        private string _nextWeek;

        public string NextWeek
        {
            get => _nextWeek;
            set
            {
                _nextWeek = value;
                NotifyOfPropertyChange(() => NextWeek);
            }
        }

        private string _previousWeek;

        public string PreviousWeek
        {
            get => _previousWeek;
            set
            {
                _previousWeek = value;
                NotifyOfPropertyChange(() => PreviousWeek);
            }
        }

        private string _weekIntervalDates;

        public string WeekIntervalDates
        {
            get => _weekIntervalDates;
            set
            {
                _weekIntervalDates = value;
                NotifyOfPropertyChange(() => WeekIntervalDates);
            }
        }

        private BindableCollection<string> _headerDayStrings;
        public BindableCollection<string> HeaderDayStrings
        {
            get => _headerDayStrings;
            set
            {
                _headerDayStrings = value;
                NotifyOfPropertyChange(() => HeaderDayStrings);
            }
        }



        public UserViewModel(IDataBaseService dataBaseService, int userId, TimeReportHandler reportHandler)
        {
            _weekDayCreator = new WeekDateCreator();
            _dataBaseService = dataBaseService;
            User = _dataBaseService.GetUser(userId);

            _projects = new BindableCollection<IProject>(dataBaseService.GetUserProjects(userId));
            _projectViewModels = new BindableCollection<UserReportProjectViewModel>();
            _totalWeek = new BindableCollection<UserTotalTimeViewModel>();
            _userTotalTimeViewModel = new UserTotalTimeViewModel(_projectViewModels.ToList(), SelectedDates,
                _dataBaseService, User.UserId);

            _headerDayStrings = new BindableCollection<string>();
            
            InitializeProjects();



            UpdateTotals();
        }

        private void InitializeProjects()
        {
            SelectedDates = _weekDayCreator.GetWeekDatesFromMonday(_weekDayCreator.GetCurrentWeekMonday(), WeekTraverse.This);
            _currentWeek = _weekDayCreator.GetCurrentWeekNr(DateTime.Today);
            HeaderDayStrings.AddRange(_weekDayCreator.GetHeaderDayStrings(SelectedDates));

            if (_projects.Count == 0)
            {
            }
            else
            {
                foreach (var project in _projects)
                {
                    UserReportProjectViewModel uwms = new UserReportProjectViewModel(project, SelectedDates, _dataBaseService, User.UserId); //kunna skickas in
                    _projectViewModels.Add(uwms);
                }

                _weekIntervalDates = _weekDayCreator.GetWeekIntervallText(SelectedDates[0]);
                _totalWeek.Add(_userTotalTimeViewModel);
                TotalWeek = _totalWeek;
            }
        }

        public void Update()
        {
            foreach (var pvm in _projectViewModels)
            {
                pvm.SelectedDates = SelectedDates;
                pvm.UpdateWeek();
            }

            _userTotalTimeViewModel.SelectedDates = SelectedDates;
            _userTotalTimeViewModel.ProjectsViewModels = _projectViewModels.ToList();
            WeekIntervalDates = _weekDayCreator.GetWeekIntervallText(SelectedDates[0]);
            
            _totalWeek.Clear();
            _totalWeek.Add(_userTotalTimeViewModel);
            TotalWeek = _totalWeek;

            HeaderDayStrings.Clear();
            HeaderDayStrings.AddRange(_weekDayCreator.GetHeaderDayStrings(SelectedDates));

        }

        public void UpdateTotals()
        {

            _totalWeek.Clear();
            _totalWeek.Add(_userTotalTimeViewModel);
            TotalWeek = _totalWeek;
        }

        public void LogOut()
        {
        }

        public void SetWeek(string weekDesignation)
        {
            if (_currentWeek > 0)

            {
                switch (weekDesignation)
                {
                    case "NextWeek":

                        SelectedDates =
                            _weekDayCreator.GetWeekDatesFromMonday(
                                _weekDayCreator.GetMondayFromWeek(_currentWeek, 2024),
                                WeekTraverse.Next);
                        _currentWeek++;
                        CurrentWeek = _currentWeek;

                        break;

                    case "PreviousWeek":

                        SelectedDates =
                            _weekDayCreator.GetWeekDatesFromMonday(
                                _weekDayCreator.GetMondayFromWeek(_currentWeek, 2024),
                                WeekTraverse.Previous);
                        _currentWeek--;
                        CurrentWeek = _currentWeek;
                        break;
                }

                Update();
            }

        }
    }
}