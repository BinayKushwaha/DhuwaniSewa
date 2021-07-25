using DhuwaniSewa.Database.Repository;
using DhuwaniSewa.Model.DbEntities;
using DhuwaniSewa.Model.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DhuwaniSewa.Domain
{
    public class FiscalYearService : IFiscalYearService
    {
        private readonly IRepositoryService<FiscalYear, int> _fiscalYearRepo;
        public FiscalYearService(IRepositoryService<FiscalYear, int> fiscalYearRepo)
        {
            _fiscalYearRepo = fiscalYearRepo;
        }
        public async Task<IList<FiscalYearDetailViewModel>> GetAllAsync()
        {
            try
            {
                var fiscalYears = await _fiscalYearRepo.GetAllAsync();
                var result = new List<FiscalYearDetailViewModel>();
                foreach (var fiscalYear in fiscalYears)
                {
                    result.Add(new FiscalYearDetailViewModel()
                    {
                        Name = fiscalYear.Name,
                        StartDate = fiscalYear.StartDate,
                        EndDate = fiscalYear.EndDate
                    });
                }
                return result;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public async Task<string> GetCurrentAsync() {
            try
            {
                string fiscalYear = string.Empty;
                var currentDate = DateTime.Now;
                var fiscalYearDetail = await _fiscalYearRepo.GetAync(a => currentDate.Date >= a.StartDate && currentDate.Date <= a.EndDate);
                fiscalYear = fiscalYearDetail?.Name;
                return fiscalYear;
            }
            catch(Exception ex)
            {
                throw;
            }
        }

    }
}
