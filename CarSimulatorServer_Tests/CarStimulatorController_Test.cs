using APIServiceLibrary.Services;
using CarSimulator.Server.Controllers;
using CarSimulator.Server.Factories;
using CarSimulator.Server.Models.ViewModels;
using DataLogicLibrary.Factories;
using DataLogicLibrary.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace CarSimulatorServer_Tests
{
    public class CarStimulatorController_Test
    {
        [Fact]
        public async Task Index_WhenSelectedActionIs7_ShouldRedirectToIndex()
        {

            // Arrange

            var controller = new CarSimulatorController(null, null, null, null, null, null);
            var viewModel = new SimulationViewModel { SelectedAction = 7 };

            // Act
            var result = await controller.Index(viewModel);

            // Assert
            var redirect = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirect.ActionName);
        }

        [Fact]
        public async Task WhenDriverIsNull_ShouldCreateDriver()
        {
            // Arrange
            var mockApiService = new Mock<IAPIService>();
            var mockDriverFactory = new Mock<IDriverFactory>();

            // Setup mock to return a sample ResultsDTO


            var controller = new CarSimulatorController(
                mockApiService.Object,
                Mock.Of<ISimulationLogicService>(),
                Mock.Of<ICarFactory>(),
                mockDriverFactory.Object,
                Mock.Of<IStatusFactory>(),
                Mock.Of<IStatusMessageService>()
                );

            var viewModel = new SimulationViewModel { Driver = null, SelectedAction = 1 };

            // Act
            var result = await controller.Index(viewModel);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<SimulationViewModel>(viewResult.Model);
            Assert.NotNull(model.Driver);
            Assert.Equal("TestDriver", model.Driver.First);
        }

    }
}
