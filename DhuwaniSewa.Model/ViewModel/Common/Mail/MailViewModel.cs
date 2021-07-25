using System;
using System.Collections.Generic;
using System.Text;

namespace DhuwaniSewa.Model.ViewModel
{
    public class MailViewModel
    {
        public MailViewModel()
        {
            To = new List<string>();
        }
        public string Body { get; set; }
        public string Subject { get; set; }
        public List<string> To { get; set; }
    }
}
