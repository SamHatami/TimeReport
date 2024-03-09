using System.Data.SQLite;
using FluentAssertions;
using Xunit.Abstractions;
using Zurvan.Core.Interfaces;
using Zurvan.Core.UserFactory.UserTypes;
using Zurvan.DataBase;
namespace Zurvan.Test
{
    public class DataBaseTests
    {
        SQLiteService dbservice = new SQLiteService();
        private List<IUser> users = new List<IUser>();
        private readonly ITestOutputHelper output;
        private int userId;

        public DataBaseTests(ITestOutputHelper output)
        {
            this.output = output;
        }
  
        [Fact]
        public void ConnectToDatabase()
        {
            string connected = dbservice.ConnectToDatabase();

            //Assert
            connected.Should().Be("Connected");

        }

        [Fact]
        public void GetAllUsers()
        {
            dbservice.ConnectToDatabase();
            users = dbservice.GetAllUsers();

            users[0].UserId.Should().BeGreaterThan(0);
            users[0].FirstName.Should().NotBeNullOrEmpty();
            users[0].LastName.Should().NotBeNullOrEmpty();
            users[0].Type.Should().Be(UserType.Employee);
            users[0].Email.Should().Contain(users[0].FirstName.ToLowerInvariant()).And.Contain(users[0].LastName.ToLowerInvariant());

            output.WriteLine("ID: " + users[0].UserId +" "+ users[0].FirstName + " is an " + users[0].Type);
   
        }

        [Fact]

        public void GetProjectIds()
        {
            string connected = dbservice.ConnectToDatabase();

            List<int> ids = dbservice.GetProjectIds(1);

            //Assert
            ids.Should().NotBeNull();
            ids.Count().Should().BeGreaterOrEqualTo(1);

            foreach (int id in ids)
            {
                output.WriteLine("project id: " +id);
            }
           
        }
        [Fact]
        public void GetUserProjects()
        {
            string connected = dbservice.ConnectToDatabase();

            List<IProject> userProjects = dbservice.GetUserProjects(1);

            userProjects.Should().NotBeNull();
            userProjects.Should().HaveCountGreaterThan(0);

            foreach (IProject project in userProjects)
            {
                output.WriteLine("project id: " + project.id + " is called " + project.Name);
            }


        }

        [Fact]
        public void GetReportedTime()
        {
            string connected = dbservice.ConnectToDatabase();

            int userId = 1;
            int projectId = 10;

            Dictionary<string,int> reportedTime = new Dictionary<string,int>();

            reportedTime = dbservice.GetReportedTimePerUser(projectId, userId);

            reportedTime.Should().NotBeNull();


            output.WriteLine("Date: " + reportedTime.ElementAt(0).Key+ " has reported time of " + reportedTime.ElementAt(0).Value );
            

        }
    }
}