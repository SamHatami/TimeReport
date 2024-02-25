using System.Data.SQLite;
using Zurvan.Core.Interfaces;
using Zurvan.Core.UserFactory;
using Zurvan.Core.UserFactory.UserTypes;

namespace Zurvan.DataBase
{
    public class SQLiteService : IDataBaseService
    {
        private string database = "DataSource=TimeRecords.db;Version=3";
        private SQLiteConnection connection;

        public SQLiteService(string connectionString)
        {
        }

        public string ConnectToDatabase()
        {
            using (SQLiteConnection cn = new SQLiteConnection(database)) { return "Connected"; }

            return "Could not connect";
        }

        public List<IProject> GetAllProjects()
        {
            throw new NotImplementedException();
        }

        public List<IUser> GetAllUsers()
        {
            var sqlRequestUsers = "select * from Users";
            var users = new List<IUser>();

            using (SQLiteConnection connection = new SQLiteConnection(database))
            {
                connection.Open();
                using (var command = new SQLiteCommand(sqlRequestUsers, connection))
                {
                    using (var dataReader = command.ExecuteReader())
                    {
                        if (dataReader.Read())
                        {
                            var user = new UserCreator().NewUser(UserType.Employee);
                            dataReader.GetInt16(dataReader.GetOrdinal("id"));
                            user.FirstName = dataReader.GetString(dataReader.GetOrdinal("FirstName"));
                            user.LastName = dataReader.GetString(dataReader.GetOrdinal("LastName"));
                            users.Add(user);
                        }
                    }
                }

                return users;
            }
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
            throw new ArgumentException("Could not retrieve with user ID " + id);
        }

        public void AddNewUser(IUser user)
        {
            using (SQLiteTransaction mytransaction = connection.BeginTransaction())
            {
                using (SQLiteCommand mycommand = new SQLiteCommand(connection))
                {
                    SQLiteParameter myparam = new SQLiteParameter();
                    int n;

                    mycommand.CommandText = "INSERT INTO [MyTable] ([MyId]) VALUES(?)";
                    mycommand.Parameters.Add(myparam);

                    for (n = 0; n < 100000; n++)
                    {
                        myparam.Value = n + 1;
                        mycommand.ExecuteNonQuery();
                    }
                }
                mytransaction.Commit();
            }
        }
    }
}