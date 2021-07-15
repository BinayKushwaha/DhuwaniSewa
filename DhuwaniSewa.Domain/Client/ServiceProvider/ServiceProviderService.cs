using AutoMapper;
using DhuwaniSewa.Database.Repository;
using DhuwaniSewa.Model.Constant;
using DhuwaniSewa.Model.DbEntities;
using DhuwaniSewa.Model.Enum;
using DhuwaniSewa.Model.ViewModel;
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DhuwaniSewa.Domain
{
    public class ServiceProviderService : IServiceProviderService
    {
        private readonly IRepositoryService<ServiceProvider, int> _serviceProviderRepo;
        private readonly IServiceProviderMapper _mapper;
        private readonly IFiscalYearService _fiscalYearService;
        private readonly ISerialNumberSevice _serialNumberSevice;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPersonDetailService _personDetailService;
        private readonly IRepositoryService<PersonalDetailDocumentDetail, int> _personDocsRepo;
        private readonly IRepositoryService<PersonalDetailContactDetail, int> _personContactRepo;
        private readonly IRepositoryService<ServiceProviderVehicleDetail, int> _serviceProviderVehicleRepo;
        private readonly IRepositoryService<ServiceProviderContactPerson, int> _serviceProviderContactPersonRepo;
        private readonly IRepositoryService<PersonalDetail, int> _persondetailRepo;
        private readonly IRepositoryService<ContactDetail, int> _contactDetailRepo;
        private readonly IUserService _userService;
        private readonly IRepositoryService<DocumentDetail, int> _documentRepo;
        public ServiceProviderService(
            IPersonDetailService personDetailService,
            IRepositoryService<ServiceProvider, int> serviceProviderRepo,
            IServiceProviderMapper mapper,
            IFiscalYearService fiscalYearService,
            ISerialNumberSevice serialNumberSevice,
            IUnitOfWork unitOfWork,
            IRepositoryService<PersonalDetailDocumentDetail, int> personDocsRepo,
            IRepositoryService<PersonalDetailContactDetail, int> personContactRepo,
            IRepositoryService<ServiceProviderVehicleDetail, int> serviceProviderVehicleRepo,
            IRepositoryService<ServiceProviderContactPerson, int> serviceProviderContactPersonRepo,
            IRepositoryService<PersonalDetail, int> persondetailRepo,
            IRepositoryService<ContactDetail, int> contactDetailRepo,
            IUserService userService,
            IRepositoryService<DocumentDetail, int> documentRepo
            )
        {
            this._serviceProviderRepo = serviceProviderRepo;
            this._mapper = mapper;
            this._serialNumberSevice = serialNumberSevice;
            this._fiscalYearService = fiscalYearService;
            this._unitOfWork = unitOfWork;
            this._personDetailService = personDetailService;
            this._personDocsRepo = personDocsRepo;
            this._personContactRepo = personContactRepo;
            this._serviceProviderVehicleRepo = serviceProviderVehicleRepo;
            this._persondetailRepo = persondetailRepo;
            this._contactDetailRepo = contactDetailRepo;
            this._serviceProviderContactPersonRepo = serviceProviderContactPersonRepo;
            this._userService = userService;
        }
        public async Task<int> Save(ServiceProviderViewModel request)
        {
            using (var transaction = await _unitOfWork.BeginTransaction(System.Data.IsolationLevel.ReadCommitted))
            {
                try
                {
                    var serviceProvider = _mapper.MapToEntity(request);
                    await _serviceProviderRepo.AddAsync(serviceProvider);
                    await _unitOfWork.CommitAsync();

                    await transaction.CommitAsync();
                    return serviceProvider.Id;
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    throw;
                }
            }
        }
        public IList<ServiceProviderViewModel> GetAll()
        {
            try
            {
                var serviceProviders = _serviceProviderRepo.GetQueryable().Include(a => a.AppUser).
                    ThenInclude(a => a.PersonalDetail).
                    Where(b => b.Active).ToList();

                var personIds = serviceProviders.SelectMany(a => a.AppUser.PersonalDetail.Select(s => s.PersonalDetail.Id)).ToList();
                var serviceProvidersIds = serviceProviders.Select(a => a.Id).ToList();
                var companyIds = serviceProviders.SelectMany(a => a.AppUser.CompanyDetail.Select(s => s.Id)).ToList();

                var personDocuments = _personDocsRepo.GetQueryable().Include(a => a.DocumentDetail).
                    Where(a => personIds.Contains(a.PersonDetailId)).ToList();
                var personContacts = _personContactRepo.GetQueryable().Include(a => a.ContactDetail).
                    Where(a => personIds.Contains(a.PersonalDetailId)).ToList();

                var serviceProviderVehicles = _serviceProviderVehicleRepo.GetQueryable().Include(a => a.VehicleDetail).
                    Where(a => serviceProvidersIds.Contains(a.ServiceProviderId)).ToList();

                var result = new List<ServiceProviderViewModel>();
                foreach (var serviceProvider in serviceProviders)
                {
                    var model = new ServiceProviderViewModel();
                    model.IsCompany = serviceProvider.AppUser.IsCompnay;
                    model.ServiceProviderId = serviceProvider.Id;
                    model.Active = serviceProvider.Active;
                    model.DhuwaniSewaId = serviceProvider.DhuwaniSewaId;
                    model.DetailsCorrectAggreed = serviceProvider.DetailsCorrectAgreed;
                    model.UserId = serviceProvider.UserId;

                    if (model.IsCompany)
                        model.CompanyDetail = new CompanyDetailViewModel()
                        {
                            Name = serviceProvider.AppUser.CompanyDetail.FirstOrDefault().Name
                        };
                    else
                    {
                        var personId = serviceProvider.AppUser.PersonalDetail.FirstOrDefault().PersonalDetail.Id;
                        var personDetail = serviceProvider.AppUser.PersonalDetail.FirstOrDefault();
                        var contactDetails = personContacts.Where(a => a.PersonalDetailId == personId).
                            Select(a => a.ContactDetail).ToList();
                        var documentDetails = personDocuments.Where(a => a.PersonDetailId == personId).
                            Select(a => a.DocumentDetail).ToList();
                        var spVehicles = serviceProviderVehicles.Where(a => a.ServiceProviderId == serviceProvider.Id).
                            Select(a => a.VehicleDetail);

                        model.PersonDetail = new PersonDetailViewmodel()
                        {
                            PersondetailId = personDetail.PersonalDetail.Id,
                            FirstName = personDetail.PersonalDetail.FirstName,
                            MiddleName = personDetail.PersonalDetail.MiddleName,
                            LastName = personDetail.PersonalDetail.LastName,
                            UserId = personDetail.UserId
                        };

                        foreach (var contact in contactDetails)
                        {
                            model.PersonDetail.ContactDetails.Add(new ContactDetailViewModel()
                            {
                                ContactDetailId = contact.Id,
                                Email = contact.Email,
                                Number = contact.ContactNumber
                            });
                        }

                        foreach (var document in documentDetails)
                        {
                            model.PersonDetail.Documents.Add(new DocumentDetailViewModel()
                            {
                                DocumentDetailId = document.Id,
                                Type = document.Type,
                                RegistrationNumber = document.RegistrationNumber,
                                IssuedDistrict = document.IssuedDistrict
                            });
                        }
                        foreach (var vehicle in spVehicles)
                        {
                            model.VehicleDetails.Add(new VehicleDetailViewModel()
                            {
                                VehicleDetailId = vehicle.Id,
                                TypeId = vehicle.TypeId,
                                RegistrationNumber = vehicle.RegistrationNumber,
                                WheelType = vehicle.WheelType,
                                MaxWeight = vehicle.MaxWeight,
                                WeightUnit = vehicle.WeightUnit,
                                BrandId = vehicle.BrandId,
                                Model = vehicle.Model
                            });
                        }
                    }
                    result.Add(model);
                }
                return result;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public async Task<ServiceProviderViewModel> Get(int Id)
        {
            try
            {
                var result = await _serviceProviderRepo.GetQueryable().Include(a => a.AppUser.PersonalDetail).
                    FirstOrDefaultAsync(a => a.Id == Id && a.Active);
                return _mapper.MapToViewmodel(result);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public async void Update(ServiceProviderViewModel request)
        {
            using (var transaction = await _unitOfWork.BeginTransaction(System.Data.IsolationLevel.ReadCommitted))
            {
                try
                {
                    var exsitingServiceProvider = await _serviceProviderRepo.GetQueryable().Include(a => a.AppUser.PersonalDetail).
                        FirstOrDefaultAsync(a => a.Active && a.Id == request.ServiceProviderId);
                    if (exsitingServiceProvider == null)
                        throw new ArgumentNullException($"Service Proider detail with Id : {request.ServiceProviderId} does not exits.");
                    _personDetailService.Update(request.PersonDetail);
                    exsitingServiceProvider = _mapper.MapToEntity(request, exsitingServiceProvider);
                    _serviceProviderRepo.Update(exsitingServiceProvider);

                    await _unitOfWork.CommitAsync();
                    await transaction.CommitAsync();
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    throw;
                }
            }
        }
        public async void Delete(int Id)
        {
            try
            {
                var serviceProvider = await _serviceProviderRepo.GetByIdAsync(Id);
                if (serviceProvider == null)
                    throw new ArgumentNullException($"Service provider with Id : {Id} does not exist.");
                serviceProvider.Active = false;
                _serviceProviderRepo.Update(serviceProvider);
                await _unitOfWork.CommitAsync();
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public async Task<ServiceProviderContactPersonViewModel> AddContactPerson(ServiceProviderContactPersonViewModel request)
        {
            using (var transaction = await _unitOfWork.BeginTransaction(System.Data.IsolationLevel.ReadCommitted))
            {
                try
                {
                    var contactPerson = new ServiceProviderContactPerson();
                    int personId = 0;
                    var personDetail = new PersonalDetail();

                    personDetail.FirstName = request.FirstName;
                    personDetail.LastName = request.LastName;
                    foreach(var contact in request.ContactDetails)
                    {
                        personDetail.PersonalDetailContactDetails.Add(new PersonalDetailContactDetail()
                        {
                            ContactDetail = new ContactDetail()
                            {
                                ContactNumber = contact.Number,
                                Email = contact.Email
                            }
                        });
                    }
                    personDetail.PersonalDetailDocumentDetails.Add(new PersonalDetailDocumentDetail()
                    {
                        DocumentDetail = new DocumentDetail()
                        {
                            Type = nameof(DocumentType.Ctitzenship),
                            RegistrationNumber = request.CitizenshipNumber,
                            IssuedDistrict = request.CitizenshipIssuedDistrict
                        }
                    });
                    await _persondetailRepo.AddAsync(personDetail);
                    await _unitOfWork.CommitAsync();

                    personId = personDetail.Id;

                    contactPerson.ServiceProviderId = request.ServiceProviderId;
                    contactPerson.ContactPerson=new ContactPerson() { 
                        PersonId=personId
                    };
                    await _serviceProviderContactPersonRepo.AddAsync(contactPerson);
                    await _unitOfWork.CommitAsync();
                    await transaction.CommitAsync();

                    request.ContactPersonId = contactPerson.ContactPersonId;
                    return request;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }
        public async Task<ServiceProviderContactPersonViewModel> UpdateContactPerson(ServiceProviderContactPersonViewModel request)
        {
            using (var transaction=await _unitOfWork.BeginTransaction(System.Data.IsolationLevel.ReadCommitted))
            {
                try
                {
                    var contactPerson =await _serviceProviderContactPersonRepo.GetQueryable().Include(a=>a.ContactPerson).FirstOrDefaultAsync(a => a.ServiceProviderId == request.ServiceProviderId 
                    && a.ContactPersonId == request.ContactPersonId && a.ContactPerson.Active);
                    if (contactPerson == null)
                        throw new ArgumentNullException($"Contact person with ServiceProviderId: {request.ServiceProviderId} and contact personId: {request.ContactPersonId}");
                    var personDetail =await _persondetailRepo.GetQueryable().Include(a => a.PersonalDetailContactDetails)
                        .Include(a => a.PersonalDetailDocumentDetails).FirstOrDefaultAsync(a=>a.Id==contactPerson.ContactPersonId);

                    personDetail.FirstName = request.FirstName;
                    personDetail.LastName = request.LastName;
                    var contactDetails=personDetail.PersonalDetailContactDetails;
                    foreach(var contact in request.ContactDetails)
                    {
                        var updateContact = contactDetails.FirstOrDefault(a => a.ContactDetailId == contact.ContactDetailId).ContactDetail;
                        if (updateContact == null)
                            throw new ArgumentNullException($"Contact detail with id: {contact.ContactDetailId} does not exist.");
                        updateContact.ContactNumber = contact.Number;
                        updateContact.Email = contact.Email;
                        _contactDetailRepo.Update(updateContact);
                    }

                    var citizenshipDoc = personDetail.PersonalDetailDocumentDetails.FirstOrDefault(a=>a.DocumentDetail.Type==nameof(DocumentType.Ctitzenship)).DocumentDetail;
                    citizenshipDoc.RegistrationNumber = request.CitizenshipNumber;
                    citizenshipDoc.IssuedDistrict = request.CitizenshipIssuedDistrict;
                    _documentRepo.Update(citizenshipDoc);

                    return request;
                }
                catch (Exception ex)
                {
                    throw;
                }
            }
        }
    }
}
