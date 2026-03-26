using Xunit;

public class ValidationTests
{
    [Fact]
    public void ValidateRequiredFields_ShouldReturnFalse_WhenFieldIsEmpty()
    {
        var result = Validation.ValidateRequiredFields(
            ("", "Введите имя")
        );

        Assert.False(result);
    }

    [Fact]
    public void ValidateRequiredFields_ShouldReturnTrue_WhenAllFieldsFilled()
    {
        var result = Validation.ValidateRequiredFields(
            ("Иван", "Введите имя")
        );

        Assert.True(result);
    }

    [Theory]
    [InlineData("100", true)]
    [InlineData("-5", false)]
    [InlineData("abc", false)]
    public void ValidatePrice_ShouldWorkCorrect(string input, bool expected)
    {
        var result = Validation.ValidatePrice(input, out decimal price);

        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData("10", true)]
    [InlineData("0", false)]
    [InlineData("-1", false)]
    public void ValidateQuantity_ShouldWorkCorrect(string input, bool expected)
    {
        var result = Validation.ValidateQuantity(input, out int quantity);

        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData("Иван", true)]
    [InlineData("John", false)]
    public void IsValidRussianName_ShouldWork(string name, bool expected)
    {
        var result = Validation.IsValidRussianName(name);

        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData("user_123", true)]
    [InlineData("user@", false)]
    public void IsValidLogin_ShouldWork(string login, bool expected)
    {
        var result = Validation.IsValidLogin(login);

        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData("123", false)]
    [InlineData("abcdef", false)]
    [InlineData("abc123", true)]
    public void IsValidPassword_ShouldWork(string password, bool expected)
    {
        var result = Validation.IsValidPassword(password);

        Assert.Equal(expected, result);
    }

    [Fact]
    public void IsPasswordMatch_ShouldReturnTrue_WhenEqual()
    {
        var result = Validation.IsPasswordMatch("123", "123");

        Assert.True(result);

    }
    [Fact]
    public void ValidateRegistrationFields_ShouldReturnFalse_WhenPasswordsDontMatch()
    {
        var result = Validation.ValidateRegistrationFields(
            "Иванов", "фамилия",
            "Иван", "имя",
            "user123", "логин",
            "pass123", "пароль",
            "wrong", "подтверждение",
            out string error
        );

        Assert.False(result);
        Assert.Equal("Пароли не совпадают!", error);
    }
    [Theory]
    [InlineData("ab", false)]
    [InlineData("abc", true)]
    [InlineData("thisisveryverylonglogin123", false)]
    public void IsValidLoginLength_ShouldWork(string login, bool expected)
    {
        var result = Validation.IsValidLoginLength(login);

        Assert.Equal(expected, result);
    }
    [Theory]
    [InlineData("А", false)]
    [InlineData("Иван", true)]
    public void IsValidNameLength_ShouldWork(string name, bool expected)
    {
        var result = Validation.IsValidNameLength(name);

        Assert.Equal(expected, result);
    }
    [Fact]
    public void ValidateRegistrationFields_ShouldFail_WhenPasswordEmpty()
    {
        var result = Validation.ValidateRegistrationFields(
            "Иванов", "фамилия",
            "Иван", "имя",
            "user123", "логин",
            "", "пароль",
            "", "подтверждение",
            out string error
        );

        Assert.False(result);
        Assert.Equal("Введите пароль", error);
    }
    [Fact]
    public void ValidateRegistrationFields_ShouldReturnTrue_WhenAllValid()
    {
        var result = Validation.ValidateRegistrationFields(
            "Иванов", "фамилия",
            "Иван", "имя",
            "user123", "логин",
            "pass123", "пароль",
            "pass123", "подтверждение",
            out string error
        );

        Assert.True(result);
        Assert.Equal(string.Empty, error);
    }
    [Fact]
    public void ValidateRegistrationFields_ShouldFail_WhenSurnameEmpty()
    {
        var result = Validation.ValidateRegistrationFields(
            "", "фамилия",
            "Иван", "имя",
            "user123", "логин",
            "pass123", "пароль",
            "pass123", "подтверждение",
            out string error
        );

        Assert.False(result);
        Assert.Equal("Введите фамилию", error);
    }
    [Fact]
    public void ValidateRegistrationFields_ShouldFail_WhenNameEmpty()
    {
        var result = Validation.ValidateRegistrationFields(
            "Иванов", "фамилия",
            "", "имя",
            "user123", "логин",
            "pass123", "пароль",
            "pass123", "подтверждение",
            out string error
        );

        Assert.False(result);
        Assert.Equal("Введите имя", error);
    }
    [Fact]
    public void ValidateRegistrationFields_ShouldFail_WhenLoginInvalid()
    {
        var result = Validation.ValidateRegistrationFields(
            "Иванов", "фамилия",
            "Иван", "имя",
            "user@", "логин",
            "pass123", "пароль",
            "pass123", "подтверждение",
            out string error
        );

        Assert.False(result);
        Assert.Equal("Логин может содержать только английские буквы, цифры и подчеркивание", error);
    }
    [Fact]
    public void ValidateRegistrationFields_ShouldFail_WhenConfirmPasswordEmpty()
    {
        var result = Validation.ValidateRegistrationFields(
            "Иванов", "фамилия",
            "Иван", "имя",
            "user123", "логин",
            "pass123", "пароль",
            "", "подтверждение",
            out string error
        );

        Assert.False(result);
        Assert.Equal("Подтвердите пароль", error);
    }
}