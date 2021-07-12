using DhuwaniSewa.Model.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DhuwaniSewa.Domain
{
    public interface IPersonDetailService
    {
        Task<int> Save(PersonDetailViewmodel request);
        Task<IList<PersonDetailViewmodel>> GetALL();
        void Update(PersonDetailViewmodel request);
        void Delete(int Id); 
    }
}
