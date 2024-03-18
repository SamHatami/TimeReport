using Microsoft.VisualBasic;
using System.Configuration;
using System.Data.SQLite;
using System.Windows.Input;
using Zurvan.Core.Interfaces;
using Zurvan.Core.Projects;
using Zurvan.Core.TimeModels;
using Zurvan.Core.UserFactory;
using static System.Runtime.InteropServices.JavaScript.JSType;
using String = System.String;

namespace Zurvan.DataBase
{
    public class SQLiteService : IDataBaseService
    {
        private string database = "DataSource=TimeRecords.db;Version=3";
        private SQLiteConnection connection;
        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();

        public SQLiteService()
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

        public IUser GetUser(int id)
        {
            var sqlRequestUsers = "SELECT * FROM Users WHERE UserId=" + id;

            using (SQLiteConnection connection = new SQLiteConnection(database))
            {
                connection.Open();
                using (var command = new SQLiteCommand(sqlRequestUsers, connection))
                {
                    using (var dataReader = command.ExecuteReader())
                    {
                        if (dataReader.Read())
                        {
                            var user = new UserCreator().NewUser(GetUserType(id));
                            user.UserId = id;
                            user.FirstName = dataReader.GetString(dataReader.GetOrdinal("FirstName"));
                            user.LastName = dataReader.GetString(dataReader.GetOrdinal("LastName"));
                            user.Email = dataReader.GetString(dataReader.GetOrdinal("Email"));
                            user.Projects = GetUserProjects(id);

                            return user;
                        }
                    }
                }

                Logger.Info("Fetched userid " + id);

                return null;
            }
        }

        public IUser? Login(string email, string password)
        {
            var sqlRequestUsers = "SELECT * FROM Users WHERE Password = @Password AND Email = @Email";

            using (SQLiteConnection connection = new SQLiteConnection(database))
            {
                connection.Open();
                using (var command = new SQLiteCommand(sqlRequestUsers, connection))
                {
                    command.Parameters.AddWithValue("@Email", email);
                    command.Parameters.AddWithValue("@Password", password);
                    using (var dataReader = command.ExecuteReader())
                    {
                        if (dataReader.Read())
                        {
                            int id = dataReader.GetInt16(dataReader.GetOrdinal("UserID"));
                            var user = new UserCreator().NewUser(GetUserType(id));
                            user.UserId = id;
                            user.FirstName = dataReader.GetString(dataReader.GetOrdinal("FirstName"));
                            user.LastName = dataReader.GetString(dataReader.GetOrdinal("LastName"));
                            user.Email = dataReader.GetString(dataReader.GetOrdinal("Email"));

                            return user;
                        }
                    }
                }
                return null;
            }
        }

        public List<IUser> GetAllUsers()
        {
            var sqlRequestUsers = "SELECT * FROM Users";
            var users = new List<IUser>();

            using (SQLiteConnection connection = new SQLiteConnection(database))
            {
                connection.Open();
                using (var command = new SQLiteCommand(sqlRequestUsers, connection))
                {
                    using (var dataReader = command.ExecuteReader())
                    {
                        while (dataReader.Read())
                        {
                            int id = dataReader.GetInt16(dataReader.GetOrdinal("UserID"));
                            var user = new UserCreator().NewUser(GetUserType(id));
                            user.UserId = id;
                            user.FirstName = dataReader.GetString(dataReader.GetOrdinal("FirstName"));
                            user.LastName = dataReader.GetString(dataReader.GetOrdinal("LastName"));
                            user.Email = dataReader.GetString(dataReader.GetOrdinal("Email"));

                            users.Add(user);
                        }
                    }
                }
                Logger.Info("Fetched all users.");
                return users;
            }
        }

        public List<int> GetProjectIds(int userId)
        {
            var sqlRequestProjects = "SELECT ProjectsUsers.ProjectID FROM ProjectsUsers INNER JOIN Users ON Users.UserID = ProjectsUsers.UserID WHERE Users.UserID=" + userId;
            List<int> projectIds = new List<int>();

            using (SQLiteConnection connection = new SQLiteConnection(database))
            {
                connection.Open();
                using (var command = new SQLiteCommand(sqlRequestProjects, connection))
                {
                    using (var dataReader = command.ExecuteReader())
                    {
                        while (dataReader.Read())
                        {
                            int projectId = dataReader.GetInt16(dataReader.GetOrdinal("ProjectID"));
                            projectIds.Add(projectId);
                        }
                    }
                }
            }

            return projectIds;
        }

        //public IUser GetUserByID(int id) // Change this to a open search user?
        //{
        //    IUser user =  (GetUserType(id));
        //    string sqlRequestUser = "SELECT * FROM Users WHERE UserID=" + id;
        //    using (SQLiteConnection connection = new SQLiteConnection(database))
        //    {
        //        using (var command = new SQLiteCommand(sqlRequestUser, connection))
        //        {
        //            using (var dataReader = command.ExecuteReader())
        //            {
        //                if (dataReader.Read())
        //                {
        //                    int id = dataReader.GetInt16(dataReader.GetOrdinal("UserID"));
        //                    var user = new UserCreator().NewUser(GetUserType(id));
        //                    user.UserId = id;
        //                    user.FirstName = dataReader.GetString(dataReader.GetOrdinal("FirstName"));
        //                    user.LastName = dataReader.GetString(dataReader.GetOrdinal("LastName"));

        //                }
        //            }
        //        }
        //    }

        //}

        public IProject GetProject(int id)
        {
            string sqlRequest = "SELECT * FROM Projects WHERE ProjectID=" + id;
            throw new NotImplementedException();
        }

        public List<HourReportData> GetReportedTimePerUser(int projectId, int userId, List<string> selectedDates)
        {
            List<HourReportData> TimeData = new List<HourReportData>();

            string sqlRequest = "Select DISTINCT * from ProjectsUsersDateReports Where ProjectID="+projectId +" AND UserID=" + userId;
            DateTime dateValue = new DateTime();
            using (SQLiteConnection connection = new SQLiteConnection(database))
            {
   
                connection.Open();
                using (var command = new SQLiteCommand(sqlRequest, connection))
                {
                    using (var dataReader = command.ExecuteReader())
                    {
                        while (dataReader.Read())
                        {
                            string date = dataReader.GetString(dataReader.GetOrdinal("Date"));
                            int time = dataReader.GetInt32(dataReader.GetOrdinal("Time"));
                            dateValue = DateTime.Parse(date);

                            if (selectedDates.Contains(date))
                            {
                                HourReportData dtd = new HourReportData(date, time, dateValue.DayOfWeek.ToString());
                                TimeData.Add(dtd);
                            }

                            int step =dataReader.StepCount;
                        }

                    }
                }
            }

            return TimeData;
        }

        public void UpdateProjectByDateAndUser(int projectId, int userId, string date, int updatedTime)
        {
            string sqlRequest;

            if (!CheckIfDateDataExist(projectId, userId, date))
                sqlRequest = "INSERT INTO ProjectsUsersDateReports (ProjectID, UserID, Date, Time) VALUES (@projectId, @userId, @date, @time)";
            
            else
                sqlRequest = "UPDATE ProjectsUsersDateReports SET Time= @time WHERE ProjectID = @projectId AND UserID= @userId AND Date= @date";

            

            using (SQLiteConnection connection = new SQLiteConnection(database))
            {
                connection.Open();
                using (var command = new SQLiteCommand(sqlRequest, connection))
                {
                    command.Parameters.AddWithValue("@date", date);
                    command.Parameters.AddWithValue("@userId", userId);
                    command.Parameters.AddWithValue("@projectId", projectId);
                    command.Parameters.AddWithValue("@time", updatedTime);
                    command.ExecuteNonQuery();
                }
            }
        }

        private bool CheckIfDateDataExist(int projectId, int userId, string date)
        {
            int count;
            string sqlRequest =
                "SELECT COUNT(*) FROM ProjectsUsersDateReports WHERE ProjectID = @projectId AND UserID = @userId AND Date = @date";

            using (SQLiteConnection connection = new SQLiteConnection(database))
            {
                connection.Open();
                using (var command = new SQLiteCommand(sqlRequest, connection))
                {
                    command.Parameters.AddWithValue("@projectId", projectId);
                    command.Parameters.AddWithValue("@userId", userId);
                    command.Parameters.AddWithValue("@date", date); // Assuming date is of type DateTime
                    count = Convert.ToInt32(command.ExecuteScalar());
                }
            }

            return count > 0;
        }



        public List<IProject> GetUserProjects(int userId)
        {
            List<IProject> projects = new List<IProject>();
            using (SQLiteConnection connection = new SQLiteConnection(database))
            {
                connection.Open();
                foreach (int projectId in GetProjectIds(userId))
                {
                    var sqlRequestUsers = "SELECT * FROM Projects WHERE ProjectID=" + projectId;

                    using (var command = new SQLiteCommand(sqlRequestUsers, connection))
                    {
                        using (var dataReader = command.ExecuteReader())
                        {
                            while (dataReader.Read())
                            {
                                IProject project = new Project();
                                project.id = projectId;
                                project.Name = dataReader.GetString(dataReader.GetOrdinal("Name"));
                                projects.Add(project);
                            }
                        }
                    }
                }
            }
            return projects;
        }

        public string GetUserType(int userId)
        {
            var sqlRequestUsers = "SELECT UsersTypes.Type FROM UsersTypes JOIN Users ON Users.UserID = UsersTypes.UserID";
            string userType ="";

            using (SQLiteConnection connection = new SQLiteConnection(database))
            {
                connection.Open();
                using (var command = new SQLiteCommand(sqlRequestUsers, connection))
                {
                    using (var dataReader = command.ExecuteReader())
                    {
                        if (dataReader.Read())
                        {
                            userType = dataReader.GetString(dataReader.GetOrdinal("Type"));
                        }
                    }
                }
            }

            return userType;
        }

        public List<IUser> GetProjectMembers(int projectId)
        {
            var sqlRequestUsers = "select * from ProjectUsers";
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
                        }
                    }
                }
            }

            return users;
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