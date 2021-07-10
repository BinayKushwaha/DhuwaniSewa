using DhuwaniSewa.Model.DbEntities;
using DhuwaniSewa.Model.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace DhuwaniSewa.Domain
{
    public interface IPersonalDetailMapper
    {
        PersonDetailViewmodel MapToViewmodel(PersonalDetail source);
        PersonalDetail MapToEntity(PersonDetailViewmodel source);
    }
}
