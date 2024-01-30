using System.Text;
using CronParser.Core.Fields;

namespace CronParser.Core
{
    public class CronExpressionParser
    {
        public CronExpression Parse(string input)
        {
            var reader = new StringReader(input);
            var minutes = ParseListOrSingleField(reader, CronFieldType.Minute);
            var hours = ParseListOrSingleField(reader, CronFieldType.Hour);
            var daysOfMonth = ParseListOrSingleField(reader, CronFieldType.DayOfMonth);
            var months = ParseListOrSingleField(reader, CronFieldType.Month);
            var daysOfWeek = ParseListOrSingleField(reader, CronFieldType.DayOfWeek);
            SkipWhiteSpace(reader);
            var command = reader.ReadToEnd();
            return new CronExpression(minutes, hours, daysOfMonth, months, daysOfWeek, command);
        }

        private CronField ParseListOrSingleField(TextReader reader, CronFieldType fieldType)
        {
            SkipWhiteSpace(reader);
            var subFields = new List<CronField>();
            while (true)
            {
                subFields.Add(ParseSingleField(reader, fieldType));
                if (reader.Peek() == ',')
                {
                    reader.Read();
                    continue;
                }

                break;
            }

            if (subFields.Count == 1)
            {
                return subFields[0];
            }

            return new ListField(fieldType, subFields);
        }

        private CronField ParseSingleField(TextReader reader, CronFieldType fieldType)
        {
            if (reader.Peek() == '*')
            {
                reader.Read();
                var stepValue = ParseOptionalStepValue(reader) ?? 1;
                return new WildcardField(fieldType, stepValue);
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
                    return new LiteralField(fieldType, value);
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
