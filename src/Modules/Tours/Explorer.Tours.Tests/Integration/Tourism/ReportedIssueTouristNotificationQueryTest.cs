//using Explorer.API.Controllers.Tourist;
//using Explorer.BuildingBlocks.Core.UseCases;
//using Explorer.Tours.API.Dtos;
//using Explorer.Tours.API.Public.Administration;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.Extensions.DependencyInjection;
//using Shouldly;

//namespace Explorer.Tours.Tests.Integration.Tourism;
//[Collection("Sequential")]
//public class ReportedIssueTouristNotificationQueryTest : BaseToursIntegrationTest
//{
//    public ReportedIssueTouristNotificationQueryTest(ToursTestFactory factory) : base(factory) { }

//    [Fact]
//    public void Retrieves_AllByUser()
//    {
//        // Arrange
//        using var scope = Factory.Services.CreateScope();
//        var controller = CreateController(scope);

//        // Act
//        var result = ((ObjectResult)controller.GetAllByUser(2, 0, 0).Result)?.Value as PagedResult<ReportedIssueNotificationDto>;

//        // Assert
//        result.ShouldNotBeNull();
//        result.Results.Count.ShouldBe(2);
//        result.TotalCount.ShouldBe(2);
//    }

//    [Fact]
//    public void Retrieves_UnreadByUser()
//    {
//        // Arrange
//        using var scope = Factory.Services.CreateScope();
//        var controller = CreateController(scope);

//        // Act
//        var result = ((ObjectResult)controller.GetUnreadByUser(2, 0, 0).Result)?.Value as PagedResult<ReportedIssueNotificationDto>;

//        // Assert
//        result.ShouldNotBeNull();
//        result.Results.Count.ShouldBe(1);
//        result.TotalCount.ShouldBe(1);
//    }

//    private static ReportedIssueTouristNotificationController CreateController(IServiceScope scope)
//    {
//        return new ReportedIssueTouristNotificationController(scope.ServiceProvider.GetRequiredService<IReportedIssueNotificationService>())
//        {
//            ControllerContext = BuildContext("-1")
//        };
//    }
//}
