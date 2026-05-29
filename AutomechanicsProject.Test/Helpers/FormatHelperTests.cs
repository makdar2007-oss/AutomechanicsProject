using Xunit;
using AutomechanicsProject.Helpers;
using System;

namespace AutomechanicsProject.Tests.Helpers
{
    public class FormatHelperTests
    {
        [Fact]
        public void FormatDate_WithDate_ReturnsFormattedString()
        {
            var date = new DateTime(2025, 12, 31);
            var result = FormatHelper.FormatDate(date);
            Assert.Equal("31.12.2025", result);
        }

        [Fact]
        public void FormatDate_WithNull_ReturnsDash()
        {
            var result = FormatHelper.FormatDate(null);
            Assert.Equal("—", result);
        }

        [Fact]
        public void FormatDate_WithCustomFormat_ReturnsFormatted()
        {
            var date = new DateTime(2025, 12, 31);
            var result = FormatHelper.FormatDate(date, "yyyy-MM-dd");
            Assert.Equal("2025-12-31", result);
        }

        [Fact]
        public void FormatDateTime_ReturnsFormatted()
        {
            var date = new DateTime(2025, 12, 31, 14, 30, 0);
            var result = FormatHelper.FormatDateTime(date);
            Assert.Equal("31.12.2025 14:30", result);
        }

        [Fact]
        public void FormatProductShort_ReturnsArticleDashName()
        {
            var result = FormatHelper.FormatProductShort("ART-001", "Масло");
            Assert.Equal("ART-001 - Масло", result);
        }

        [Fact]
        public void FormatProductWithBalance_FormatsCorrectly()
        {
            var result = FormatHelper.FormatProductWithBalance("ART-001", "Масло", 10, "л");
            Assert.Contains("ART-001", result);
            Assert.Contains("Масло", result);
            Assert.Contains("10", result);
            Assert.Contains("л", result);
        }

        [Fact]
        public void FormatPrice_ReturnsPriceWithCurrency()
        {
            var result = FormatHelper.FormatPrice(1500.50m, "USD");
            Assert.Equal("1500,50 USD", result);
        }

        [Fact]
        public void FormatPurchasePrice_ReturnsRubles()
        {
            var result = FormatHelper.FormatPurchasePrice(1000m);
            Assert.Equal("1000,00 ₽", result);
        }

        [Fact]
        public void FormatBalance_ReturnsBalanceWithUnit()
        {
            var result = FormatHelper.FormatBalance(25, "шт");
            Assert.Equal("25 шт", result);
        }

        [Fact]
        public void FormatUnitDisplay_ReturnsNameWithShortName()
        {
            var result = FormatHelper.FormatUnitDisplay("Литр", "л");
            Assert.Equal("Литр (л)", result);
        }

        [Fact]
        public void FormatExpiryDateDisplay_WithExpiryDate_FormatsCorrectly()
        {
            var expiryDate = new DateTime(2025, 12, 31);
            var result = FormatHelper.FormatExpiryDateDisplay(expiryDate, 10);
            Assert.Contains("31.12.2025", result);
            Assert.Contains("10", result);
        }

        [Fact]
        public void FormatExpiryDateDisplay_WithoutExpiryDate_ShowsNoExpiry()
        {
            var result = FormatHelper.FormatExpiryDateDisplay(null, 10);
            Assert.Contains("10", result);
        }
    }
}