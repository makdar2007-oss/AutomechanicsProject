using System;
using System.Collections.Generic;
using AutomechanicsProject.Classes;

public class FakeDb
{
    public List<Users> Users { get; set; } = new List<Users>();
    public List<Role> Roles { get; set; } = new List<Role>();
}