﻿using System;
using System.Collections.Generic;
using System.Text;

namespace TransportLogistics.Model
{
    public enum EditStatusRequest { Created,Accepted,Refused}
   public class PersonalInfoRequest:DataEntity
   {
        public Guid Applicant { get; private set; }
        public Guid Administrator { get; private set; }
        public string NewName { get; private set; }
        public string NewEmail { get; private set; }
        public string NewPhoneNumber { get; private set; }
        public string OldName { get; private set; }
        public string OldEmail { get; private set; }
        public string OldPhoneNumber { get; private set; }
        public EditStatusRequest Status { get; private set; }
        public static PersonalInfoRequest Create(Guid applicant , string newName , string newEmail , string newPhoneNumber,string oldName,
                                                 string oldEmail,string oldPhoneNumber)
        {
            var request = new PersonalInfoRequest()
            {
                Id = Guid.NewGuid(),
                Status = EditStatusRequest.Created,
                Applicant = applicant,
                NewName = newName,
                NewEmail = newEmail,
                NewPhoneNumber = newPhoneNumber,
                OldName = oldName,
                OldEmail = oldEmail,
                OldPhoneNumber = oldPhoneNumber
            };
            return request;
        }

        public void SetStatus(EditStatusRequest status)
        {
            Status = status;
        }

        public void SetAdministrator(Guid id)
        {
            Administrator = id;
        }
    }
}
