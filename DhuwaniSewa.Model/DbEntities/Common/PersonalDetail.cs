using DhuwaniSewa.Model.DbEntities;
using System;
using System.Collections.Generic;
using System.Text;

namespace DhuwaniSewa.Model.DbEntities
{
    public class PersonalDetail
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public int AppUserId { get; set; }
        public AppUsers  AppUsers { get; set; }
    }
}
