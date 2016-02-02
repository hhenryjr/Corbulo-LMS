using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Core;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using Microsoft.Owin.Security.Provider;
using Sabio.Data;
using Sabio.Web.Domain;
using Sabio.Web.Models.Requests.UserOnboard;
using Sabio.Web.Services.Interfaces;

namespace Sabio.Web.Services
{
    public class UserOnboardService : BaseService, IUserOnboardService
    {

        public int Get()
        {
            return 0;
        }

        public OnboardRegistration Get(int id)
        {
            OnboardRegistration s = null;
            DataProvider.ExecuteCmd(GetConnection, "dbo.Users_Onboard_SelectById",
                inputParamMapper: delegate(SqlParameterCollection parameterCollection)
                {
                    parameterCollection.AddWithValue("@Id", id);
                },
                map: delegate(IDataReader reader, short set)
                {
                    s = UserMap(reader);
                }
                );
            return s;
        }

        private OnboardRegistration UserMap(IDataReader reader)
        {
            OnboardRegistration s = new OnboardRegistration();
            int startingIndex = 0;
            s.Id = reader.GetSafeInt32(startingIndex++);
            s.FirstName = reader.GetSafeString(startingIndex++);
            s.LastName = reader.GetSafeString(startingIndex++);
            s.Gender = reader.GetSafeInt32(startingIndex++);
            s.EthnicityId = reader.GetSafeInt32(startingIndex++);
            s.DateOfBirth = reader.GetSafeDateTimeNullable(startingIndex++);
            s.Phone = reader.GetSafeString(startingIndex++);
            s.AddressId = reader.GetSafeInt32(startingIndex++);
            s.CountryId = reader.GetSafeInt32(startingIndex++);
            s.Country = reader.GetSafeString(startingIndex++);
            s.StreetAddress1 = reader.GetSafeString(startingIndex++);
            s.StreetAddress2 = reader.GetSafeString(startingIndex++);
            s.City = reader.GetSafeString(startingIndex++);
            s.StateProvinceId = reader.GetSafeInt32(startingIndex++);
            s.StateProvince = reader.GetSafeString(startingIndex++);
            s.ZipCode = reader.GetSafeString(startingIndex++);
            s.Facebook = reader.GetSafeString(startingIndex++);
            s.LinkedIn = reader.GetSafeString(startingIndex++);
            s.Twitter = reader.GetSafeString(startingIndex++);
            s.Webpage = reader.GetSafeString(startingIndex++);
            s.EmploymentStatusId = reader.GetSafeInt32(startingIndex++);
            s.Bio = reader.GetSafeString(startingIndex++);
            s.ProgrammingExperience = reader.GetSafeString(startingIndex++);
            s.WorkExperience = reader.GetSafeString(startingIndex++);
            s.ExtraCurricularActivities = reader.GetSafeString(startingIndex++);
            s.LearningObjective = reader.GetSafeString(startingIndex++);
            s.ReferredBy = reader.GetSafeString(startingIndex++);
            s.DesiredTrack = reader.GetSafeInt32(startingIndex++);
            s.DesiredCampusLocation = reader.GetSafeInt32(startingIndex++);
            s.DesiredSabioStartDate = reader.GetSafeDateTimeNullable(startingIndex++);
            s.TargetEmploymentLocation = reader.GetSafeString(startingIndex++);
            s.AccreditationId = reader.GetSafeInt32(startingIndex++);
            s.OnboardStatus = reader.GetSafeInt32(startingIndex++);
            return s;
        }

        public void Update1(UserOnboardUpdate1 model)
        {
            DataProvider.ExecuteNonQuery(GetConnection, "dbo.Users_Onboard_Update1",
           inputParamMapper: delegate(SqlParameterCollection u)
           {
               u.AddWithValue("@Id", model.Id);
               u.AddWithValue("@DesiredTrack", model.DesiredTrack);
               u.AddWithValue("@DesiredCampusLocation", model.DesiredCampusLocation);
               u.AddWithValue("@DesiredSabioStartDate", model.DesiredSabioStartDate);
               u.AddWithValue("@AccreditationId", model.AccreditationId);
           });
        }

        public void Update2(UserOnboardUpdate2 model)
        {
            DataProvider.ExecuteNonQuery(GetConnection, "dbo.Users_Onboard_Update2",
           inputParamMapper: delegate(SqlParameterCollection u)
           {
               u.AddWithValue("@Id", model.Id);
               u.AddWithValue("@FirstName", model.FirstName);
               u.AddWithValue("@LastName", model.LastName);
               u.AddWithValue("@Gender", model.Gender);
               u.AddWithValue("@EthnicityId", model.EthnicityId);
               u.AddWithValue("@DateOfBirth", model.DateOfBirth);
               u.AddWithValue("@Phone", model.Phone);
           });
        }

        public void Update3(UserOnboardUpdate3 model)
        {
            DataProvider.ExecuteNonQuery(GetConnection, "dbo.Users_Onboard_Update3",
           inputParamMapper: delegate(SqlParameterCollection u)
           {
               u.AddWithValue("@Id", model.Id);
               u.AddWithValue("@AddressId", model.AddressId);
               u.AddWithValue("@StreetAddress1", model.StreetAddress1);
               u.AddWithValue("@StreetAddress2", model.StreetAddress2);
               u.AddWithValue("@City", model.City);
               u.AddWithValue("@StateProvinceId", model.StateProvinceId);
               u.AddWithValue("@ZipCode", model.ZipCode);
           });
        }

        public void Update4(UserOnboardUpdate4 model)
        {
            DataProvider.ExecuteNonQuery(GetConnection, "dbo.Users_Onboard_Update4",
           inputParamMapper: delegate(SqlParameterCollection u)
           {
               u.AddWithValue("@Id", model.Id);
               u.AddWithValue("@ReferredBy", model.ReferredBy);
               u.AddWithValue("@ExtraCurricularActivities", model.ExtraCurricularActivities);
               u.AddWithValue("@Facebook", model.Facebook);
               u.AddWithValue("@LinkedIn", model.LinkedIn);
               u.AddWithValue("@Twitter", model.Twitter);
               u.AddWithValue("@Webpage", model.Webpage);
           });
        }

        public void Update5(UserOnboardUpdate5 model)
        {
            DataProvider.ExecuteNonQuery(GetConnection, "dbo.Users_Onboard_Update5",
           inputParamMapper: delegate(SqlParameterCollection u)
           {
               u.AddWithValue("@Id", model.Id);
               u.AddWithValue("@EmploymentStatusId", model.EmploymentStatusId);
               u.AddWithValue("@LearningObjective", model.LearningObjective);
               u.AddWithValue("@TargetEmploymentLocation", model.TargetEmploymentLocation);
               u.AddWithValue("@Bio", model.Bio);
               u.AddWithValue("@ProgrammingExperience", model.ProgrammingExperience);
               u.AddWithValue("@WorkExperience", model.WorkExperience);
               u.AddWithValue("@OnboardStatus", model.OnboardStatus);
           });
        }
    }
}