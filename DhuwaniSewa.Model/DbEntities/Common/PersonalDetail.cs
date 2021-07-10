using DhuwaniSewa.Model.DbEntities;
using System;
using System.Collections.Generic;
using System.Text;

namespace DhuwaniSewa.Model.DbEntities
{
    public class PersonalDetail
    {
        public PersonalDetail()
        {
            PersonalDetailDocumentDetails = new List<PersonalDetailDocumentDetail>();
            PersonalDetailContactDetails = new List<PersonalDetailContactDetail>();
        }
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public int AppUserId { get; set; }
        public AppUsers  AppUsers { get; set; }
        public IList<PersonalDetailDocumentDetail> PersonalDetailDocumentDetails { get; set; }
        public IList<PersonalDetailContactDetail> PersonalDetailContactDetails { get; set; }
    }
}
