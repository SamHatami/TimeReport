using Zurvan.Core.Interfaces;
using Zurvan.DataBase;

namespace Zurvan.ClientApp.Models
{
    public class TimeReportHandler
    {
        private IDataBaseService _dataBaseService;
        public TimeReportHandler()
        {
            _dataBaseService = new SQLiteService();
            
        }
    }
}