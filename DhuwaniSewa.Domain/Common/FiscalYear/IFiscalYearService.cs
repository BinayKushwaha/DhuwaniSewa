using DhuwaniSewa.Model.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DhuwaniSewa.Domain
{
    public interface IFiscalYearService
    {
        Task<IList<FiscalYearDetailViewModel>> GetAllAsync();

        // TO DO: Implement fiscal year list in cache and get curren fiscal year 
        Task<string> GetCurrentAsync();
    }
}
