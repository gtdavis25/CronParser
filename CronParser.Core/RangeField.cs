namespace CronParser.Core
{
    internal class RangeField : CronField
    {
        private readonly int _min;
        private readonly int _max;

        internal RangeField(CronFieldType fieldType, int min, int max) : base(fieldType)
        {
            _min = min;
            _max = max;
        }

        public override IEnumerable<int> Expand()
        {
            for (var i = Math.Max(_min, FieldType.RangeStart); i < Math.Min(_max + 1, FieldType.RangeStart + FieldType.RangeLength); i++)
            {
                yield return i;
            }
        }
    }
}
