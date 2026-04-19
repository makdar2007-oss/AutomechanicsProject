using Xunit;
using AutomechanicsProject.Helpers;

namespace AutomechanicsProjectTest.Classes
{
    public class FormHelperTests
    {
        [Fact]
        public void IsWatermark_Empty_ReturnsTrue()
        {
            var result = FormHelper.IsWatermark("", "test");

            Assert.True(result);
        }

        [Fact]
        public void IsWatermark_SameAsWatermark_ReturnsTrue()
        {
            var result = FormHelper.IsWatermark("test", "test");

            Assert.True(result);
        }

        [Fact]
        public void IsWatermark_NormalText_ReturnsFalse()
        {
            var result = FormHelper.IsWatermark("hello", "test");

            Assert.False(result);
        }
    }
}