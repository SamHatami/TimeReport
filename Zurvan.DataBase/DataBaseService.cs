using System.Data.SQLite;
using Zurvan.Core.Interfaces;

namespace Zurvan.DataBase
{
    public class DataBaseService : IDataBaseService
    {

        string database = "";

        public DataBaseService() 
        { 

        }

        public void ConnectToDatabase(string database)
        {
            SQLiteAuthorizerActionCode.
        }

        public List<IProject> GetAllProjects()
        {
            throw new NotImplementedException();
        }

        public List<IUser> GetAllUsers()
        {
            throw new NotImplementedException();
        }

        public IProject GetProject(int id)
        {
            throw new NotImplementedException();
        }

        public List<IProject> GetProjects(List<int> ids)
        {
            throw new NotImplementedException();
        }

        public IUser GetUser(int id)
        {
            throw new NotImplementedException();
        }
    }
}