using Xunit;
using AutomechanicsProject.Helpers;
using System;

namespace AutomechanicsProjectTest.Classes
{
    public class FormatHelperTests
    {
        [Fact]
        public void FormatDate_WithValue_ReturnsFormatted()
        {
            var result = FormatHelper.FormatDate(new DateTime(2024, 1, 15));
            Assert.Equal("15.01.2024", result);
        }

        [Fact]
        public void FormatDate_Null_ReturnsDash()
        {
            var result = FormatHelper.FormatDate(null);
            Assert.Equal("—", result);
        }

        [Fact]
        public void FormatDateTime_ReturnsCorrect()
        {
            var result = FormatHelper.FormatDateTime(new DateTime(2024, 1, 15, 14, 30, 0));
            Assert.Equal("15.01.2024 14:30", result);
        }

        [Fact]
        public void FormatProductShort_ReturnsCorrect()
        {
            var result = FormatHelper.FormatProductShort("A1", "Товар");
            Assert.Equal("A1 - Товар", result);
        }

        [Fact]
        public void FormatBalance_ReturnsCorrect()
        {
            var result = FormatHelper.FormatBalance(10, "шт");
            Assert.Equal("10 шт", result);
        }

        [Fact]
        public void FormatDate_MinValue_Works()
        {
            var result = FormatHelper.FormatDate(DateTime.MinValue);

            Assert.Equal("01.01.0001", result);
        }

        [Fact]
        public void FormatDateTime_SecondsIgnored()
        {
            var result = FormatHelper.FormatDateTime(new DateTime(2024, 1, 15, 14, 30, 59));

            Assert.Equal("15.01.2024 14:30", result);
        }

        [Fact]
        public void FormatProductShort_EmptyValues()
        {
            var result = FormatHelper.FormatProductShort("", "");

            Assert.Equal(" - ", result);
        }

        [Fact]
        public void FormatBalance_Zero_ReturnsCorrect()
        {
            var result = FormatHelper.FormatBalance(0, "шт");

            Assert.Equal("0 шт", result);
        }

        [Fact]
        public void FormatBalance_Negative_ReturnsCorrect()
        {
            var result = FormatHelper.FormatBalance(-5, "шт");

            Assert.Equal("-5 шт", result);
        }
    }
}