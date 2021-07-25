using DhuwaniSewa.Model.Constant;
using DhuwaniSewa.Model.DbEntities;
using DhuwaniSewa.Model.Enum;
using DhuwaniSewa.Model.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DhuwaniSewa.Domain
{
    public class ServiceProviderMapper : IServiceProviderMapper
    {
        private readonly IPersonalDetailMapper _personMapper;
        private readonly ISerialNumberSevice _serialNumberSevice;
        private readonly IFiscalYearService _fiscalYearService; // TO DO: Implement fiscal year list in cache and get curren fiscal year 
        public ServiceProviderMapper(
            IPersonalDetailMapper personMapper,
            ISerialNumberSevice serialNumberSevice,
            IFiscalYearService fiscalYear
        )
        {
            this._personMapper = personMapper;
            this._serialNumberSevice = serialNumberSevice;
            this._fiscalYearService = fiscalYear;
        }

        public ServiceProviderViewModel MapToViewmodel(ServiceProvider source, ServiceProviderViewModel destination = null)
        {
            try
            {
                if (destination == null)
                    destination = new ServiceProviderViewModel();
                destination.Active = source.Active;
                destination.IsCompany = source.AppUser.IsCompnay;
                destination.ServiceProviderId = source.Id;
                destination.DhuwaniSewaId = source.DhuwaniSewaId;
                destination.DetailsCorrectAggreed = source.DetailsCorrectAgreed;
                if (destination.IsCompany)
                {
                    destination.CompanyDetail = new CompanyDetailViewModel()
                    {
                        Name = source.AppUser.CompanyDetail.FirstOrDefault().Name
                    };
                }
                else
                {
                    destination.PersonDetail = _personMapper.MapToViewmodel(source.AppUser.PersonalDetail.FirstOrDefault().PersonalDetail);
                }
                foreach (var vehicle in source.ServiceProviderVehicleDetail)
                {
                    destination.VehicleDetails.Add(new VehicleDetailViewModel()
                    {
                        BrandId = vehicle.VehicleDetail.BrandId,
                        TypeId = vehicle.VehicleDetail.TypeId,
                        RegistrationNumber = vehicle.VehicleDetail.RegistrationNumber
                    });
                }
                return destination;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public ServiceProvider MapToEntity(ServiceProviderViewModel source, ServiceProvider destination = null)
        {
            try
            {
                if (destination == null)
                    destination = new ServiceProvider();
                destination.UserId = source.UserId;
                destination.Active = source.Active;
                destination.DetailsCorrectAgreed = source.DetailsCorrectAggreed;
                if (source.ServiceProviderId == 0)
                {
                    int sn = _serialNumberSevice.GetAsync(SerialNumber.ServiceProvider).Result;
                    string fs = _fiscalYearService.GetCurrentAsync().Result;
                    destination.DhuwaniSewaId = string.Format(DhuwaniSewaIdFormat.ServiceProviderIdFormat, fs, sn);
                }
                return destination;
            }
            catch (Exception ex)
            {
                throw;
            }

        }
    }
}
