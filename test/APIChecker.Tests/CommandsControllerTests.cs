using System;
using System.Collections.Generic;
using APICheckerAPI.Controllers;
using APICheckerAPI.Data;
using APICheckerAPI.Dtos;
using APICheckerAPI.Models;
using APICheckerAPI.Profiles;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using Xunit;

namespace APIChecker.Tests
{
    public class CommandsControllerTests : IDisposable
    {
        private ICommandAPIRepo mockRepo;
        CommandsProfile realProfile;
        MapperConfiguration configuration;
        IMapper mapper;
        
        public CommandsControllerTests()
        {
            mockRepo = Substitute.For<ICommandAPIRepo>();
            realProfile = new CommandsProfile();
            configuration = new MapperConfiguration(cfg => cfg.AddProfile(realProfile));
            mapper = new Mapper(configuration);
        }
        [Fact]
        public void GetCommandItems_ReturnsZeroItems_WhenDBIsEmpty()
        {
            //Arrange
            //We need to create an instance of our CommandsController class
            var controller = new CommandsController(mockRepo, mapper);
        }
        
        [Fact]
        public void GetCommandItems_Returns200OK_WhenDBIsEmpty()
        {
            //Arrange
            mockRepo.GetAllCommands().Returns(GetCommands(0));
            var controller = new CommandsController(mockRepo, mapper);
            
            //Act
            var result = controller.GetAllCommands();
            //Assert
            Assert.IsType<OkObjectResult>(result.Result);
        }
        [Fact]
        public void GetAllCommands_ReturnsCorrectType_WhenDBHasOneResource()
        {
            //Arrange
            mockRepo.GetAllCommands().Returns(GetCommands(1));
            var controller = new CommandsController(mockRepo, mapper);
            //Act
            var result = controller.GetAllCommands();
            //Assert
            Assert.IsType<ActionResult<IEnumerable<CommandReadDto>>>(result);
        }
        [Fact]
        public void GetCommandByID_Returns200OK__WhenValidIDProvided()
        {
            //Arrange
            mockRepo.GetCommandById(1).Returns(new 
                Command { Id = 1,
                HowTo = "mock",
                Platform = "Mock",
                CommandLine = "Mock" });
            var controller = new CommandsController(mockRepo, mapper);
            //Act
            var result = controller.GetCommandById(1);
            //Assert
            Assert.IsType<OkObjectResult>(result.Result);
        }
        [Fact]
        public void GetAllCommands_ReturnsOneItem_WhenDBHasOneResource()
        {
            //Arrange
            mockRepo.GetAllCommands().Returns(GetCommands(1));
            var controller = new CommandsController(mockRepo, mapper);
            //Act
            var result = controller.GetAllCommands();
            //Assert
            var okResult = result.Result as OkObjectResult;
            var commands = okResult.Value as List<CommandReadDto>;
            Assert.Single(commands);
        }
        
        [Fact]
        public void GetCommandByID_Returns404NotFound_WhenNonExistentIDProvided()
        {
            //Arrange
            mockRepo.GetCommandById(0).ReturnsNull();
            var controller = new CommandsController(mockRepo, mapper);
            //Act
            var result = controller.GetCommandById(1);
            //Assert
            Assert.IsType<NotFoundResult>(result.Result);
        }
        
        [Fact]
        public void CreateCommand_Returns201Created_WhenValidObjectSubmitted()
        {
            //Arrange
            mockRepo.GetCommandById(1).Returns(new 
                Command { Id = 1,
                    HowTo = "mock",
                    Platform = "Mock",
                    CommandLine = "Mock" });
            var controller = new CommandsController(mockRepo, mapper);
            //Act
            var result = controller.CreateCommand(new CommandCreateDto { });
            //Assert
            Assert.IsType<CreatedAtRouteResult>(result.Result);
        }
        [Fact]
        public void UpdateCommand_Returns404NotFound_WhenNonExistentResourceIDSubmitted()
        {
            //Arrange
            mockRepo.GetCommandById(1).Returns(new 
                Command { Id = 1,
                    HowTo = "mock",
                    Platform = "Mock",
                    CommandLine = "Mock" });
            var controller = new CommandsController(mockRepo, mapper);
            //Act
            var result = controller.UpdateCommand(0, new CommandUpdateDto { });
            //Assert
            Assert.IsType<NotFoundResult>(result);
        }
        [Fact]
        public void UpdateCommand_Returns204NoContent_WhenValidObjectSubmitted()
        {
            //Arrange
            mockRepo.GetCommandById(1).Returns(new 
                Command { Id = 1,
                    HowTo = "mock",
                    Platform = "Mock",
                    CommandLine = "Mock" });
            var controller = new CommandsController(mockRepo, mapper);
            //Act
            var result = controller.UpdateCommand(1, new CommandUpdateDto { });
            //Assert
            Assert.IsType<NoContentResult>(result);
        }
        
        [Fact]
        public void CreateCommand_ReturnsCorrectResourceType_WhenValidObjectSubmitted()
        {
            //Arrange
            mockRepo.GetCommandById(1).Returns(new 
                Command { Id = 1,
                    HowTo = "mock",
                    Platform = "Mock",
                    CommandLine = "Mock" });
            var controller = new CommandsController(mockRepo, mapper);
            //Act
            var result = controller.CreateCommand(new CommandCreateDto { });
            //Assert
            Assert.IsType<ActionResult<CommandReadDto>>(result);
        }
        
        [Fact]
        public void PartialCommandUpdate_Returns404NotFound_WhenNonExistentResourceIDSubmitted()
        {
            //Arrange
            mockRepo.GetCommandById(0).ReturnsNull();
            var controller = new CommandsController(mockRepo, mapper);
            //Act
            var result = controller.PartialCommandUpdate(0,
                new Microsoft.AspNetCore.JsonPatch.JsonPatchDocument<CommandUpdateDto> { });
            //Assert
            Assert.IsType<NotFoundResult>(result);
        }
        
        [Fact]
        public void DeleteCommand_Returns204NoContent_WhenValidResourceIDSubmitted()
        {
            //Arrange
            mockRepo.GetCommandById(1).Returns(new 
                Command { Id = 1,
                    HowTo = "mock",
                    Platform = "Mock",
                    CommandLine = "Mock" });
            var controller = new CommandsController(mockRepo, mapper);
            //Act
            var result = controller.DeleteCommand(1);
            //Assert
            Assert.IsType<NoContentResult>(result);
        }
        
        [Fact]
        public void DeleteCommand_Returns_404NotFound_WhenNonExistentResourceIDSubmitted()
        {
            //Arrange
            mockRepo.GetCommandById(0).ReturnsNull();
            var controller = new CommandsController(mockRepo, mapper);
            //Act
            var result = controller.DeleteCommand(0);
            //Assert
            Assert.IsType<NotFoundResult>(result);
        }
        private List<Command> GetCommands(int num)
        {
            var commands = new List<Command>();
            if (num > 0){
                commands.Add(new Command
                {
                    Id = 0,
                    HowTo = "How to generate a migration",
                    CommandLine = "dotnet ef migrations add <Name of Migration>",
                    Platform = ".Net Core EF"
                });
            }
            return commands;
        }
        
        public void Dispose()
        {
            mockRepo = null;
            mapper = null;
            configuration = null;
            realProfile = null;
        }
    }
}