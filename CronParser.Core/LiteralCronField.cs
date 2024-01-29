namespace CronParser.Core
{
    internal class LiteralCronField : CronField
    {
        private readonly int _value;

        internal LiteralCronField(CronFieldType fieldType, int value) : base(fieldType)
        {
            _value = value;
        }

        public override IEnumerable<int> Expand()
        {
            yield return _value;
        }
    }
}
