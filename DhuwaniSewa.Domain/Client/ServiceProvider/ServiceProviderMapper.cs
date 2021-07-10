using DhuwaniSewa.Model.Constant;
using DhuwaniSewa.Model.DbEntities;
using DhuwaniSewa.Model.Enum;
using DhuwaniSewa.Model.ViewModel;
using System;
using System.Collections.Generic;
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

        public ServiceProviderViewModel MapToViewmodel(ServiceProvider source)
        {
            try
            {
                ServiceProviderViewModel destination = new ServiceProviderViewModel();
                destination.Active = source.Active;
                destination.IsCompany = source.AppUser.IsCompnay;
                destination.ServiceProviderId = source.Id;
                destination.DhuwaniSewaId = source.DhuwaniSewaId;
                destination.DetailsCorrectAggreed = source.DetailsCorrectAgreed;
                if (destination.IsCompany)
                {
                    destination.CompanyDetail = new CompanyDetailViewModel()
                    {
                        Name = source.AppUser.CompanyDetail.Name
                    };
                }
                else
                {
                    destination.PersonDetail = _personMapper.MapToViewmodel(source.AppUser.PersonalDetail);
                }
                return destination;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public ServiceProvider MapToEntity(ServiceProviderViewModel source)
        {
            try
            {
                ServiceProvider destination = new ServiceProvider();
                destination.Active = source.Active;
                destination.DetailsCorrectAgreed = source.DetailsCorrectAggreed;
                if (source.ServiceProviderId == 0)
                {
                    int sn = _serialNumberSevice.Get(SerialNumber.ServiceProvider).Result;
                    string fs = _fiscalYearService.GetCurrent().Result;
                    destination.DhuwaniSewaId = string.Format(DhuwaniSewaIdFormat.ServiceProviderIdFormat,fs,sn);
                }
                destination.UserId = source.UserId;
                return destination;
            }
            catch (Exception ex)
            {
                throw;
            }

        }
    }
}
