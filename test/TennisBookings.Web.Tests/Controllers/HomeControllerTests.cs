using Microsoft.AspNetCore.Mvc;
using Moq;
using TennisBookings.Web.Controllers;
using TennisBookings.Web.Services;
using TennisBookings.Web.ViewModels;
using Xunit;

namespace TennisBookings.Web.Tests.Controllers
{
    public class HomeControllerTests
    {
        [Fact]
        public void ReturnsExpectedViewModel_WhenWeatherIsSun()
        {
            var mockForcaster = new Mock<IWeatherForecaster>();
            mockForcaster.Setup(w => w.GetCurrentWeather()).Returns(new WeatherResult
            {
                WeatherCondition = External.Models.WeatherCondition.Sun
            });
            var sut = new HomeController(mockForcaster.Object);

            var result = sut.Index();

            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<HomeViewModel>(viewResult.ViewData.Model);
            Assert.Contains("It's sunny right now", model.WeatherDescription);
        }

        [Fact]
        public void ReturnsExpectedViewModel_WhenWeatherIsRain()
        {
            var mockForcaster = new Mock<IWeatherForecaster>();
            mockForcaster.Setup(w => w.GetCurrentWeather()).Returns(new WeatherResult
            {
                WeatherCondition = External.Models.WeatherCondition.Rain
            });
            var sut = new HomeController(mockForcaster.Object);

            var result = sut.Index();

            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<HomeViewModel>(viewResult.ViewData.Model);
            Assert.Contains("We're sorry but it's raining here.", model.WeatherDescription);
        }
    }
}
