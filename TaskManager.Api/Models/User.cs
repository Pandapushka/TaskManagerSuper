﻿using System.Numerics;
using TaskManager.Common.Models;

namespace TaskManager.Api.Models
{
    public class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Phone { get; set; }
        public DateTime RegistrationDate { get; set; }
        public DateTime LastLoginDate { get; set;}
        public string Photo { get; set;}
        public List<Project> Projects { get; set; } = new List<Project>();
        public List<Desk> Desks { get; set; } = new List<Desk>();
        public List<TaskModel> Tasks { get; set; } = new List<TaskModel>();
        public UserStatus Status { get; set; }
        public User()
        {
                
        }
        public User(string fname, string lname, string email, string password,
                    UserStatus userStatus = UserStatus.User, string phone = null, string photo = null)
        {
            FirstName = fname;
            LastName = lname;
            Email = email;
            Password = password;
            Phone = phone;
            Photo = photo;
            RegistrationDate = DateTime.Now;
            Status = userStatus;
        }

        public UserModel ToDto() 
        {
            return new UserModel()
            {
                Id = this.Id,
                FirstName = this.FirstName,
                LastName = this.LastName,
                Email = this.Email,
                Password = this.Password,
                Phone = this.Phone,
                //Photo = this.Photo,
                RegistrationDate = this.RegistrationDate,
                Status = this.Status

            };
        }
    }
}
