using System.Runtime.CompilerServices;
using Caliburn.Micro;
using Zurvan.ClientApp.Views;
using Zurvan.Core.Interfaces;
using Zurvan.DataBase;

namespace Zurvan.ClientApp.ViewModels
{
    public class LogInViewModel : Screen
    {
        private IDataBaseService _dataBaseService;
        private ShellViewModel _shellViewModel;
        private string email;
        private string message;

        public string Message
        {
            get { return message; }
            set
            {
                message = value;
                NotifyOfPropertyChange(() => Message);
            }
        }

        public string Email
        {
            get { return email; }
            set
            {
                email = value;
                NotifyOfPropertyChange(() => Email);
            }
        }
        private string password;
        public string Password
        {
            get { return password; }
            set
            {
                password = value;
                NotifyOfPropertyChange(() => Password);
            }
        }


        public LogInViewModel(IDataBaseService dataBaseService, ShellViewModel ShellViewModel)
        {
            _dataBaseService = dataBaseService;
            _shellViewModel = ShellViewModel;
        }

        public void Login()
        {
            //perhaps some type of string check class?
            if (string.IsNullOrEmpty(email) && string.IsNullOrEmpty(password))
            {
                //set message to fill in all
                Message = "Please fill in email and password";
                return ;
            }
            if (!EmailIsOk())
                return ;

            else if (string.IsNullOrEmpty(Password))
            {
                Message = "Please enter a password";
                return ;
            }

            IUser? user = _dataBaseService.Login(email.Replace(" ", ""), Password.Replace(" ", ""));
            if (user != null )
                _shellViewModel.ActivateUserView(user.UserId); // ska nog skicka något till shellview istället för att anropa en metod.

     
        }


        private bool EmailIsOk()
        {
            if (string.IsNullOrEmpty(email))
            {
                Message = "Please fill in email";
                return false;
            }

            if (!email.Contains('@'))
            {
                Message = "Email in wrong format";
                return false;
            }

            if (!email.Contains('.'))
            {
                Message = "Email in wrong format";
                return false;
            }

            return true;
        }
    }
}
