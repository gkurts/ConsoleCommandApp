using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCommandApp.Services
{
    public interface IWeatherService
    {
        Task<int> GetTemperatureAsync(int zip);
    }

    public class WeatherService : IWeatherService
    {
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
