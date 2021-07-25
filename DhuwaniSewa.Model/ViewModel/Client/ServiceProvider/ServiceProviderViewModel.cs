using DhuwaniSewa.Model.DbEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DhuwaniSewa.Model.ViewModel
{
    public class ServiceProviderViewModel
    {
        public ServiceProviderViewModel()
        {
            VehicleDetails = new List<VehicleDetailViewModel>();
        }
        public int ServiceProviderId { get; set; }
        public bool Active { get; set; }
        public bool DetailsCorrectAggreed { get; set; }
        public string DhuwaniSewaId { get; set; }
        public int UserId { get; set; }
        public bool IsCompany { get; set; }
        public PersonDetailViewmodel PersonDetail { get; set; }
        public CompanyDetailViewModel CompanyDetail { get; set; }
        public IList<VehicleDetailViewModel> VehicleDetails { get; set; }
    }
}
