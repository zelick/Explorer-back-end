using AutoMapper;
using Explorer.API.Controllers;
using Explorer.API.Controllers.Administrator.Administration;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Public;
using Explorer.Stakeholders.Core.Domain;
using Explorer.Stakeholders.Core.UseCases;
using Explorer.Stakeholders.Infrastructure.Database;
using FluentResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Stakeholders.Tests.Integration.Authentication
{
    [Collection("Sequential")]
    public class AccountsManagementTests : BaseStakeholdersIntegrationTest
    {
        public AccountsManagementTests(StakeholdersTestFactory factory) : base(factory) { }

        [Fact]
        public void Successfully_get_all_accounts()
        {
            //Arange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var dbContext = scope.ServiceProvider.GetRequiredService<StakeholdersContext>();

            //Act
            var page = 1;
            var pageSize = 10;
            var result = controller.GetAll(page, pageSize);

            // Assert
            result.ShouldNotBeNull();
            var objectResult = (result.Result as ObjectResult);
            objectResult.ShouldNotBeNull();
            objectResult.StatusCode.ShouldBe(200);
        }


        [Fact]
        public void Block_user_successfully()
        {
            //Arange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var dbContext = scope.ServiceProvider.GetRequiredService<StakeholdersContext>();
            var userDto = new UserDto
            {
                Id = -1,
                IsActive = true,
                Role = RoleUser.Administrator,
                Email = "admin@gmail.com",
                Password = "admin",
                Username = "admin@gmail.com" // like as in b-users.sql
            };

            // Act
            var userBlockResponse = ((ObjectResult)controller.Block(userDto.Id).Result).Value as UserDto;

            // Assert - Response
            userBlockResponse.ShouldBeNull(); //because that user doesn't have his corresponding person


            userDto = new UserDto
            {
                Id = -11,
                IsActive = true,
                Role = RoleUser.Author,
                Email = "autor1@gmail.com",
                Password = "autor1",
                Username = "autor1@gmail.com" // like as in b-users.sql
            };

            userBlockResponse = ((ObjectResult)controller.Block(userDto.Id).Result).Value as UserDto;
            userBlockResponse.ShouldNotBeNull();
            userBlockResponse.Id.ShouldBe(userDto.Id);
            userBlockResponse.IsActive.ShouldBe(false);

            // Assert - Database
            dbContext.ChangeTracker.Clear();
            var storedAccount = dbContext.Users.FirstOrDefault(u => u.Username == userDto.Username);

            storedAccount.ShouldNotBeNull();
            storedAccount.Role.ShouldBe(UserRole.Author);
            storedAccount.Username.ShouldBe(userDto.Username);
        }


        private static AccountsManagementController CreateController(IServiceScope scope)
        {
            return new AccountsManagementController(scope.ServiceProvider.GetRequiredService<IAccountsManagementService>());
        }
    }
}
