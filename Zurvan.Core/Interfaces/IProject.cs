using Zurvan.Core.TimeModels;

namespace Zurvan.Core.Interfaces
{
    public interface IProject
    {
        string Name { get; set; }
        string Description { get; set; }
        int id { get; set; }
        int AllocatedTime { get; set; }
        int UsedTime { get; set; }
        List<IUser> Members { get; }

        List<HourReportData> UserReportedData { get; set; }

        int ShowTotalTimeUsed();
    }
}