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
        /// Получить один общий контекст базы данных 
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
                    _context.ChangeTracker.QueryTrackingBehavior = Microsoft.EntityFrameworkCore.QueryTrackingBehavior.TrackAll;
                }

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
        /// Увеличить счетчик ссылок 
        /// </summary>
        public static void AddReference()
        {
            Interlocked.Increment(ref _referenceCount);
        }

        /// <summary>
        /// Уменьшить счетчик ссылок 
        /// </summary>
        public static void ReleaseReference()
        {
            int newCount = Interlocked.Decrement(ref _referenceCount);
        }

        /// <summary>
        /// Принудительное освобождение контекста 
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