using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Web.Http;
using System.Web.Http.Results;
using CasCap.Controllers;
using Microsoft.Extensions.Logging;
using Moq;
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
            var result = _controller.TestDI() as OkNegotiatedContentResult<List<int>>;

            // Assert
            Assert.IsNotNull(result);
            CollectionAssert.AreEqual(expectedValues, result.Content);
            _loggerMock.Verify(l => l.LogTrace(It.IsAny<string>()), Times.Once);
        }
    }
}
