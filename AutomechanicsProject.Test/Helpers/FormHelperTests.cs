using Xunit;
using AutomechanicsProject.Helpers;

namespace AutomechanicsProject.Tests.Helpers
{
    public class FormHelperTests
    {
        [Fact]
        public void IsWatermark_WithEmptyText_ReturnsTrue()
        {
            var result = FormHelper.IsWatermark("", "watermark");
            Assert.True(result);
        }

        [Fact]
        public void IsWatermark_WithNullText_ReturnsTrue()
        {
            var result = FormHelper.IsWatermark(null, "watermark");
            Assert.True(result);
        }

        [Fact]
        public void IsWatermark_WithWatermarkText_ReturnsTrue()
        {
            var result = FormHelper.IsWatermark("watermark", "watermark");
            Assert.True(result);
        }

        [Fact]
        public void IsWatermark_WithNormalText_ReturnsFalse()
        {
            var result = FormHelper.IsWatermark("normal text", "watermark");
            Assert.False(result);
        }
    }
}