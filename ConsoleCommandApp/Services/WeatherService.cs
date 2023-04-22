using ConsoleCommandApp.Exceptions;
using ConsoleCommandApp.Interfaces;

namespace ConsoleCommandApp.Services
{
    public class WeatherService : IWeatherService
    {
        ///<inheritdoc />
        public async Task<int> GetTemperatureAsync(int zip)
        {
            Thread.Sleep(500);

            var random = new Random();
            var temp = random.Next(0, 115);

            //keep it spicy 🌶️
            if (temp % 10 == 0)
            {
                throw new GetTemperatureException("Oh noes! Unable to fetch the temperature at this time!");
            }

            return await Task.FromResult(temp);
        }
    }
}
