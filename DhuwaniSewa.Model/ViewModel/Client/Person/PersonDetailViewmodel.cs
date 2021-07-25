using DhuwaniSewa.Model.DbEntities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace DhuwaniSewa.Model.ViewModel
{
    public class PersonDetailViewmodel 
    {
        public PersonDetailViewmodel()
        {
            ContactDetails = new List<ContactDetailViewModel>();
            Documents = new List<DocumentDetailViewModel>();
        }
        public int PersondetailId { get; set; }
        public int UserId { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public IList<ContactDetailViewModel> ContactDetails { get; set; }
        public IList<DocumentDetailViewModel> Documents { get; set; }
    }
}