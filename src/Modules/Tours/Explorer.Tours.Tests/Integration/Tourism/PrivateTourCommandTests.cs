using Explorer.API.Controllers.Tourist.Tour;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.Administration;
using Explorer.Tours.Infrastructure.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.Tests.Integration.Tourism
{
    [Collection("Sequential")]
    public class PrivateTourCommandTests: BaseToursIntegrationTest
    {
        public PrivateTourCommandTests(ToursTestFactory factory) : base(factory) { }

        [Fact]
        public void createPrivateTour_Succeed()
        {
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var dbContext = scope.ServiceProvider.GetRequiredService<ToursContext>();
            PrivateTourDto privateTourDto = new PrivateTourDto()
            {
                Name = "tura 2",
                TouristId = -5,
                Checkpoints = new List<PublicCheckpointDto>()
                {
                    new PublicCheckpointDto()
                    {
                        Description = "test",
                        Id = 5,
                        Name = "test",
                        Longitude = 5,
                        Latitude = 5,
                        Pictures = new List<string>(){"p","f"}
                    },
                    new PublicCheckpointDto()
                    {
                        Description = "test",
                        Id = 6,
                        Name = "test",
                        Longitude = 6,
                        Latitude = 6,
                        Pictures = new List<string>(){"p","f"}
                    },
                    new PublicCheckpointDto()
                    {
                        Description = "test",
                        Id = 2,
                        Name = "test",
                        Longitude = 6,
                        Latitude = 6,
                        Pictures = new List<string>(){"p","f"}
                    }
                },
                Execution = null
            };
            privateTourDto.Execution = null;
            // Act
            var result = ((ObjectResult)controller.CreatePrivateTour(privateTourDto).Result)?.Value as PrivateTourDto;
            result.ShouldNotBeNull();
            result.Id.ShouldNotBe(0);
        }
        [Fact]
        public void createPrivateTour_Fail1()
        {
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var dbContext = scope.ServiceProvider.GetRequiredService<ToursContext>();
            PrivateTourDto privateTourDto = new PrivateTourDto()
            {
                Name = "",
                TouristId = -5,
                Checkpoints = new List<PublicCheckpointDto>()
                {
                    new PublicCheckpointDto()
                    {
                        Description = "test",
                        Id = 5,
                        Name = "test",
                        Longitude = 5,
                        Latitude = 5,
                        Pictures = new List<string>(){"p","f"}
                    },
                    new PublicCheckpointDto()
                    {
                        Description = "test",
                        Id = 6,
                        Name = "test",
                        Longitude = 6,
                        Latitude = 6,
                        Pictures = new List<string>(){"p","f"}
                    }
                },
                Execution = null
            };

            // Act
            ArgumentException ex = null;
            try
            {
                var result = ((ObjectResult)controller.CreatePrivateTour(privateTourDto).Result)?.Value as PrivateTourDto;
            }
            catch (ArgumentException e)
            {
                ex = e;
            }
            ex.ShouldNotBeNull();
        }
        [Fact]
        public void createPrivateTour_Fail2()
        {
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var dbContext = scope.ServiceProvider.GetRequiredService<ToursContext>();
            PrivateTourDto privateTourDto = new PrivateTourDto()
            {
                Name = "",
                TouristId = -5,
                Checkpoints = new List<PublicCheckpointDto>()
                {
                },
                Execution = null
            };
            // Act
            ArgumentException ex = null;
            try
            {
                var result = ((ObjectResult)controller.CreatePrivateTour(privateTourDto).Result)?.Value as PrivateTourDto;
            }catch(ArgumentException e)
            {
                ex = e;
            }
            ex.ShouldNotBeNull();
        }

        private static PrivateTourController CreateController(IServiceScope scope)
        {
            return new PrivateTourController(scope.ServiceProvider.GetRequiredService<IPrivateTourService>())
            {
                ControllerContext = BuildContext("-1")
            };
        }
    }
}
