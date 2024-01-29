namespace CronParser.Core
{
    public abstract class CronField
    {
        public abstract IEnumerable<int> Expand();
    }
}
