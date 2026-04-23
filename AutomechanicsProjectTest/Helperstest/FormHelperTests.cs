using Xunit;
using AutomechanicsProject.Helpers;

namespace AutomechanicsProjectTest.Classes
{
    public class FormHelperTests
    {
        [Fact]
        public void IsWatermark_Empty_ReturnsTrue()
        {
            Assert.True(FormHelper.IsWatermark("", "test"));
        }

        [Fact]
        public void IsWatermark_Same_ReturnsTrue()
        {
            Assert.True(FormHelper.IsWatermark("test", "test"));
        }

        [Fact]
        public void IsWatermark_Normal_ReturnsFalse()
        {
            Assert.False(FormHelper.IsWatermark("hello", "test"));
        }

        [Fact]
        public void IsWatermark_NullText_ReturnsTrue()
        {
            Assert.True(FormHelper.IsWatermark(null, "test"));
        }

        [Fact]
        public void IsWatermark_NullWatermark_ReturnsFalse()
        {
            Assert.False(FormHelper.IsWatermark("text", null));
        }

        [Fact]
        public void IsWatermark_DifferentCase_ReturnsFalse()
        {
            Assert.False(FormHelper.IsWatermark("Test", "test"));
        }

        [Fact]
        public void IsWatermark_LongText_ReturnsFalse()
        {
            var text = new string('a', 1000);

            Assert.False(FormHelper.IsWatermark(text, "test"));
        }
    }
}