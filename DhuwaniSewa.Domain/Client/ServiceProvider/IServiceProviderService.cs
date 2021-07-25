using DhuwaniSewa.Model.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DhuwaniSewa.Domain
{
    public interface IServiceProviderService
    {
        Task<int> SaveAsync(ServiceProviderViewModel reuest);
        void Update(ServiceProviderViewModel request);
        IList<ServiceProviderViewModel> GetAll();
        Task<ServiceProviderViewModel> Get(int Id);
        void Delete(int Id);
        Task<ServiceProviderContactPersonViewModel> AddContactPerson(ServiceProviderContactPersonViewModel request);
        Task<ServiceProviderContactPersonViewModel> UpdateContactPerson(ServiceProviderContactPersonViewModel request);
    }
}
