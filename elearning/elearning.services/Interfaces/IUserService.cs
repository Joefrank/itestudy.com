
using elearning.model.DataModels;
using elearning.model.ViewModels;
using System;
using System.Collections;
using System.Collections.Generic;

namespace elearning.services.Interfaces
{
    public interface IUserService
    {
        User CreateUser(RegisterVm model);

        User GetUserById(Guid userId);

        User GetUserById(int userId);

        User GetUserByEmail(string email);

        User GetUserByActivationCode(string code);

        bool ActivateUser(Guid userId);

        User ValidateUser(LoginVm model);

        User ValidateUser(string email, string password);

        User SaveUserDetails(UserDetailsVm details);

        List<User> GetAllUsers();
    }
}
