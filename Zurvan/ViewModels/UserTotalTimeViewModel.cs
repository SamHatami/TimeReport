using System.Data.Entity.Infrastructure.Design;
using Caliburn.Micro;
using Zurvan.Core.Interfaces;

namespace Zurvan.ClientApp.ViewModels
{
    public class UserTotalTimeViewModel : Screen
    {
        private readonly int _userId;
        private readonly IDataBaseService _database;
        
        private List<UserReportProjectViewModel> _projectsViewModels;
        public List<UserReportProjectViewModel> ProjectsViewModels
        {
        get => _projectsViewModels;
        set
        {
            _projectsViewModels = value;
            NotifyOfPropertyChange(() => ProjectsViewModels);
            Update();
        }
    }

        private List<string> _selectedDates;
        public List<string> SelectedDates
        {
            get => _selectedDates;
            set
            {
                _selectedDates = value;
                NotifyOfPropertyChange(() => SelectedDates);
            }
        }

        private int _mondayTotal;

        public int MondayTotal
        {
            get => _mondayTotal;
            set
            {
                _mondayTotal = value;
                NotifyOfPropertyChange(() => MondayTotal);
            }
        }

        private int _tuesdayTotal;

        public int TuesdayTotal
        {
            get => _tuesdayTotal;
            set
            {
                _tuesdayTotal = value;
                NotifyOfPropertyChange(() => TuesdayTotal);
            }
        }

        private int _wednesdayTotal;

        public int WednesdayTotal
        {
            get => _wednesdayTotal;
            set
            {
                _wednesdayTotal = value;
                NotifyOfPropertyChange(() => WednesdayTotal);
            }
        }

        private int _thursdayTotal;

        public int ThursdayTotal
        {
            get => _thursdayTotal;
            set
            {
                _thursdayTotal = value; 
                NotifyOfPropertyChange(() => ThursdayTotal);
            }
        }


        private int _fridayTotal;
        public int FridayTotal
        {
            get => _fridayTotal;
            set
            {
                    _fridayTotal = value;
                    NotifyOfPropertyChange(() => FridayTotal);

            }
        }
        public UserTotalTimeViewModel(List<UserReportProjectViewModel> projectViewModelsViewModels, List<string> selectedDates, IDataBaseService dataBase, int userId)
        {
            _userId = userId;
            _database = dataBase;
            _projectsViewModels = projectViewModelsViewModels;
            _selectedDates = selectedDates;

            Update();
        }

        private void Update()
        {
            _mondayTotal = 0;
            _tuesdayTotal = 0;
            _wednesdayTotal = 0;
            _thursdayTotal = 0;
            _fridayTotal = 0;
            
            foreach (var projectViewModel in _projectsViewModels)
            {
                MondayTotal += projectViewModel.Monday;
                ThursdayTotal += projectViewModel.Tuesday;
                WednesdayTotal += projectViewModel.Wednesday;
                ThursdayTotal += projectViewModel.Thursday;
                FridayTotal += projectViewModel.Friday;
            }
        }
    }
}