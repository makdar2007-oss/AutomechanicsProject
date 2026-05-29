using Xunit;
using AutomechanicsProject.Classes;
using System;

namespace AutomechanicsProject.Tests.Classes
{
    public class MoscowTimeTests
    {
        [Fact]
        public void Now_ReturnsCurrentTime()
        {
            var now = MoscowTime.Now;
            Assert.NotNull(now);
        }

        [Fact]
        public void Today_ReturnsDateWithoutTime()
        {
            var today = MoscowTime.Today;
            Assert.Equal(today.Date, today);
        }

        [Fact]
        public void ConvertToMoscow_Works()
        {
            var utcTime = new DateTime(2024, 1, 1, 10, 0, 0, DateTimeKind.Utc);
            var moscowTime = MoscowTime.ConvertToMoscow(utcTime);

            // Москва UTC+3
            Assert.Equal(13, moscowTime.Hour);
        }
    }
}