using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zurvan.Core.Interfaces
{
    public interface IUser
    {
        string Name { get; }
        string Email { get; set; }
        int UserId { get; set; }

        UserType Type { get; set; }
        

        List<IProject> GetProjects { get; }
    }
}
