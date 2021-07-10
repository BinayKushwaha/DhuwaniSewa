using AutoMapper;
using DhuwaniSewa.Database.Repository;
using DhuwaniSewa.Model.Constant;
using DhuwaniSewa.Model.DbEntities;
using DhuwaniSewa.Model.Enum;
using DhuwaniSewa.Model.ViewModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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
        public ServiceProviderService(
            IPersonDetailService  personDetailService,
            IRepositoryService<ServiceProvider, int> serviceProviderRepo,
            IServiceProviderMapper mapper,
            IFiscalYearService fiscalYearService,
            ISerialNumberSevice serialNumberSevice,
            IUnitOfWork unitOfWork
            )
        {
            this._serviceProviderRepo = serviceProviderRepo;
            this._mapper = mapper;
            this._serialNumberSevice = serialNumberSevice;
            this._fiscalYearService = fiscalYearService;
            this._unitOfWork = unitOfWork;
            _personDetailService = personDetailService;
        }
        public async Task<int> Save(ServiceProviderViewModel request)
        {
            try
            {
                request.PersonDetail.UserId = request.UserId;
                await _personDetailService.Save(request.PersonDetail);
                var serviceProvider = _mapper.MapToEntity(request);
                await _serviceProviderRepo.AddAsync(serviceProvider);
                await _unitOfWork.CommitAsync();
                return serviceProvider.Id;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public IList<ServiceProviderViewModel> GetAll()
        {
            try
            {
                var serviceProviders = _serviceProviderRepo.GetQueryable().Include(a => a.AppUser).Where(b => b.Active).ToList();
                var result = new List<ServiceProviderViewModel>();
                foreach(var serviceProvider in serviceProviders)
                {
                    result.Add(_mapper.MapToViewmodel(serviceProvider));
                }
                return result;
            }
            catch(Exception ex)
            {
                throw;
            }
        }
        public void Update(ServiceProviderViewModel request)
        {
            throw new NotImplementedException();
        }

    }
}
