using System;
using System.Collections.Generic;
using System.Text;

namespace DhuwaniSewa.Model.DbEntities
{
    public class DocumentDetail
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public string Number { get; set; }
        public string IssuedDistrict { get; set; }
        public IList<PersonalDetailDocumentDetail> PersonalDetailDocumentDetails { get; set; }
    }
}
