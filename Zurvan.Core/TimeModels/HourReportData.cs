using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zurvan.Core.TimeModels
{
    public class HourReportData(string date = "", int timeUsed = 0, string weekDay = "")
    {
        public string Date { get; set; } = date;
        public string WeekDay { get; set; } = weekDay;
        public int TimeUsed { get; set; } = timeUsed;
    }
}
