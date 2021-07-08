using System;
using System.Collections.Generic;
using System.Text;

namespace DhuwaniSewa.Model.DbEntities
{
    public class PersonalDetailDocumentDetail
    {
        public int PersonDetailId { get; set; }
        public int DocumentDetailId { get; set; }

        public PersonalDetail PersonalDetail { get; set; }
        public DocumentDetail DocumentDetail { get; set; }
    }
}
