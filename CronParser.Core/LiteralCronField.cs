namespace CronParser.Core
{
    internal class LiteralCronField : CronField
    {
        public int Value { get; set; }

        public LiteralCronField(int value)
        {
            Value = value;
        }

        public override IEnumerable<int> Expand()
        {
            yield return Value;
        }
    }
}
