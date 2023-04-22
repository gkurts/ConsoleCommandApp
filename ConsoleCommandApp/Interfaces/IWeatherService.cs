namespace ConsoleCommandApp.Interfaces
{
    /// <summary>
    /// Fake weather service
    /// </summary>
    public interface IWeatherService
    {
        /// <summary>
        /// Returns a random temperature.
        /// </summary>
        /// <param name="zip">Any 5 numbers will do.</param>
        /// <returns></returns>
        Task<int> GetTemperatureAsync(int zip);
    }
}
