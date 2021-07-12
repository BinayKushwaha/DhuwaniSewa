using System;
using System.Collections.Generic;
using System.Text;

namespace DhuwaniSewa.Model.DbEntities
{
    public class DocumentDetail
    {
        public DocumentDetail()
        {
            PersonalDetailDocumentDetails = new HashSet<PersonalDetailDocumentDetail>();
        }
        public int Id { get; set; }
        public string Type { get; set; }
        public string RegistrationNumber { get; set; }
        public string IssuedDistrict { get; set; }
        public virtual ICollection<PersonalDetailDocumentDetail> PersonalDetailDocumentDetails { get; set; }
    }
}
