using System;
using APICheckerAPI.Models;
using Xunit;

namespace APIChecker.Tests
{ 
    public class CommandTests : IDisposable
    {
        Command testCommand;
        public CommandTests()
        {
            testCommand = new Command
            {
                HowTo = "Do something",
                Platform = "Some platform",
                CommandLine = "Some commandline"
             };
        }
        public void Dispose()
        {
            testCommand = null;
        }

        [Fact]
        public void CanChangeHowTo()
        {
            //Act
            testCommand.HowTo = "Execute Unit Tests";
            //Assert
            Assert.Equal("Execute Unit Tests", testCommand.HowTo);
        }
        [Fact]
        public void CanChangePlatform()
        {
            //Arrange
            //Act
            testCommand.Platform = "xUnit";
            //Assert
            Assert.Equal("xUnit", testCommand.Platform);
        }
        [Fact]
        public void CanChangeCommandLine()
        {
                //Arrange
                //Act
            testCommand.CommandLine = "dotnet test";
                //Assert
            Assert.Equal("dotnet test", testCommand.CommandLine);
        }
    }
}