using AutomechanicsProject.Classes;
using System;
using System.Linq;
using System.Windows.Forms;

namespace AutomechanicsProject.Helpers
{
    public static class HashExistingPasswords
    {
        public static void HashAllPasswords()
        {
            try
            {
                using (var db = new DateBase())
                {
                    var users = db.Users.ToList();
                    int updatedCount = 0;

                    foreach (var user in users)
                    {
                        if (!user.Password.StartsWith("$2a$") && !user.Password.StartsWith("$2b$"))
                        {
                            string hashedPassword = PasswordHelper.HashPassword(user.Password);
                            user.Password = hashedPassword;
                            updatedCount++;
                        }
                    }

                    if (updatedCount > 0)
                    {
                        db.SaveChanges();
                        MessageBox.Show($"Успешно захэшировано {updatedCount} паролей!",
                            "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Все пароли уже захэшированы!",
                            "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при хешировании паролей: {ex.Message}",
                    "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}