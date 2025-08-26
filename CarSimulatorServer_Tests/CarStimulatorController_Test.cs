using APIServiceLibrary.DTO;
using APIServiceLibrary.Services;
using CarSimulator.Server.Controllers;
using CarSimulator.Server.Factories;
using CarSimulator.Server.Models;
using CarSimulator.Server.Models.ViewModels;
using DataLogicLibrary.DTO;
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

            var controller = new CarSimulatorController(null!, null!, null!, null!, null!, null!);
            var viewModel = new SimulationViewModel { SelectedAction = 7 };

            // Act
            var result = await controller.Index(viewModel);

            // Assert
            var redirect = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirect.ActionName);
        }

        [Fact]
      public async Task ShouldReturnDriver_WhenDriverIsNull()

       {
            //arrange
            var mockApiService = new Mock<IAPIService>();
            var mockDriverFactory = new Mock <IDriverFactory>();

            var fakeResultsDTO = new ResultsDTO
            {
                Results = new List<ResultDTO>
                {
                    new ResultDTO
                    { Name = new NameDTO {Title = "Mr", First = "John", Last = "Mohammed"},
                        Location= new LocationDTO {City = "Stockholm", Country = "Sweden"}

                    }
                }
            };

            var fakeDriver = new Driver
            {
                Title = "Mr",
                First = "John",
                Last = "Mohammed",
                City = "Stockholm",
                Country = "Sweden"
            };

            mockApiService.Setup(api => api.GetOneDriver()).ReturnsAsync(fakeResultsDTO);
            mockDriverFactory.Setup(factory => factory.CreateDriver(fakeResultsDTO)).Returns(fakeDriver);


            var controller = new CarSimulatorController
               (
                   mockApiService.Object,
                   Mock.Of<ISimulationLogicService>(),
                   Mock.Of<ICarFactory>(),
                   mockDriverFactory.Object,
                   Mock.Of<IStatusFactory>(),
                   Mock.Of<IStatusMessageService>()

               );

            var viewModel = new SimulationViewModel { SelectedAction = 1, Driver = null! };



            //Act 
            var result = await controller.Index(viewModel);

            //Assert 
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsType<SimulationViewModel>(viewResult.Model);

            Assert.NotNull(model.Driver);
            Assert.Equal("Mr", model.Driver.Title);
            Assert.Equal("John", model.Driver.First);
            Assert.Equal("Mohammed", model.Driver.Last);
            Assert.Equal("Stockholm", model.Driver.City);
            Assert.Equal("Sweden", model.Driver.Country);

            mockApiService.Verify(mockApiService => mockApiService.GetOneDriver(), Times.Once);
            mockDriverFactory.Verify(mockDriverFactory => mockDriverFactory.CreateDriver(fakeResultsDTO), Times.Once);

        }

        [Fact]
        public async Task WhenSelectedAction_GasAndEnergyValues_ShouldDeacrease()
        {
            //Arrange 
            var mocksimulationLogicService = new Mock<ISimulationLogicService>();
            var mockStatusMessageService = new Mock<IStatusMessageService>();

            mockStatusMessageService.Setup(service => service.GetCurrentActionMessage(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string>())).Returns("Action Performed");
            mockStatusMessageService.Setup(service => service.GetDriverStatusMessage(It.IsAny<int>(), It.IsAny<string>())).Returns("Driver Status OK");
            mockStatusMessageService.Setup(service => service.GetCarStatusMessage(It.IsAny<int>())).Returns("Car Status OK");


            var fakeinitialStatus = new StatusDTO
            {
                GasValue=100,
                EnergyValue= 100
            };

            var fakeDecreasedStatus = new StatusDTO
            {
                GasValue = 90,
                EnergyValue = 95
            };

            mocksimulationLogicService.Setup(service => service.DecreaseStatusValues(1, fakeinitialStatus)).Returns(fakeDecreasedStatus);
            mocksimulationLogicService.Setup(service => service.PerformAction(1, fakeDecreasedStatus)).Returns(fakeDecreasedStatus);

            var controller = new CarSimulatorController
                (
                    Mock.Of<IAPIService>(),
                    mocksimulationLogicService.Object,
                    Mock.Of<ICarFactory>(),
                    Mock.Of<IDriverFactory>(),
                    Mock.Of<IStatusFactory>(),
                    mockStatusMessageService.Object

                );

            var viewModel = new SimulationViewModel
            {
                IsRunning = true,        
                Car = new Car { },
                CurrentStatus = fakeinitialStatus,
                SelectedAction = 1,
                Driver = new Driver { First = "John" }
            };

            //act 
            var result = await controller.Index (viewModel);


            //assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsType<SimulationViewModel>(viewResult.Model);

            Assert.NotNull(model.CurrentStatus);
            Assert.Equal(90, model.CurrentStatus.GasValue);
            Assert.Equal(95, model.CurrentStatus.EnergyValue);

            mocksimulationLogicService.Verify(mocksimulationLogicService => mocksimulationLogicService.DecreaseStatusValues(1, fakeinitialStatus), Times.Once);
            mocksimulationLogicService.Verify(mocksimulationLogicService => mocksimulationLogicService.PerformAction(1, fakeDecreasedStatus), Times.Once);
        }

    }
}
