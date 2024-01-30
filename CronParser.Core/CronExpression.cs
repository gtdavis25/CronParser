using CronParser.Core.Fields;

namespace CronParser.Core
{
    public class CronExpression
    {
        public CronField Minutes { get; }
        public CronField Hours { get; }
        public CronField DaysOfMonth { get; }
        public CronField Months { get; }
        public CronField DaysOfWeek { get; }
        public string Command { get; }

        internal CronExpression(CronField minutes,
                              CronField hours,
                              CronField daysOfMonth,
                              CronField months,
                              CronField daysOfWeek,
                              string command)
        {
            Minutes = minutes;
            Hours = hours;
            DaysOfMonth = daysOfMonth;
            Months = months;
            DaysOfWeek = daysOfWeek;
            Command = command;
        }
    }
}
