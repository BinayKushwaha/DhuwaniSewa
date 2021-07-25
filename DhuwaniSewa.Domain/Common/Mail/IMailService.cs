using DhuwaniSewa.Model.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DhuwaniSewa.Domain
{
    public interface IMailService
    {
        Task SendMailAsync(MailViewModel request);
    }
}
