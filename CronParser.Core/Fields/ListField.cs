namespace CronParser.Core.Fields
{
    internal class ListField : CronField
    {
        private readonly List<CronField> _subFields;

        internal ListField(CronFieldType fieldType, IEnumerable<CronField> subFields) : base(fieldType)
        {
            _subFields = subFields.ToList();
        }

        public override IEnumerable<int> Expand()
        {
            return _subFields.SelectMany(x => x.Expand())
                .Distinct()
                .OrderBy(x => x);
        }
    }
}
