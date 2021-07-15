using AutoMapper;
using DhuwaniSewa.Database.Repository;
using DhuwaniSewa.Model.DbEntities;
using DhuwaniSewa.Model.Enum;
using DhuwaniSewa.Model.ViewModel;
using DhuwaniSewa.Utils.CustomException;
using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DhuwaniSewa.Domain
{
    public class UserService : IUserService
    {
        private readonly IRepositoryService<AppUsers, int> _userRepository;
        private readonly UserManager<ApplicationUsers> _userManager;
        private readonly IPersonDetailService _personDetailService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepositoryService<UserPersonDetail, int> _userPersonRepo;
        public UserService(IRepositoryService<AppUsers, int> userRepository,
            IUnitOfWork unitOfWork,
            UserManager<ApplicationUsers> userManager,
            IPersonDetailService personDetailService,
            IRepositoryService<UserPersonDetail, int> userPersonRepo
            )
        {
            this._userRepository = userRepository;
            this._unitOfWork = unitOfWork;
            this._userManager = userManager;
            this._personDetailService = personDetailService;
            this._userPersonRepo = userPersonRepo;
        }
        public async Task<RegisterUserViewModel> Register(RegisterUserViewModel model)
        {
            using (var transaction = await _unitOfWork.BeginTransaction(System.Data.IsolationLevel.ReadCommitted))
            {
                try
                {
                    ApplicationUsers applicationUser = new ApplicationUsers();

                    if (string.IsNullOrWhiteSpace(model.UserName) || string.IsNullOrEmpty(model.UserName))
                        throw new CustomException("Invalid username. Please try again.");

                    if (IsEmail(model.UserName))
                        model.Email = model.UserName;
                    else if (IsMobileNumber(model.UserName))
                        model.MobileNumber = model.UserName;
                    else
                        throw new CustomException("Invalid username. Please try again.");

                    applicationUser.Email = model.Email;
                    applicationUser.UserName = model.UserName;
                    applicationUser.PhoneNumber = model.MobileNumber;

                    var existingUser = await _userManager.FindByNameAsync(model.UserName);
                    if (existingUser != null)
                        applicationUser = existingUser;
                    else
                    {
                        /// creating a new user
                        var newAddedUser = await _userManager.CreateAsync(applicationUser, model.Password);
                        if (newAddedUser.Succeeded)
                        {
                            applicationUser = await _userManager.FindByNameAsync(model.UserName);

                            /// assigning a role for service seeker and service provider
                            if (model.IsServiceProvider && !model.IsEmployee)
                                await _userManager.AddToRoleAsync(applicationUser, nameof(UserRole.ServiceProvider));
                            else if (!model.IsServiceProvider && !model.IsEmployee)
                                await _userManager.AddToRoleAsync(applicationUser, nameof(UserRole.ServiceSeeker));
                        }
                        else
                        {
                            var errors = newAddedUser.Errors;
                            if (errors.Any(a => a.Code == "PasswordRequiresLower"))
                                throw new CustomException("Passwords must have at least one lowercase ('a'-'z').");

                        }

                    }

                    if (await _userRepository.GetAync(a => a.UserId == applicationUser.Id) != null)
                        throw new CustomException($"{model.UserName} as username already exist.");

                    AppUsers appUsers = new AppUsers();
                    appUsers.IsCompnay = model.IsCompany;
                    appUsers.IsEmployee = model.IsEmployee;
                    appUsers.IsServiceProvider = model.IsServiceProvider;

                    appUsers.UserId = applicationUser.Id;

                    await _userRepository.AddAsync(appUsers);
                    await _unitOfWork.CommitAsync();

                    var personModel = new PersonDetailViewmodel();
                    personModel.UserId = appUsers.Id;
                    personModel.FirstName = model.FirstName;
                    personModel.LastName = model.LastName;
                    personModel.ContactDetails.Add(new ContactDetailViewModel()
                    {
                        Number = model.MobileNumber,
                        Email = model.Email
                    });
                    var personId= await _personDetailService.Save(personModel);
                    
                    var userPersonModel = new UserPersonViewModel()
                    {
                        UserId = appUsers.Id,
                        PersonId = personId
                    };
                    await SaveUserPerson(userPersonModel);

                    model.Id = appUsers.Id;
                    await transaction.CommitAsync();
                    return model;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }
        public async Task<UserPersonViewModel> SaveUserPerson(UserPersonViewModel request)
        {
            var userPerson = new UserPersonDetail();
            userPerson.UserId = request.UserId;
            userPerson.PersonId = request.PersonId;
            await _userPersonRepo.AddAsync(userPerson);
            await _unitOfWork.CommitAsync();
            return request;
        }
        #region Helper
        private bool IsEmail(string email)
        {
            return Regex.IsMatch(email,
                    @"^[^@\s]+@[^@\s]+\.[^@\s]+$",
                    RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
        }
        private bool IsMobileNumber(string mobileNumber)
        {
            Regex reg = new Regex(@"^[0-9]{10}$");
            return reg.IsMatch(mobileNumber);
        }
        #endregion
    }
}
