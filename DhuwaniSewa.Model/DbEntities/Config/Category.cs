using System;
using System.Collections.Generic;
using System.Text;

namespace DhuwaniSewa.Model.DbEntities
{
    public class Category:RecordHistory
    {
        public int Id { get; set; }
        public bool Active { get; set; }
        public string Enum { get; set; }
        public string DisplayName { get; set; }
        public string Description { get; set; }
        public IList<Choice> Choices { get; set; }
    }
}
