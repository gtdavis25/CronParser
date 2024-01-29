using System.Text;

namespace CronParser.Core
{
    public class CronExpressionParser
    {
        public CronExpression Parse(string input)
        {
            var reader = new StringReader(input);
            var minutes = ParseField(reader);
            var hours = ParseField(reader);
            var daysOfMonth = ParseField(reader);
            var months = ParseField(reader);
            var daysOfWeek = ParseField(reader);
            SkipWhiteSpace(reader);
            var command = reader.ReadToEnd();
            return new CronExpression(minutes, hours, daysOfMonth, months, daysOfWeek, command);
        }

        private CronField ParseField(TextReader reader)
        {
            SkipWhiteSpace(reader);
            if (char.IsDigit((char)reader.Peek()))
            {
                var buffer = new StringBuilder();
                while (char.IsDigit((char)reader.Peek()))
                {
                    buffer.Append((char)reader.Read());
                }

                return new LiteralCronField(int.Parse(buffer.ToString()));
            }

            throw new FormatException($"Unexpected character: {(char)reader.Peek()}");
        }

        private void SkipWhiteSpace(TextReader reader)
        {
            while (char.IsWhiteSpace((char)reader.Peek()))
            {
                reader.Read();
            }
        }
    }
}
