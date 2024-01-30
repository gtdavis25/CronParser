using CronParser.Core;

namespace CronParser.Tests
{
    public class CronExpressionParserTests
    {
        [Fact]
        public void Parse_should_parse_an_expression_containing_only_literal_values()
        {
            var input = "0 0 1 1 1 echo Hello world";
            var parser = new CronExpressionParser();
            var result = parser.Parse(input);
            Assert.Equal(new[] { 0 }, result.Minutes.Expand());
            Assert.Equal(new[] { 0 }, result.Hours.Expand());
            Assert.Equal(new[] { 1 }, result.DaysOfMonth.Expand());
            Assert.Equal(new[] { 1 }, result.Months.Expand());
            Assert.Equal(new[] { 1 }, result.DaysOfWeek.Expand());
            Assert.Equal("echo Hello world", result.Command);
        }

        [Fact]
        public void Parse_should_parse_an_expression_containing_wildcards()
        {
            var input = "0 * * * * echo Hello world";
            var parser = new CronExpressionParser();
            var result = parser.Parse(input);
            Assert.Equal(new[] { 0 }, result.Minutes.Expand());
            Assert.Equal(Enumerable.Range(0, 24), result.Hours.Expand());
            Assert.Equal(Enumerable.Range(1, 31), result.DaysOfMonth.Expand());
            Assert.Equal(Enumerable.Range(1, 12), result.Months.Expand());
            Assert.Equal(Enumerable.Range(1, 7), result.DaysOfWeek.Expand());
            Assert.Equal("echo Hello world", result.Command);
        }

        [Fact]
        public void Parse_should_parse_an_expression_containing_ranges()
        {
            var input = "0 9-17 * * * echo Hello world";
            var parser = new CronExpressionParser();
            var result = parser.Parse(input);
            Assert.Equal(new[] { 0 }, result.Minutes.Expand());
            Assert.Equal(new[] { 9, 10, 11, 12, 13, 14, 15, 16, 17 }, result.Hours.Expand());
            Assert.Equal(Enumerable.Range(1, 31), result.DaysOfMonth.Expand());
            Assert.Equal(Enumerable.Range(1, 12), result.Months.Expand());
            Assert.Equal(Enumerable.Range(1, 7), result.DaysOfWeek.Expand());
            Assert.Equal("echo Hello world", result.Command);
        }

        [Fact]
        public void Parse_should_parse_an_expression_containing_wildcards_with_step_values()
        {
            var input = "*/15 9-17 * * 1-5 echo Hello world";
            var parser = new CronExpressionParser();
            var result = parser.Parse(input);
            Assert.Equal(new[] { 0, 15, 30, 45 }, result.Minutes.Expand());
            Assert.Equal(new[] { 9, 10, 11, 12, 13, 14, 15, 16, 17 }, result.Hours.Expand());
            Assert.Equal(Enumerable.Range(1, 31), result.DaysOfMonth.Expand());
            Assert.Equal(Enumerable.Range(1, 12), result.Months.Expand());
            Assert.Equal(new[] { 1, 2, 3, 4, 5 }, result.DaysOfWeek.Expand());
            Assert.Equal("echo Hello world", result.Command);
        }

        [Fact]
        public void Parse_should_parse_an_expression_containing_ranges_with_step_values()
        {
            var input = "0 9-17/2 * * * echo Hello world";
            var parser = new CronExpressionParser();
            var result = parser.Parse(input);
            Assert.Equal(new[] { 0 }, result.Minutes.Expand());
            Assert.Equal(new[] { 9, 11, 13, 15, 17 }, result.Hours.Expand());
            Assert.Equal(Enumerable.Range(1, 31), result.DaysOfMonth.Expand());
            Assert.Equal(Enumerable.Range(1, 12), result.Months.Expand());
            Assert.Equal(Enumerable.Range(1, 7), result.DaysOfWeek.Expand());
            Assert.Equal("echo Hello world", result.Command);
        }

        [Fact]
        public void Parse_should_parse_an_expression_containing_list_fields()
        {
            var input = "*/10,*/15 9-17/2 * * 1,3,5 echo Hello world";
            var parser = new CronExpressionParser();
            var result = parser.Parse(input);
            Assert.Equal(new[] { 0, 10, 15, 20, 30, 40, 45, 50 }, result.Minutes.Expand());
            Assert.Equal(new[] { 9, 11, 13, 15, 17 }, result.Hours.Expand());
            Assert.Equal(Enumerable.Range(1, 31), result.DaysOfMonth.Expand());
            Assert.Equal(Enumerable.Range(1, 12), result.Months.Expand());
            Assert.Equal(new[] { 1, 3, 5 }, result.DaysOfWeek.Expand());
            Assert.Equal("echo Hello world", result.Command);
        }
    }
}