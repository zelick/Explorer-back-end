using Explorer.API.Controllers.Author.Administration;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.Administration;
using Explorer.Tours.Infrastructure.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;

namespace Explorer.Tours.Tests.Integration.Administration;
[Collection("Sequential")]
public class ReportedIssueAuthorNotificationCommandTests : BaseToursIntegrationTest
{
    public ReportedIssueAuthorNotificationCommandTests(ToursTestFactory factory) : base(factory) { }

    //[Fact]
    //public void Updates()
    //{
    //    // Arrange
    //    using var scope = Factory.Services.CreateScope();
    //    var controller = CreateController(scope);
    //    var dbContext = scope.ServiceProvider.GetRequiredService<ToursContext>();
    //    var updatedEntity = new ReportedIssueNotificationDto
    //    {
    //        Id = -2,
    //        Description = "Old notification!!!",
    //        IsRead = true
    //    };

    //    // Act
    //    var result = ((ObjectResult)controller.Update(updatedEntity).Result)?.Value as ReportedIssueNotificationDto;

    //    // Assert - Response
    //    result.ShouldNotBeNull();
    //    result.Id.ShouldBe(-2);
    //    result.Description.ShouldBe(updatedEntity.Description);
    //    result.IsRead.ShouldBe(updatedEntity.IsRead);

    //    // Assert - Database
    //    var storedEntity = dbContext.ReportedIssueNotifications.FirstOrDefault(i => i.Description == "Old notification!!!");
    //    storedEntity.ShouldNotBeNull();
    //    storedEntity.Description.ShouldBe(updatedEntity.Description);
    //    var oldEntity = dbContext.ReportedIssueNotifications.FirstOrDefault(i => i.Description == "Old notification!!!");
    //    oldEntity.ShouldBeNull();
    //}

    //[Fact]
    //public void Update_fails_invalid_id()
    //{
    //    // Arrange
    //    using var scope = Factory.Services.CreateScope();
    //    var controller = CreateController(scope);
    //    var updatedEntity = new ReportedIssueNotificationDto
    //    {
    //        Id = -100,
    //        IsRead = true
    //    };

    //    // Act
    //    var result = (ObjectResult)controller.Update(updatedEntity).Result;

    //    // Assert
    //    result.ShouldNotBeNull();
    //    result.StatusCode.ShouldBe(404);
    //}

    [Fact]
    public void Deletes()
    { 
        // Arrange
        using var scope = Factory.Services.CreateScope();
        var controller = CreateController(scope);
        var dbContext = scope.ServiceProvider.GetRequiredService<ToursContext>();

        // Act
        var result = (OkResult)controller.Delete(-2);

        // Assert - Response
        result.ShouldNotBeNull();
        result.StatusCode.ShouldBe(200);

        // Assert - Database
        var storedCourse = dbContext.ReportedIssueNotifications.FirstOrDefault(i => i.Id == -2);
        storedCourse.ShouldBeNull();
    }

    [Fact]
    public void Delete_fails_invalid_id()
    {
        // Arrange
        using var scope = Factory.Services.CreateScope();
        var controller = CreateController(scope);

        // Act
        var result = (ObjectResult)controller.Delete(-1000);

        // Assert
        result.ShouldNotBeNull();
        result.StatusCode.ShouldBe(404);
    }

    private static ReportedIssueAuthorNotificationController CreateController(IServiceScope scope)
    {
        return new ReportedIssueAuthorNotificationController(scope.ServiceProvider.GetRequiredService<IReportedIssueNotificationService>())
        {
            ControllerContext = BuildContext("-1")
        };
    }
}