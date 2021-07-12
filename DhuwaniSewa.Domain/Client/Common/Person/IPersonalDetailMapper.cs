using DhuwaniSewa.Model.DbEntities;
using DhuwaniSewa.Model.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace DhuwaniSewa.Domain
{
    public interface IPersonalDetailMapper
    {
        PersonDetailViewmodel MapToViewmodel(PersonalDetail source, PersonDetailViewmodel destination=null);
        PersonalDetail MapToEntity(PersonDetailViewmodel source, PersonalDetail destination=null);
    }
}
