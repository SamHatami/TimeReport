using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zurvan.Core.Interfaces
{
    public interface IProject
    {
        string Name { get; }
        string Description { get; }
        int id { get; set; }

        List<IUser> Members { get; }

        int ShowTotalTimeUsed();

    }
}
