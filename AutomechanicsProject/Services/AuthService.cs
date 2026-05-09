using AutomechanicsProject.Classes;
using AutomechanicsProject.Helpers;
using AutomechanicsProject.Properties;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using AutomechanicsProject.Services.Interfaces;

namespace AutomechanicsProject.Services
{
    public class AuthService : IAuthService
    {
        private readonly DateBase _db;

        public AuthService(DateBase db)
        {
            _db = db;
        }

        public Users Login(string login, string password)
        {
            var user = _db.Users
                .Include(u => u.Role)
                .FirstOrDefault(u => u.Login == login);

            if (user == null)
                return null;

            bool isValid;

            if (user.Password.StartsWith("$2"))
            {
                isValid = PasswordHelper.VerifyPassword(password, user.Password);
            }
            else
            {
                isValid = password == user.Password;

                if (isValid)
                {
                    user.Password = PasswordHelper.HashPassword(password);
                    _db.SaveChanges();
                }
            }

            return isValid ? user : null;
        }

        public void Register(string surname, string name, string lastname, string login, string password)
        {
            if (_db.Users.Any(u => u.Login == login))
                throw new Exception("Пользователь с таким логином уже существует");

            var role = _db.Roles.FirstOrDefault(r => r.Position == Resources.StorekeeperRoleName);

            if (role == null)
                throw new Exception("Роль кладовщика не найдена");

            var user = new Users
            {
                Id = Guid.NewGuid(),
                Surname = surname.Trim(),
                Name = name.Trim(),
                Lastname = lastname.Trim(),
                Login = login.Trim(),
                Password = PasswordHelper.HashPassword(password),
                RoleId = role.Id
            };

            _db.Users.Add(user);
            _db.SaveChanges();
        }
    }
}