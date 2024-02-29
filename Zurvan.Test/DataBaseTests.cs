using System.Data.SQLite;
using Xunit.Abstractions;
using Zurvan.Core.Interfaces;
using Zurvan.DataBase;
namespace Zurvan.Test
{
    public class DataBaseTests
    {
        SQLiteService dbservice = new SQLiteService("DataSource=TimeRecords.db;Version=3");
        private List<IUser> users = new List<IUser>();
        private readonly ITestOutputHelper output;
        private int userId;

        public DataBaseTests(ITestOutputHelper output)
        {
            this.output = output;
        }
  
        [Fact]
        public void Test1()
        {
            string connected = dbservice.ConnectToDatabase();
            
        }

        [Fact]
        public void Test2()
        {
            string connected = dbservice.ConnectToDatabase();

            users = dbservice.GetAllUsers();
            output.WriteLine("ID: " + users[0].UserId +" "+ users[0].FirstName + " is an " + users[0].Type);
   
        }

        [Fact]

        public void Test3()
        {
            string connected = dbservice.ConnectToDatabase();

            List<int> ids = dbservice.GetProjectIds(1);
            foreach (int id in ids)
            {
                output.WriteLine("project id: " +id);
            }
            
            //should just be project
        }
        [Fact]
        public void Test4()
        {
            string connected = dbservice.ConnectToDatabase();

            List<IProject> userProjects = dbservice.GetUserProjects(1);
            foreach (IProject project in userProjects)
            {
                output.WriteLine("project id: " + project.id + " is called " + project.Name);
            }


        }
    }
}