namespace CronParser.Core
{
    [Serializable]
    public class InvalidCronExpressionException : Exception
    {
        public InvalidCronExpressionException() { }

        public InvalidCronExpressionException(string message) : base(message) { }

        public InvalidCronExpressionException(string message, Exception inner) : base(message, inner) { }
    }
}
