using DhuwaniSewa.Model.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DhuwaniSewa.Domain
{
    public interface IServiceProviderService
    {
        Task<int> Save(ServiceProviderViewModel reuest);
        void Update(ServiceProviderViewModel request);
        IList<ServiceProviderViewModel> GetAll();
    }
}
