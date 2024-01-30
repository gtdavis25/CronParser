using System.Text;

namespace CronParser.Core
{
    public class CronExpressionParser
    {
        public CronExpression Parse(string input)
        {
            var reader = new StringReader(input);
            var minutes = ParseField(reader, CronFieldType.Minute);
            var hours = ParseField(reader, CronFieldType.Hour);
            var daysOfMonth = ParseField(reader, CronFieldType.DayOfMonth);
            var months = ParseField(reader, CronFieldType.Month);
            var daysOfWeek = ParseField(reader, CronFieldType.DayOfWeek);
            SkipWhiteSpace(reader);
            var command = reader.ReadToEnd();
            return new CronExpression(minutes, hours, daysOfMonth, months, daysOfWeek, command);
        }

        private CronField ParseField(TextReader reader, CronFieldType fieldType)
        {
            SkipWhiteSpace(reader);
            if (reader.Peek() == '*')
            {
                reader.Read();
                var stepValue = ParseOptionalStepValue(reader) ?? 1;
                return new WildcardCronField(fieldType, stepValue);
            }
            else if (char.IsDigit((char)reader.Peek()))
            {
                var value = ParseInteger(reader);
                if (reader.Peek() == '-')
                {
                    reader.Read();
                    var max = ParseInteger(reader);
                    var stepValue = ParseOptionalStepValue(reader) ?? 1;
                    return new RangeField(fieldType, value, max, stepValue);
                }
                else
                {
                    return new LiteralCronField(fieldType, value);
                }
            }

            throw new InvalidCronExpressionException($"Unexpected character: {(char)reader.Peek()}");
        }

        private void SkipWhiteSpace(TextReader reader)
        {
            while (char.IsWhiteSpace((char)reader.Peek()))
            {
                reader.Read();
            }
        }

        private int ParseInteger(TextReader reader)
        {
            if (!char.IsDigit((char)reader.Peek()))
            {
                throw new InvalidCronExpressionException($"Unexpected character: {(char)reader.Peek()}");
            }

            var buffer = new StringBuilder();
            while (char.IsDigit((char)reader.Peek()))
            {
                buffer.Append((char)reader.Read());
            }

            return int.Parse(buffer.ToString());
        }

        private int? ParseOptionalStepValue(TextReader reader)
        {
            if (reader.Peek() == '/')
            {
                reader.Read();
                return ParseInteger(reader);
            }

            return null;
        }
    }
}
