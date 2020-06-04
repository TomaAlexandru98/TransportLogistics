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

        public IEnumerable<PersonalInfoRequest> GetAllCreatedRequests()
        {
            return PersonalInfoRepository.GetAllCreatedRequests();
        }

        public PersonalInfoRequest GetById(Guid requestId)
        {
            return PersonalInfoRepository.GetById(requestId);
        }

        public void SetStatus(Guid requestId, EditStatusRequest refused)
        {
            var request = GetById(requestId);
            request.SetStatus(refused);
            PersonalInfoRepository.Update(request);
        }

        public void ApproveRequest(PersonalInfoRequest request, string id)
        {
            request.SetAdministrator(Guid.Parse(id));
            SetStatus(request.Id, EditStatusRequest.Accepted);
        }
    }
}
