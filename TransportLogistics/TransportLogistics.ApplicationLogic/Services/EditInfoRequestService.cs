using System;
using System.Collections.Generic;
using System.Text;
using TransportLogistics.DataAccess.Abstractions;
using TransportLogistics.Model;

namespace TransportLogistics.ApplicationLogic.Services
{
    public class EditInfoRequestService
    {
        private readonly IPersistenceContext PersistenceContext;
        private readonly IPersonalInfoRepository PersonalInfoRepository;

        public EditInfoRequestService(IPersistenceContext persistenceContext)
        {
            PersistenceContext = persistenceContext;
            PersonalInfoRepository = persistenceContext.PersonalInfoRepository;
        }
        public IEnumerable<PersonalInfoRequest> GetAllRequests()
        {
            return PersonalInfoRepository.GetAll();
        }
        public void CreateEditInfoRequest(Guid applicant, string newName, string newEmail, string newPhoneNumber, string oldName,
                                                 string oldEmail, string oldPhoneNumber)
        {
            var request = PersonalInfoRequest.Create( applicant,  newName,  newEmail,  newPhoneNumber,  oldName, oldEmail,  oldPhoneNumber);
            PersonalInfoRepository.Add(request);
            PersistenceContext.SaveChanges();
        }
    }
}
