namespace ConsoleCommandApp.Exceptions
{
    [Serializable]
    public class GetTemperatureException : Exception
    {
        public GetTemperatureException()
        { }

        public GetTemperatureException(string message)
            : base(message)
        { }

        public GetTemperatureException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}
