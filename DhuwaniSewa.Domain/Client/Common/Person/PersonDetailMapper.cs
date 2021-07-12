using DhuwaniSewa.Model.DbEntities;
using DhuwaniSewa.Model.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace DhuwaniSewa.Domain
{
    public class PersonDetailMapper : IPersonalDetailMapper
    {
        public PersonDetailViewmodel MapToViewmodel(PersonalDetail source, PersonDetailViewmodel destination=null)
        {
            try
            {
                if(destination==null)
                    destination = new PersonDetailViewmodel();
                destination.FirstName = source.FirstName;
                destination.MiddleName = source.MiddleName;
                destination.LastName = source.LastName;
                foreach (var contact in source.PersonalDetailContactDetails)
                {
                    destination.ContactDetails.Add(new ContactDetailViewModel()
                    {
                        ContactDetailId = contact.ContactDetailId,
                        Email = contact.ContactDetail.Email,
                        Number = contact.ContactDetail.ContactNumber
                    });
                }
                foreach (var document in source.PersonalDetailDocumentDetails)
                {
                    destination.Documents.Add(new DocumentDetailViewModel()
                    {
                        DocumentDetailId = document.DocumentDetailId,
                        RegistrationNumber = document.DocumentDetail.RegistrationNumber,
                        IssuedDistrict = document.DocumentDetail.IssuedDistrict
                    });
                }
                return destination;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public PersonalDetail MapToEntity(PersonDetailViewmodel source,PersonalDetail destination)
        {
            try
            {
                if(destination==null)
                    destination = new PersonalDetail();
                destination.AppUserId = source.UserId;
                destination.FirstName = source.FirstName;
                destination.MiddleName = source.MiddleName;
                destination.LastName = source.LastName;
                foreach (var contact in source.ContactDetails)
                {
                    destination.PersonalDetailContactDetails.Add(new PersonalDetailContactDetail()
                    {
                        ContactDetail = new ContactDetail()
                        {
                            ContactNumber = contact.Number,
                            Email = contact.Email
                        }
                    });
                }
                foreach (var document in source.Documents)
                {
                    destination.PersonalDetailDocumentDetails.Add(new PersonalDetailDocumentDetail()
                    {
                        DocumentDetail = new DocumentDetail()
                        {
                            Type = document.Type,
                            RegistrationNumber = document.RegistrationNumber,
                            IssuedDistrict = document.IssuedDistrict
                        }
                    });
                }
                return destination;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
