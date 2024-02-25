using System.Data.SQLite;
using Zurvan.DataBase;
namespace Zurvan.Test
{
    public class ConnectToDatabase
    {
        SQLiteService dbservice = new SQLiteService("DataSource=TimeRecords.db;Version=3");

        [Fact]
        public void Test1()
        {
            string connected = dbservice.ConnectToDatabase();
            
        }

        [Fact]
        public void Test2()
        {
            string connected = dbservice.ConnectToDatabase();

            dbservice.GetAllUsers();
        }
    }
}