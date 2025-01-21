using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace CasCap.Tests.Services
{
    [TestClass]
    public class DITestServiceTests
    {
        private DITestService _service;

        [TestInitialize]
        public void Setup()
        {
            _service = new DITestService();
        }

        [TestMethod]
        public void GetIntValues_ReturnsCurrentDateTimeComponents()
        {
            // Act
            var result = _service.GetIntValues();

            // Assert
            Assert.AreEqual(5, result.Count);
            Assert.AreEqual(DateTime.UtcNow.Year, result[0]);
            Assert.AreEqual(DateTime.UtcNow.Month, result[1]);
            Assert.AreEqual(DateTime.UtcNow.Day, result[2]);
            // Skip minute and second assertions as they might change during test execution
        }

        [TestMethod]
        public void GetStringValues_ReturnsExpectedValues()
        {
            // Act
            var result = _service.GetStringValues();

            // Assert
            Assert.AreEqual(2, result.Count);
            Assert.AreEqual(Environment.MachineName, result[0]);
            Assert.IsTrue(DateTime.TryParse(result[1], out _));
        }
    }
}
