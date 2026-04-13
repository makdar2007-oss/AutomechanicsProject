using System;

namespace AutomechanicsProject.Classes
{
    public static class CurrentUser
    {
        public static Guid Id { get; set; }
        public static string Username { get; set; }
        public static string Role { get; set; }
        public static bool IsAuthenticated { get; set; } = false;

        public static void Clear()
        {
            Id = Guid.Empty;
            Username = null;
            Role = null;
            IsAuthenticated = false;
        }
    }
}