
using Caliburn.Micro;
using Zurvan.ClientApp.Views;
using Zurvan.Core.Interfaces;
using Zurvan.DataBase;

namespace Zurvan.ClientApp.ViewModels
{
    public class ShellViewModel : Conductor<object>
    {
        private IDataBaseService _dataBaseService;

        public ShellViewModel()
        {
            _dataBaseService = new SQLiteService();
            LogInView();
            
        }

        public void LogInView()
        {
            ActivateItemAsync(new LogInViewModel(_dataBaseService, this));
        }

        public void ActivateUserView(int userId)
        {
            ActivateItemAsync(new UserViewModel(_dataBaseService, userId));
        }

    }
}