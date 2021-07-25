using DhuwaniSewa.Database.Repository;
using DhuwaniSewa.Model.DbEntities;
using DhuwaniSewa.Model.Enum;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DhuwaniSewa.Domain
{
    public class SerialNumberService : ISerialNumberSevice
    {
        private readonly IRepositoryService<SerialNumbers,int> _serailNumberRepo;
        private readonly IUnitOfWork _unitOfWork;
        public SerialNumberService(IRepositoryService<SerialNumbers, int> serailNumberRepo,
            IUnitOfWork unitOfWork)
        {
            this._serailNumberRepo = serailNumberRepo;
            this._unitOfWork = unitOfWork;
        }
        public async Task<int> GetAsync(SerialNumber type)
        {
            try
            {
                var serailNumber = await _serailNumberRepo.GetAync()??new SerialNumbers();
                int number = 0;
                switch (type)
                {
                    case SerialNumber.Employee:
                        number = serailNumber.Employee;
                        number++;
                        serailNumber.Employee = number;
                        break;
                    case SerialNumber.ServiceProvider:
                        number = serailNumber.ServiceProvider;
                        number++;
                        serailNumber.ServiceProvider = number;
                        break;
                    case SerialNumber.ServiceSeeker:
                        number = serailNumber.ServiceSeeker;
                        number++;
                        serailNumber.ServiceSeeker = number;
                        break;
                }
                _serailNumberRepo.Update(serailNumber);
                await  _unitOfWork.CommitAsync();
                return number;
            }
            catch(Exception ex)
            {
                throw;
            }
        }
    }
}
