using Explorer.API.Controllers.Tourist.Tour;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Public;
using Explorer.Stakeholders.Infrastructure.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Stakeholders.Tests.Integration.Administration
{
    public class ApplicationGradeTests : BaseStakeholdersIntegrationTest
    {
        public ApplicationGradeTests(StakeholdersTestFactory factory) : base(factory)
        {

        }


        [Fact]
        public void GiveHigherRating()
        {
            //Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var dto = new ApplicationGradeDto();
            dto.Rating = 6;
            dto.Comment = "This is the best app I have ever used";

            //Act
            var result = (ObjectResult)controller.EvaluateApplication(dto).Result;

            //Assert
            result.StatusCode.ShouldBe(500);
        }

        private static ApplicationGradeController CreateController(IServiceScope scope)
        {
            return new ApplicationGradeController(scope.ServiceProvider.GetRequiredService<IApplicationGradeService>());
        }
    }
}
