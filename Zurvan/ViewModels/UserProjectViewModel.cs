using Caliburn.Micro;
using Zurvan.Core.TimeModels;

namespace Zurvan.ClientApp.ViewModels
{
    public class UserProjectViewModel : PropertyChangedBase
    {
        public List<DateTimeData> ProjectTime { get; }

        public string ProjectName { get; }

        public UserProjectViewModel(string projectName, List<DateTimeData> time)
        {
            ProjectName = projectName;
            ProjectTime = time;

            projectName = "TestProject";

        }


    }
}