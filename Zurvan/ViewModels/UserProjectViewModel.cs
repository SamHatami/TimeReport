using Caliburn.Micro;
using Zurvan.Core.TimeModels;

namespace Zurvan.ClientApp.ViewModels
{
    public class UserProjectViewModel : Screen
    {
        public List<DateTimeData> ProjectTime { get; set; }

        public string ProjectName { get; set; }

        public UserProjectViewModel(string projectName, List<DateTimeData> time)
        {
            ProjectName = projectName;
            ProjectTime = time;

        }


    }
}