namespace CronParser.Core.Fields
{
    internal class LiteralField : CronField
    {
        private readonly int _value;

        internal LiteralField(CronFieldType fieldType, int value) : base(fieldType)
        {
            _value = value;
        }

        public override IEnumerable<int> Expand()
        {
            yield return _value;
        }
    }
}
