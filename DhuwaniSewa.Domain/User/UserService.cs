using DhuwaniSewa.Database.Repository;
using DhuwaniSewa.Model.DbEntities;
using DhuwaniSewa.Model.Enum;
using DhuwaniSewa.Model.ViewModel;
using DhuwaniSewa.Utils.CustomException;
using Microsoft.AspNetCore.Identity;
using System;
using System.Threading.Tasks;

namespace DhuwaniSewa.Domain
{
    public class UserService : IUserService
    {
        private readonly IRepositoryService<AppUsers, int> _userRepository;
        private readonly UserManager<ApplicationUsers> _userManager;
        private readonly IUnitOfWork _unitOfWork;
        public UserService(IRepositoryService<AppUsers, int> userRepository,
            IUnitOfWork unitOfWork,
            UserManager<ApplicationUsers> userManager
            )
        {
            this._userRepository = userRepository;
            this._unitOfWork = unitOfWork;
            this._userManager = userManager;
        }
        public async Task<RegisterUserViewModel> Register(RegisterUserViewModel model)
        {
            try
            {
                ApplicationUsers applicationUser = new ApplicationUsers();
                applicationUser.Email = model.Email;
                applicationUser.UserName = model.UserName;
                applicationUser.PhoneNumber = model.MobileNumber;

                var existingUser =await _userManager.FindByNameAsync(model.UserName);
                if (existingUser != null)
                    applicationUser = existingUser;
                else
                {
                    /// creating a new user
                    var newAddedUser =await _userManager.CreateAsync(applicationUser, model.Password);
                    if (newAddedUser.Succeeded)
                        applicationUser =await _userManager.FindByNameAsync(model.UserName);

                    /// assigning a role for service seeker and service provider
                    if (model.IsServiceProvider && !model.IsEmployee)
                        await _userManager.AddToRoleAsync(applicationUser, nameof(UserRole.ServiceProvider));
                    else if (!model.IsServiceProvider && !model.IsEmployee)
                        await _userManager.AddToRoleAsync(applicationUser, nameof(UserRole.ServiceSeeker));
                }
               
                if (await _userRepository.GetAync(a => a.UserId == applicationUser.Id) != null)
                    throw new CustomException($"{model.UserName} as username already exist.");

                AppUsers appUsers = new AppUsers();
                appUsers.UserId = applicationUser.Id;
                appUsers.IsCompnay = model.IsCompany;
                appUsers.IsEmployee = model.IsEmployee;
                appUsers.IsServiceProvider = model.IsServiceProvider;

                await _userRepository.AddAsync(appUsers);
                await _unitOfWork.CommitAsync();
                
                model.Id = appUsers.Id;
                return model;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
