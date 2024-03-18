using Zurvan.Core.Interfaces;
using Zurvan.Core.TimeModels;

namespace Zurvan.Core.Projects
{
    public class Project : IProject
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int id { get; set; }
        public int AllocatedTime { get; set; }
        public int UsedTime { get; set; }
        public List<IUser> Members { get; }
        public List<HourReportData> UserReportedData {get;set;}
            
        public int ShowTotalTimeUsed()
        {
            throw new NotImplementedException();
        }
    }
}