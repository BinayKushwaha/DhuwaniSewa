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
        public ServiceProviderService(
            IPersonDetailService personDetailService,
            IRepositoryService<ServiceProvider, int> serviceProviderRepo,
            IServiceProviderMapper mapper,
            IFiscalYearService fiscalYearService,
            ISerialNumberSevice serialNumberSevice,
            IUnitOfWork unitOfWork,
            IRepositoryService<PersonalDetailDocumentDetail, int> personDocsRepo,
            IRepositoryService<PersonalDetailContactDetail, int> personContactRepo,
            IRepositoryService<ServiceProviderVehicleDetail, int> serviceProviderVehicleRepo
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
        }
        public async Task<int> Save(ServiceProviderViewModel request)
        {
            using (var transaction = await _unitOfWork.BeginTransaction(System.Data.IsolationLevel.ReadCommitted))
            {
                try
                {
                    request.PersonDetail.UserId = request.UserId;
                    await _personDetailService.Save(request.PersonDetail);
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

                var personIds = serviceProviders.SelectMany(a => a.AppUser.PersonalDetail.Select(s => s.Id)).ToList();
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
                        var personId = serviceProvider.AppUser.PersonalDetail.FirstOrDefault().Id;
                        var personDetail = serviceProvider.AppUser.PersonalDetail.FirstOrDefault();
                        var contactDetails = personContacts.Where(a => a.PersonalDetailId == personId).
                            Select(a => a.ContactDetail).ToList();
                        var documentDetails = personDocuments.Where(a => a.PersonDetailId == personId).
                            Select(a => a.DocumentDetail).ToList();
                        var spVehicles = serviceProviderVehicles.Where(a => a.ServiceProviderId == serviceProvider.Id).
                            Select(a => a.VehicleDetail);

                        model.PersonDetail = new PersonDetailViewmodel()
                        {
                            PersondetailId=personDetail.Id,
                            FirstName = personDetail.FirstName,
                            MiddleName = personDetail.MiddleName,
                            LastName = personDetail.LastName,
                            UserId = personDetail.AppUserId
                        };

                        foreach (var contact in contactDetails)
                        {
                            model.PersonDetail.ContactDetails.Add(new ContactDetailViewModel()
                            {
                                ContactDetailId=contact.Id,
                                Email = contact.Email,
                                Number = contact.ContactNumber
                            });
                        }

                        foreach (var document in documentDetails)
                        {
                            model.PersonDetail.Documents.Add(new DocumentDetailViewModel()
                            {
                                DocumentDetailId=document.Id,
                                Type = document.Type,
                                RegistrationNumber = document.RegistrationNumber,
                                IssuedDistrict = document.IssuedDistrict
                            });
                        }
                        foreach (var vehicle in spVehicles)
                        {
                            model.VehicleDetails.Add(new VehicleDetailViewModel()
                            {
                                VehicleDetailId=vehicle.Id,
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
    }
}
