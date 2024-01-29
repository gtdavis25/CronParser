namespace CronParser.Core
{
    internal class WildcardCronField : CronField
    {
        internal WildcardCronField(CronFieldType fieldType) : base(fieldType)
        {
        }

        public override IEnumerable<int> Expand()
        {
            return Enumerable.Range(FieldType.RangeStart, FieldType.RangeLength);
        }
    }
}
