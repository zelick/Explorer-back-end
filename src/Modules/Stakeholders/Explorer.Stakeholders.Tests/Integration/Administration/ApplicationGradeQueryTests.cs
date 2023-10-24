using Explorer.API.Controllers.Administrator.Administration;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Public;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.Administration;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Stakeholders.Tests.Integration.Administration
{
    public class ApplicationGradeQueryTests : BaseStakeholdersIntegrationTest
    {
        public ApplicationGradeQueryTests(StakeholdersTestFactory factory) : base(factory) { }

        [Fact]
        public void Retrieves_all()
        {
            // Arrange
            //using var scope = Factory.Services.CreateScope();
            //var controller = CreateController(scope);

            //// Act
            //var result = ((ObjectResult)controller.ReviewGrades(0, 0).Result)?.Value as PagedResult<ApplicationGradeDto>;

            //// Assert
            //result.ShouldNotBeNull();
            //result.Results.Count.ShouldBe(6);
            //result.TotalCount.ShouldBe(6);
        }

        private static ReviewGradeController CreateController(IServiceScope scope)
        {
            return new ReviewGradeController(scope.ServiceProvider.GetRequiredService<IApplicationGradeService>())
            {
                ControllerContext = BuildContext("-1")
            };
        }
    }
}
