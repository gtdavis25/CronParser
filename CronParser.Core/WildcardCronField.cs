namespace CronParser.Core
{
    internal class WildcardCronField : CronField
    {
        private readonly int _stepValue;

        internal WildcardCronField(CronFieldType fieldType, int stepValue) : base(fieldType)
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
