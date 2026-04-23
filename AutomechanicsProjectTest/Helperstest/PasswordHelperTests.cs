using AutomechanicsProject.Helpers;
using Xunit;


public class PasswordHelperTests
{
    [Fact]
    public void HashPassword_ShouldReturnHash_NotEqualToOriginal()
    {
        var password = "test123";

        var hash = PasswordHelper.HashPassword(password);

        Assert.NotEqual(password, hash);
    }

    [Fact]
    public void HashPassword_ShouldReturnDifferentHashes_ForSamePassword()
    {
        var password = "test123";

        var hash1 = PasswordHelper.HashPassword(password);
        var hash2 = PasswordHelper.HashPassword(password);

        Assert.NotEqual(hash1, hash2);
    }

    [Fact]
    public void VerifyPassword_ShouldReturnTrue_ForCorrectPassword()
    {
        var password = "test123";
        var hash = PasswordHelper.HashPassword(password);

        var result = PasswordHelper.VerifyPassword(password, hash);

        Assert.True(result);
    }

    [Fact]
    public void VerifyPassword_ShouldReturnFalse_ForWrongPassword()
    {
        var hash = PasswordHelper.HashPassword("test123");

        var result = PasswordHelper.VerifyPassword("wrong", hash);

        Assert.False(result);
    }

    [Fact]
    public void VerifyPassword_ShouldReturnFalse_ForEmptyPassword()
    {
        var hash = PasswordHelper.HashPassword("test123");

        var result = PasswordHelper.VerifyPassword("", hash);

        Assert.False(result);
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
}