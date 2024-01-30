namespace CronParser.Core.Fields
{
    internal class RangeField : CronField
    {
        private readonly int _min;
        private readonly int _max;
        private readonly int _stepValue;

        internal RangeField(CronFieldType fieldType, int min, int max, int stepValue) : base(fieldType)
        {
            _min = min;
            _max = max;
            _stepValue = stepValue;
        }

        public override IEnumerable<int> Expand()
        {
            var start = Math.Max(_min, FieldType.RangeStart);
            var end = Math.Min(_max + 1, FieldType.RangeStart + FieldType.RangeLength);
            for (var i = start; i < end; i += _stepValue)
            {
                yield return i;
            }
        }
    }
}
