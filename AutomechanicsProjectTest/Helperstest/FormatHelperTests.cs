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
            var date = new DateTime(2024, 1, 15);

            var result = FormatHelper.FormatDate(date);

            Assert.Equal("15.01.2024", result);
        }

        [Fact]
        public void FormatDate_Null_ReturnsDash()
        {
            var result = FormatHelper.FormatDate(null);

            Assert.Equal("—", result);
        }

        [Fact]
        public void FormatDateTime_ReturnsCorrectFormat()
        {
            var date = new DateTime(2024, 1, 15, 14, 30, 0);

            var result = FormatHelper.FormatDateTime(date);

            Assert.Equal("15.01.2024 14:30", result);
        }

        [Fact]
        public void FormatProductShort_ReturnsCorrectString()
        {
            var result = FormatHelper.FormatProductShort("A1", "Товар");

            Assert.Equal("A1 - Товар", result);
        }
        
        [Fact]
        public void FormatBalance_ReturnsCorrectString()
        {
            var result = FormatHelper.FormatBalance(10, "шт");

            Assert.Equal("10 шт", result);
        }

        [Fact]
        public void FormatProductWithBalance_ReturnsCorrect()
        {
            var result = FormatHelper.FormatProductWithBalance("A1", "Товар", 5, "шт");

            Assert.Equal("A1 - Товар (остаток: 5 шт)", result);
        }       

        [Fact]
        public void FormatExpiryDateDisplay_WithDate()
        {
            var date = new DateTime(2025, 5, 1);

            var result = FormatHelper.FormatExpiryDateDisplay(date, 10);

            Assert.Contains("01.05.2025", result);
        }

        [Fact]
        public void FormatExpiryDateDisplay_WithoutDate()
        {
            var result = FormatHelper.FormatExpiryDateDisplay(null, 10);

            Assert.Contains("Без срока", result);
        }   
    }
}