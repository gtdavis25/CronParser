namespace CronParser.Core
{
    internal class CronFieldType
    {
        internal int RangeStart { get; }
        internal int RangeLength { get; }

        internal static readonly CronFieldType Minute = new(0, 60);
        internal static readonly CronFieldType Hour = new(0, 24);
        internal static readonly CronFieldType DayOfMonth = new(1, 31);
        internal static readonly CronFieldType Month = new(1, 12);
        internal static readonly CronFieldType DayOfWeek = new(1, 7);

        private CronFieldType(int min, int max)
        {
            RangeStart = min;
            RangeLength = max;
        }
    }
}
