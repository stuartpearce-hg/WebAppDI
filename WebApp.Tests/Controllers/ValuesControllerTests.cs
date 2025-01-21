using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.AspNetCore.Mvc;
using CasCap.Controllers;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;

namespace CasCap.Tests.Controllers
{
    [TestClass]
    public class ValuesControllerTests
    {
        private Mock<ILogger<ValuesController>> _loggerMock;
        private Mock<IDITestService> _serviceMock;
        private ValuesController _controller;

        [TestInitialize]
        public void Setup()
        {
            _loggerMock = new Mock<ILogger<ValuesController>>();
            _serviceMock = new Mock<IDITestService>();
            _controller = new ValuesController(_loggerMock.Object, _serviceMock.Object);
        }

        [TestMethod]
        public void TestDI_ReturnsOkResultWithValues()
        {
            // Arrange
            var expectedValues = new List<int> { 2024, 3, 15 };
            _serviceMock.Setup(s => s.GetIntValues()).Returns(expectedValues);

            // Act
            var result = _controller.TestDI() as OkObjectResult;

            // Assert
            Assert.IsNotNull(result);
            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
            CollectionAssert.AreEqual(expectedValues, okResult.Value as List<int>);
            _loggerMock.Verify(l => l.Log(
                LogLevel.Trace,
                It.IsAny<EventId>(),
                It.Is<It.IsAnyType>((v, t) => true),
                It.IsAny<Exception>(),
                It.Is<Func<It.IsAnyType, Exception, string>>((v, t) => true)),
                Times.Once);
        }
    }
}
