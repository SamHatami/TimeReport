﻿namespace Zurvan.Core.Interfaces
{
    public interface IDataBaseService
    {
        List<IUser> GetAllUsers();

        List<IProject> GetAllProjects();

        IUser GetUser(int id);

        IProject GetProject(int id);

        List<IProject> GetUserProjects(int userID);

        bool Login(string name, string password);
    }
}