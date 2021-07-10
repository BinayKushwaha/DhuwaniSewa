using DhuwaniSewa.Model.DbEntities;
using DhuwaniSewa.Model.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace DhuwaniSewa.Domain
{
    public interface IServiceProviderMapper
    {
        ServiceProvider MapToEntity(ServiceProviderViewModel source);
        ServiceProviderViewModel MapToViewmodel(ServiceProvider source);
    }
}
