using System;
using System.Threading;

namespace AutomechanicsProject.Classes
{
    /// <summary>
    /// Менеджер контекста базы данных для всего приложения
    /// Предотвращает создание множественных подключений к БД
    /// </summary>
    public static class DbContextManager
    {
        private static DateBase _context;
        private static readonly object _lockObject = new object();
        private static int _referenceCount = 0;
        private static bool _isDisposing = false;

        /// <summary>
        /// Получить общий контекст базы данных (один на всё приложение)
        /// </summary>
        public static DateBase GetContext()
        {
            lock (_lockObject)
            {
                if (_isDisposing)
                {
                    _isDisposing = false;
                    _context = null;
                }

                if (_context == null)
                {
                    _context = new DateBase();
                    _context.Database.EnsureCreated();
                    // Отключаем автоматическое отслеживание для повышения производительности
                    _context.ChangeTracker.QueryTrackingBehavior = Microsoft.EntityFrameworkCore.QueryTrackingBehavior.TrackAll;
                }

                // Проверяем, жив ли контекст
                try
                {
                    if (_context.Database.CanConnect() == false)
                    {
                        _context.Dispose();
                        _context = new DateBase();
                        _context.Database.EnsureCreated();
                    }
                }
                catch
                {
                    _context = new DateBase();
                    _context.Database.EnsureCreated();
                }

                return _context;
            }
        }

        /// <summary>
        /// Увеличить счетчик ссылок (вызывать при открытии формы)
        /// </summary>
        public static void AddReference()
        {
            Interlocked.Increment(ref _referenceCount);
        }

        /// <summary>
        /// Уменьшить счетчик ссылок (вызывать при закрытии формы)
        /// </summary>
        public static void ReleaseReference()
        {
            int newCount = Interlocked.Decrement(ref _referenceCount);

            // НЕ УНИЧТОЖАЕМ КОНТЕКСТ!
            // Контекст живёт всё приложение
            // Только при ForceDispose он уничтожается
        }

        /// <summary>
        /// Принудительное освобождение контекста (при закрытии приложения)
        /// </summary>
        public static void ForceDispose()
        {
            lock (_lockObject)
            {
                _isDisposing = true;
                if (_context != null)
                {
                    try
                    {
                        _context.SaveChanges();
                        _context.Dispose();
                    }
                    catch { }
                    finally
                    {
                        _context = null;
                    }
                }
                _referenceCount = 0;
            }
        }

        /// <summary>
        /// Сохранить все изменения в БД
        /// </summary>
        public static void SaveChanges()
        {
            if (_context != null)
            {
                try
                {
                    _context.SaveChanges();
                }
                catch (Exception ex)
                {
                    Program.LogError("Ошибка при сохранении изменений", ex);
                    throw;
                }
            }
        }
    }
}