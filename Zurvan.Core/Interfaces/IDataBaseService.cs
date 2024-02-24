using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zurvan.Core.Interfaces
{
    public interface IDataBaseService
    {
        List<IUser> GetAllUsers();
        List<IProject> GetAllProjects();
        IUser GetUser(int id);
        IProject GetProject(int id);
        List<IProject> GetProjects(List<int> ids);
        
      
    }
}
