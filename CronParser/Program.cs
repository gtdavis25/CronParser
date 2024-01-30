using CronParser.Core;

namespace CronParser
{
    internal class Program
    {
        static int Main(string[] args)
        {
            if (args.Length != 1)
            {
                Console.Error.WriteLine("Usage: CronParser \"<cron expression>\"");
                return 1;
            }

            var parser = new CronExpressionParser();
            try
            {
                var expression = parser.Parse(args[0]);
                Console.WriteLine("minute        " + string.Join(" ", expression.Minutes.Expand()));
                Console.WriteLine("hour          " + string.Join(" ", expression.Hours.Expand()));
                Console.WriteLine("day of month  " + string.Join(" ", expression.DaysOfMonth.Expand()));
                Console.WriteLine("month         " + string.Join(" ", expression.Months.Expand()));
                Console.WriteLine("day of week   " + string.Join(" ", expression.DaysOfWeek.Expand()));
                Console.WriteLine("command       " + expression.Command);
                return 0;
            }
            catch (InvalidCronExpressionException e)
            {
                Console.Error.WriteLine(e.Message);
                return 1;
            }
        }
    }
}
