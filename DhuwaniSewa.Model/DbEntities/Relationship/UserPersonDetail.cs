
using System;
using System.Collections.Generic;
using System.Text;

namespace DhuwaniSewa.Model.DbEntities
{
    public class UserPersonDetail
    {
        public int UserId { get; set; }
        public int PersonId { get; set; }
        public AppUsers User { get; set; }
        public PersonalDetail PersonalDetail { get; set; }
    }
}
