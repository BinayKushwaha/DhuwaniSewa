using System;
using System.Collections.Generic;
using System.Text;

namespace DhuwaniSewa.Model.DbEntities
{
    public class PersonalDetailDocumentDetail
    {
        public int PersonDetailId { get; set; }
        public int DocumentDetailId { get; set; }

        public virtual PersonalDetail PersonalDetail { get; set; }
        public virtual DocumentDetail DocumentDetail { get; set; }
    }
}
