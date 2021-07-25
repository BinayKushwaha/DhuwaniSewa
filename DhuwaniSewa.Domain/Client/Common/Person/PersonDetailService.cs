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

        public async Task<IList<PersonDetailViewmodel>> GetALL()
        {
            try
            {
                var result = new List<PersonDetailViewmodel>();
                var personDetails =await _personRepo.GetAllAsync();
                foreach(var person in personDetails)
                {
                    result.Add(_mapper.MapToViewmodel(person));
                }
                return result;
            }
            catch(Exception ex)
            {
                throw;
            }
        }
        public async void Update(PersonDetailViewmodel request) 
        {
            try
            {
                var existingPerson =await _personRepo.GetByIdAsync(request.PersondetailId);
                if (existingPerson == null)
                    throw new ArgumentNullException($"Person detail with id : {request.PersondetailId} does not exist");
                existingPerson= _mapper.MapToEntity(request,existingPerson);
                _personRepo.Update(existingPerson);
                await _unitOfWork.CommitAsync();
            }   
            catch(Exception ex)
            {
                throw;
            }
        }
        public async void Delete(int Id)
        {
            try
            {
                var person =await _personRepo.GetByIdAsync(Id);
                if (person == null)
                    throw new ArgumentNullException($"Person detail with id : {Id} does not exit");
                _personRepo.Delete(person);
                await _unitOfWork.CommitAsync();
            }
            catch(Exception ex)
            {
                throw;
            }
        }
    }
}
