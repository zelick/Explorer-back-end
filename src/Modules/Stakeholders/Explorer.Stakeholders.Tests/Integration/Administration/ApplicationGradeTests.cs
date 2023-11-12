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
        public void Give_higher_rating()
        {
            //Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var dto = new ApplicationGradeDto();
            dto.Rating = 6;
            dto.Comment = "This is the best app I have ever used";

            //Act
            var ex = Assert.Throws<ArgumentException>(() => controller.EvaluateApplication(dto));

            //Assert
            Assert.Equal("Invalid rating", ex.Message);
        }

        [Fact]
        public void Add_empty_comment()
        {
            //Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var dto = new ApplicationGradeDto();
            dto.UserId = -1;
            dto.Created = DateTime.Now;
            dto.Rating = 4;
            dto.Comment = "";

            //Act
            var result = (ObjectResult)controller.EvaluateApplication(dto).Result;

            //Assert
            result.StatusCode.ShouldBe(200);
        }

        private static ApplicationGradeController CreateController(IServiceScope scope)
        {
            return new ApplicationGradeController(scope.ServiceProvider.GetRequiredService<IApplicationGradeService>());
        }
    }
}
