﻿using Zurvan.Core.Interfaces;

namespace Zurvan.Core.UserFactory.UserTypes
{
    internal class Lead : ILead
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }    
        public string Email { get; set; }
        public int UserId { get; set; }
        public UserType Type { get; set; }
        public List<IProject> Projects { get; set; }
        public List<IUser> TeamMembers { get; set; }
    }
}