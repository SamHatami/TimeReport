
using Caliburn.Micro;
using Zurvan.ClientApp.Views;
using Zurvan.Core.Interfaces;
using Zurvan.DataBase;

namespace Zurvan.ClientApp.ViewModels
{
    public class ZurvanViewModel: Conductor<object>
    {
        private IDataBaseService _dataBaseService;
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
            get { return email;}
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


        public ZurvanViewModel()
        {
            _dataBaseService = new SQLiteService();
        }

        public void Login()
        {
            //perhaps some type of string check class?
            if (string.IsNullOrEmpty(email) && string.IsNullOrEmpty(password))
            {
                //set message to fill in all
                Message = "Please fill in email and password";
                return;
            }
            if (!EmailIsOk())
                return;
            
            else if (string.IsNullOrEmpty(Password))
            {
                Message = "Please enter a password";
                return;
            }

            if (_dataBaseService.Login(email.Replace(" ",""), Password.Replace(" ", "")))
                ActivateItemAsync(new UserView());

        }

        private bool EmailIsOk()
        {
            if (string.IsNullOrEmpty(email))
            {
                Message = "Please fill in email";
                return false;
            }
            
            if(!email.Contains('@'))
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
