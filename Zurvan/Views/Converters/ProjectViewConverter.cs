using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using Zurvan.ClientApp.ViewModels;

namespace Zurvan.ClientApp.Views.Converters
{
    public class ProjectViewConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                var views = new List<UserProjectViewModel>();
                foreach (UserProjectViewModel pm in views)
                {
                    return pm;
                }
      
            }
            catch (Exception)
            {

            }

            return null;
        }

        private void Flatten(UserProjectViewModel vm, List<UserProjectViewModel> flattened)
        {
            flattened.Add(vm);

        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}


