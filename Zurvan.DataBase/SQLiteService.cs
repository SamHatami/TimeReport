﻿using System.Data.SQLite;
using Zurvan.Core.Interfaces;
using Zurvan.Core.Projects;
using Zurvan.Core.UserFactory;

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
            var sqlRequestUsers = "SELECT * FROM Users";
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
                            int id = dataReader.GetInt16(dataReader.GetOrdinal("UserID"));
                            var user = new UserCreator().NewUser(GetUserType(id));
                            user.UserId = id;
                            user.FirstName = dataReader.GetString(dataReader.GetOrdinal("FirstName"));
                            user.LastName = dataReader.GetString(dataReader.GetOrdinal("LastName"));

                            users.Add(user);
                        }
                    }
                }
                return users;
            }
        }

        public List<int> GetProjectIds(int userId)
        {
            var sqlRequestProjects = "SELECT ProjectsUsers.ProjectID FROM ProjectsUsers INNER JOIN Users ON Users.UserID = ProjectsUsers.UserID WHERE Users.UserID="+userId;
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

        public IUser GetUser(int id)
        {
            throw new ArgumentException("Could not retrieve with user ID " + id);
        }

        public IProject GetProject(int id)
        {
            throw new NotImplementedException();
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
            string userType = String.Empty;

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