using Zurvan.Core.Interfaces;

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
        public Dictionary<string, int> UserDateTimeReported {get;set;}

        public int ShowTotalTimeUsed()
        {
            throw new NotImplementedException();
        }
    }
}