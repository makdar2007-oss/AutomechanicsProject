using AutomechanicsProject.Classes.AutomechanicsProject.Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace AutomechanicsProject.Classes
{
    public class User
    {
        public Guid Id { get; set; }             
        public string Surname { get; set; }     
        public string Name { get; set; }        
        public string Lastname { get; set; }        
        public string Login { get; set; }      
        public string Password { get; set; }      
        public Guid RoleId { get; set; }

        public Role Role { get; set; }

        [NotMapped]
        public string FullName => $"{Surname} {Name} {Lastname}".Trim();

        [NotMapped]
        public string RoleName => Role?.Position ?? "Не назначена";
    }
}
