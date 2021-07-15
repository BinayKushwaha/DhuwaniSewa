using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DhuwaniSewa.Model.ViewModel
{
    public class ServiceProviderContactPersonViewModel
    {
        public ServiceProviderContactPersonViewModel()
        {
            ContactDetails = new List<ContactDetailViewModel>();
        }
        
        [Required]
        public int ServiceProviderId { get; set; }
        public int ContactPersonId { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string CitizenshipNumber { get; set; }
        [Required]
        public string CitizenshipIssuedDistrict { get; set; }
        public IList<ContactDetailViewModel> ContactDetails { get; set; }
        //TO DO: Citizenship attachment
    }
}
