using BCrypt.Net;

/// <summary>
/// Вспомогательный класс для работы с паролями
/// Предоставляет методы для хеширования паролей и их проверки с использованием алгоритма BCrypt
/// </summary>
public static class PasswordHelper
{
    /// <summary>
    /// Хеширует пароль с использованием алгоритма BCrypt
    /// </summary>
    public static string HashPassword(string password)
    {
        return BCrypt.Net.BCrypt.HashPassword(password);
    }
    /// <summary>
    /// Проверяет соответствие пароля его хешированной версии
    public static bool VerifyPassword(string password, string hashedPassword)
    {
        return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
    }
}