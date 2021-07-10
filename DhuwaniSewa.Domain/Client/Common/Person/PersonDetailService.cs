using AutoMapper;
using DhuwaniSewa.Database.Repository;
using DhuwaniSewa.Model.DbEntities;
using DhuwaniSewa.Model.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DhuwaniSewa.Domain
{
    public class PersonDetailService : IPersonDetailService
    {
        private readonly IRepositoryService<PersonalDetail, int> _personRepo;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPersonalDetailMapper _mapper;
        public PersonDetailService(
            IRepositoryService<PersonalDetail, int> personRepo,
            IUnitOfWork unitOfWork,
            IPersonalDetailMapper mapper)
        {
            this._personRepo = personRepo;
            this._unitOfWork = unitOfWork;
            this._mapper = mapper;
        }
        public async Task<int> Save(PersonDetailViewmodel request)
        {
            try
            {
                var personDetail = _mapper.MapToEntity(request);
                await _personRepo.AddAsync(personDetail);
                await _unitOfWork.CommitAsync();
                return personDetail.Id;
            }
            catch(Exception ex)
            {
                throw;
            }
        }

        public IList<PersonDetailViewmodel> GetALL()
        {
            throw new NotImplementedException();
        }
    }
}
