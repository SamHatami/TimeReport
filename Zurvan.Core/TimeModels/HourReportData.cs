using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zurvan.Core.TimeModels
{
    public class HourReportData
    {
        public string Date { get; set; }
        public string WeekDay { get; set; }
        public int TimeUsed { get; set; }

        public HourReportData(string date = "", int timeUsed = 0, string weekDay = "")
        {
         Date  = date;
         WeekDay  = weekDay;
         TimeUsed  = timeUsed;
        }
    }
}
