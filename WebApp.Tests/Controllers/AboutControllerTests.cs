using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.AspNetCore.Mvc;
using CasCap.Controllers;
using Microsoft.Extensions.Logging;
using Moq;
using System.Collections.Generic;

namespace CasCap.Tests.Controllers
{
    [TestClass]
    public class AboutControllerTests
    {
        private Mock<ILogger<AboutController>> _loggerMock;
        private Mock<IDITestService> _serviceMock;
        private AboutController _controller;

        [TestInitialize]
        public void Setup()
        {
            _loggerMock = new Mock<ILogger<AboutController>>();
            _serviceMock = new Mock<IDITestService>();
            _controller = new AboutController(_loggerMock.Object, _serviceMock.Object);
        }

        [TestMethod]
        public void Index_ReturnsViewResult()
        {
            // Arrange
            var intValues = new List<int> { 2024, 3, 15 };
            var stringValues = new List<string> { "TestMachine", "2024-03-15" };
            _serviceMock.Setup(s => s.GetIntValues()).Returns(intValues);
            _serviceMock.Setup(s => s.GetStringValues()).Returns(stringValues);

            // Act
            var result = _controller.Index() as ViewResult;
            var model = result?.Model as IndexViewModel;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsNotNull(model);
            CollectionAssert.AreEqual(intValues, model.SomeIntValues);
            CollectionAssert.AreEqual(stringValues, model.SomeStringValues);
        }
    }
}
