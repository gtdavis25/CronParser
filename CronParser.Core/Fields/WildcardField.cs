namespace CronParser.Core.Fields
{
    internal class WildcardField : CronField
    {
        private readonly int _stepValue;

        internal WildcardField(CronFieldType fieldType, int stepValue) : base(fieldType)
        {
            _stepValue = stepValue;
        }

        public override IEnumerable<int> Expand()
        {
            for (var i = 0; i < FieldType.RangeLength; i += _stepValue)
            {
                yield return FieldType.RangeStart + i;
            }
        }
    }
}
