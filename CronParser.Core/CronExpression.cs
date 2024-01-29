namespace CronParser.Core
{
    public class CronExpression
    {
        public CronField Minutes { get; set; }
        public CronField Hours { get; set; }
        public CronField DaysOfMonth { get; set; }
        public CronField Months { get; set; }
        public CronField DaysOfWeek { get; set; }
        public string Command { get; set; }

        public CronExpression(CronField minutes,
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
