using CronParser.Core;

namespace CronParser.Tests
{
    public class CronExpressionParserTests
    {
        [Fact]
        public void Parse_should_parse_an_expression_containing_only_literal_values()
        {
            var input = "1 1 1 1 1 echo Hello world";
            var parser = new CronExpressionParser();
            var result = parser.Parse(input);
            Assert.Equal(new[] { 1 }, result.Minutes.Expand());
            Assert.Equal(new[] { 1 }, result.Hours.Expand());
            Assert.Equal(new[] { 1 }, result.DaysOfMonth.Expand());
            Assert.Equal(new[] { 1 }, result.Months.Expand());
            Assert.Equal(new[] { 1 }, result.DaysOfWeek.Expand());
            Assert.Equal("echo Hello world", result.Command);
        }
    }
}