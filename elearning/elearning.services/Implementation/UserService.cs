using AutoMapper;
using CMCommon.Logging.Interfaces;
using CMCommon.Security.Interfaces;
using elearning.data;
using elearning.model.DataModels;
using elearning.model.Enums;
using elearning.model.ViewModels;
using elearning.services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace elearning.services.Implementation
{
    public class UserService : IUserService
    {
        private ILoggerService _logger;
        private IEncryptionService _encryptionService;

        public UserService(ILoggerService logger, IEncryptionService encryptionService)
        {
            _logger = logger;
            _encryptionService = encryptionService;
        }

        public virtual User CreateUser(RegisterVm model)
        {
            try
            {
                _logger.LogItem("Creating user");
                var user = Mapper.Map<RegisterVm, User>(model);

                //set some user default values
                user.DateRegistered = DateTime.Now;
                user.Active = false;
                user.ReceiveNewsletter = false;
                user.Type = UserType.Guest.ToString();
                user.ActivationLink = model.ActivationLink; //use security dll and generate random 10 numbers
                user.Roles = UserRole.Guest;

                //persist user
                using (var context = new DataDbContext())
                {
                    context.Users.Add(user);
                    context.SaveChanges();
                }

                return user;
            }
            catch (Exception ex)
            {
                _logger.LogItem(ex.Message);
                return null;
            }
        }

        public User GetUserById(Guid userId)
        {
            try
            {
                User user = null;

                using (var context = new DataDbContext())
                {
                    user= context.Users.FirstOrDefault(x => x.Id == userId);                    
                }

                return user;
            }
            catch(Exception ex)
            {
                _logger.LogItem("GetUserById() failed " + ex.Message);
                return null;
            }
        }

        public User GetUserById(int userId)
        {
            try
            {
                User user = null;

                using (var context = new DataDbContext())
                {
                    user = context.Users.FirstOrDefault(x => x.Identity == userId);
                }

                return user;
            }
            catch (Exception ex)
            {
                _logger.LogItem("GetUserById() failed id " + userId + System.Environment.NewLine + ex.Message);
                return null;
            }
        }

        public User GetUserByEmail(string email)
        {
            try
            {
                User user = null;

                using (var context = new DataDbContext())
                {
                    user = context.Users.FirstOrDefault(x => x.Email.Equals(email, StringComparison.CurrentCultureIgnoreCase));
                }

                return user;
            }
            catch (Exception ex)
            {
                _logger.LogItem("GetUserByEmail() failed " + ex.Message);
                return null;
            }
        }

        public User GetUserByActivationCode(string code)
        {
            try
            {
                User user = null;

                using (var context = new DataDbContext())
                {
                    user = context.Users.FirstOrDefault(x => x.ActivationLink.Equals(code));
                }

                return user;
            }
            catch (Exception ex)
            {
                _logger.LogItem("GetUserByActivationCode() code: " + code + " failed " + ex.Message);
                return null;
            }
        }

        public bool ActivateUser(Guid userId)
        {
            try
            {               
                using (var context = new DataDbContext())
                {
                    var user = context.Users.FirstOrDefault(x => x.Id == userId);
                    user.Active = true;
                    user.DateActivated = DateTime.Now;
                    context.SaveChanges();
                }

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogItem("ActivateUser() failed " + ex.Message);
                return false;
            }
        }

        public User ValidateUser(LoginVm model)
        {
            return ValidateUser(model.Email, model.Password);
        }

        public User ValidateUser(string email, string password)
        {
            try
            {
                User user;
                
                using (var context = new DataDbContext())
                {
                    var encryptedPassword = _encryptionService.Encrypt(password);

                    user = context.Users.FirstOrDefault(x => 
                            x.Email.Equals(email, StringComparison.CurrentCultureIgnoreCase)
                            && x.Password.Equals(encryptedPassword));
                }

                return user;
            }
            catch (Exception ex)
            {
                _logger.LogItem(" ValidateUser() " + ex.Message);
                return null;
            }
        }

        public User SaveUserDetails(UserDetailsVm details)
        {
            try
            {
                _logger.LogItem("updating user details");
                User user = null;

                //persist user
                using (var context = new DataDbContext())
                {
                    user = context.Users.FirstOrDefault(x => x.Identity == details.UserIdentity);
                    if (user != null)
                    {
                        user.Title = details.Title;
                        user.Firstname = details.Firstname;
                        user.Lastname = details.Lastname;
                        user.ReceiveNewsletter = details.ReceiveNewsletter;
                        user.LastModified = DateTime.Now;
                        user.LastModifiedBy = details.UserIdentity;

                        //if email has changed.notify admin
                        if (!user.Email.Equals(details.Email, StringComparison.CurrentCultureIgnoreCase))
                        {
                            var existingUser = GetUserByEmail(details.Email);
                            if (existingUser != null) //trouble
                                throw new Exception("duplicate user email attempt in edit details for emails new: " + details.Email + " and old: " + user.Email);
                            user.Email = details.Email;
                        }
                        //check that password is not null and update
                        if (!string.IsNullOrEmpty(details.Password))
                        {
                            user.Password = _encryptionService.Encrypt(details.Password);
                        }

                        context.SaveChanges();
                    }
                }

                return user;
            }
            catch (Exception ex)
            {
                _logger.LogItem(ex.Message);
                return null;
            }
        }

        public List<User> GetAllUsers()
        {
            try
            {
                List<User> users;

                using (var context = new DataDbContext())
                {
                    users = context.Users.ToList();
                }

                return users;
            }
            catch (Exception ex)
            {
                _logger.LogItem(" Failed to GetAllUsers() " + ex.Message);
                return null;
            }
        }
    }
}
