using System.ComponentModel;
using System.Globalization;
using System.Runtime.InteropServices.JavaScript;

namespace Zurvan.Core.TimeModels
{
    public class WeekDateCreator
    {
        private List<string> _currentWeekDates;
        private int _currentWeekNumber;

        private readonly CultureInfo _cultureInfo;
        private readonly Calendar _cal;

        public WeekDateCreator()
        {
            _cultureInfo = CultureInfo.CurrentCulture;
            _currentWeekDates = new List<string>();
            _cal = _cultureInfo.Calendar;
            _cultureInfo = CultureInfo.InvariantCulture;
        }

        public int GetCurrentWeekNr(DateTime dateTime)
        {
            _currentWeekNumber =
                _cal.GetWeekOfYear(dateTime.Date, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);

            return _currentWeekNumber;
        }

        public List<string> GetWeekDatesFromMonday(DateTime currentMonday, WeekTraverse traverse)
        {
            List<string> weekDates = new List<string>();
            DateTime dateStartOfWeek = new DateTime();

            switch (traverse)
            {
                case WeekTraverse.Next:
                    dateStartOfWeek = currentMonday.Date.AddDays(7);
                    break;

                case WeekTraverse.Previous:
                    dateStartOfWeek = currentMonday.Date.AddDays(-7);
                    break;
                default:
                    dateStartOfWeek = currentMonday.Date;
                    break;
            }

            DateTime endOfWeek = dateStartOfWeek.AddDays(6);

            for (DateTime date = dateStartOfWeek; date <= endOfWeek; date = date.AddDays(1))
                weekDates.Add(date.ToString("yyyy-MM-dd"));
            
            return weekDates;
        }

        public DateTime GetCurrentWeekMonday()
        {
            DateTime now = new DateTime();
            
            now = DateTime.Today;
            DateTime startOfWeek = now.AddDays(-(int)now.DayOfWeek + (int)DayOfWeek.Monday);

            return startOfWeek;
        }

        public DateTime GetMondayFromWeek(int weekNumber, int year)
        {
            DateTime firstDay = new DateTime(year, 1, 1);

            DateTime currentMonday =
                firstDay.AddDays(-(int)firstDay.DayOfWeek + (weekNumber - 1) * 7 + (int)DayOfWeek.Monday);

            return currentMonday;
        }

        public string GetWeekIntervallText(string startDate)
        {
            DateTime date = DateTime.ParseExact(startDate,"yyyy-MM-dd", null);
            string weekIntervallText =
                $"{date.ToString("MMM dd")} - {date.AddDays(6).ToString("MMM dd")}";
            return weekIntervallText;   
        }

        public List<string> GetHeaderDayStrings(List<string> selectedDates)
        {
            List<string> headerDayStrings = new List<string>();
            foreach (string date in selectedDates)
            {
                DateTime currentDate = DateTime.ParseExact(date, "yyyy-MM-dd", null);
                string headerDayString = currentDate.ToString("ddd\nMMM dd").ToUpper();
                headerDayStrings.Add(headerDayString);
            }
            return headerDayStrings;
        }

    }
}