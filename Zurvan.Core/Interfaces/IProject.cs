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

        List<DateTimeData> UserDateTimeReported { get; set; }

        int ShowTotalTimeUsed();
    }
}