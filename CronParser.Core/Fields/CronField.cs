namespace CronParser.Core.Fields
{
    public abstract class CronField
    {
        internal CronFieldType FieldType { get; }

        internal CronField(CronFieldType fieldType)
        {
            FieldType = fieldType;
        }

        public abstract IEnumerable<int> Expand();
    }
}
