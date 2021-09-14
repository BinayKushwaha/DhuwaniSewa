using DhuwaniSewa.Database.Repository;
using DhuwaniSewa.Model.Constant;
using DhuwaniSewa.Model.DbEntities;
using DhuwaniSewa.Model.Enum;
using DhuwaniSewa.Model.ViewModel;
using DhuwaniSewa.Utils;
using DhuwaniSewa.Utils.CustomException;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace DhuwaniSewa.Domain
{
    public class UserService : IUserService
    {
        private readonly IRepositoryService<AppUsers, int> _appUserRepository;
        private readonly UserManager<ApplicationUsers> _userManager;
        private readonly IPersonDetailService _personDetailService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepositoryService<UserPersonDetail, int> _userPersonRepo;
        private readonly IServiceProviderService _serviceProviderService;
        private readonly IRepositoryService<ApplicationUsers, int> _userRepo;
        private readonly IOtpService _otpService;
        private readonly IRepositoryService<PersonalDetail, int> _personDetailRepo;

        public UserService(IRepositoryService<AppUsers, int> appUserRepository,
            IUnitOfWork unitOfWork,
            UserManager<ApplicationUsers> userManager,
            IPersonDetailService personDetailService,
            IRepositoryService<UserPersonDetail, int> userPersonRepo,
            IServiceProviderService serviceProviderService,
            IRepositoryService<ApplicationUsers, int> userRepo,
            IOtpService otpService,
            IRepositoryService<PersonalDetail, int> personDetailRepo
            )
        {
            this._appUserRepository = appUserRepository;
            this._unitOfWork = unitOfWork;
            this._userManager = userManager;
            this._personDetailService = personDetailService;
            this._userPersonRepo = userPersonRepo;
            this._serviceProviderService = serviceProviderService;
            this._userRepo = userRepo;
            this._otpService = otpService;
            this._personDetailRepo = personDetailRepo;
        }
        public async Task<RegistrationResponseModel> RegisterAsync(RegisterUserViewModel model)
        {
            using (var transaction = await _unitOfWork.BeginTransaction(System.Data.IsolationLevel.ReadCommitted))
            {
                try
                {
                    ApplicationUsers applicationUser = new ApplicationUsers();
                    int personId = 0;
                    int serviceProviderId = 0;

                    if (string.IsNullOrWhiteSpace(model.UserName) || string.IsNullOrEmpty(model.UserName))
                        throw new CustomException("Invalid username. Please try again.");

                    if (CustomValidator.IsEmail(model.UserName))
                        model.Email = model.UserName;
                    else if (CustomValidator.IsMobileNumber(model.UserName))
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

                    if (await _appUserRepository.GetAync(a => a.UserId == applicationUser.Id) != null)
                        throw new CustomException("User already exist.");

                    AppUsers appUsers = new AppUsers();
                    appUsers.IsCompnay = model.IsCompany;
                    appUsers.IsEmployee = model.IsEmployee;
                    appUsers.IsServiceProvider = model.IsServiceProvider;
                    appUsers.Active = true;
                    appUsers.UserId = applicationUser.Id;

                    await _appUserRepository.AddAsync(appUsers);
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
                    personId = await _personDetailService.Save(personModel);

                    var userPersonModel = new UserPersonViewModel()
                    {
                        UserId = appUsers.Id,
                        PersonId = personId
                    };
                    await SaveUserPersonAsync(userPersonModel);

                    /// Create Serviceprovider or Service Seeker
                    if (model.IsServiceProvider && !model.IsEmployee)
                    {
                        var serviceProviderModel = new ServiceProviderViewModel();
                        serviceProviderModel.Active = true;
                        serviceProviderModel.IsCompany = model.IsCompany;
                        serviceProviderModel.UserId = appUsers.Id;
                        serviceProviderId = await _serviceProviderService.SaveAsync(serviceProviderModel);
                    }

                    ///Generate Otp and send for verification
                    var otpRequestModel = new OtpViewModel();
                    otpRequestModel.UserName = model.UserName;
                    otpRequestModel.MailSubject = MessageTemplate.Registration_OTP_Mail_Subject;
                    otpRequestModel.MailBody = MessageTemplate.Registration_OTP_Mail_Body;
                    await _otpService.GenerateSendRegistrationOtpAsync(otpRequestModel);

                    await transaction.CommitAsync();

                    return new RegistrationResponseModel()
                    {
                        UserId = appUsers.Id,
                        ServiceProviderId = serviceProviderId,
                        ServiceSeekerId = serviceProviderId,
                    };
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }
        public async Task<UserPersonViewModel> SaveUserPersonAsync(UserPersonViewModel request)
        {
            var userPerson = new UserPersonDetail();
            userPerson.UserId = request.UserId;
            userPerson.PersonId = request.PersonId;
            await _userPersonRepo.AddAsync(userPerson);
            await _unitOfWork.CommitAsync();
            return request;
        }

        public async Task<UserViewModel> GetAsync(string userId)
        {
            try
            {
                var user =await _userRepo.GetQueryable().Include(a => a.AppUsers).ThenInclude(a => a.PersonalDetail).FirstOrDefaultAsync(u => u.Id == userId && u.IsActive);
                if (user == null)
                    throw new ArgumentNullException("User not found.");

                var userResultModel = new UserViewModel();
                userResultModel.UserId = user.Id;
                userResultModel.AppUserId = user.AppUsers.Id;
                userResultModel.Active = user.IsActive;
                userResultModel.IsServiceProvider = user.AppUsers.IsServiceProvider;
                userResultModel.UserName = user.UserName;

                int personId = user.AppUsers.PersonalDetail.FirstOrDefault().PersonId;
                userResultModel.PersonId = personId;
                var person = await _personDetailRepo.GetByIdAsync(personId);

                userResultModel.FullName = $"{person.FirstName} {person.LastName}";
                return userResultModel;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public async Task<UserProfileViewModel> GetProfileAsync(int userId)
        {
            try
            {
                var userDetail = await _appUserRepository.GetQueryable().Include(a => a.PersonalDetail).FirstOrDefaultAsync(a => a.Id == userId);
                if (userDetail == null)
                    throw new ArgumentNullException("User not found.");
                int personId = userDetail.PersonalDetail.FirstOrDefault().PersonId;
                var personInfo = await _personDetailRepo.GetQueryable().Include(a => a.PersonalDetailContactDetails).ThenInclude(b => b.ContactDetail).
                    FirstOrDefaultAsync(p => p.Id == personId);
                if (personInfo == null)
                    throw new ArgumentNullException("Person not found.");
                var userProfileData = new UserProfileViewModel();
                userProfileData.AppUserId = userDetail.Id;
                userProfileData.FirstName = personInfo.FirstName;
                userProfileData.LastName = personInfo.LastName;
                var contactdetails = personInfo.PersonalDetailContactDetails.FirstOrDefault().ContactDetail;
                userProfileData.Email = contactdetails.Email;
                userProfileData.EmailConfirmed = contactdetails.EmailConfirmed;
                userProfileData.MobileNumber = contactdetails.ContactNumber;
                userProfileData.MobileNumberConfirmed = contactdetails.MobileNumberConfirmed;
                var documentDetail = personInfo.PersonalDetailDocumentDetails?.FirstOrDefault(d => d.DocumentDetail.Type == nameof(DocumentType.Ctitzenship))?.DocumentDetail;
                if(documentDetail!=null)
                    userProfileData.CitizenshipNumber = documentDetail.RegistrationNumber;
                return userProfileData;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

    }
}
