using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Sabio.Web.Domain
{
    public class OnboardRegistration
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int Gender { get; set; }

        public int EthnicityId { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? DateOfBirth { get; set; }

        public string Phone { get; set; }

        public int AddressId { get; set; }

        public int CountryId { get; set; }

        public string Country { get; set; }

        public string StreetAddress1 { get; set; }

        public string StreetAddress2 { get; set; }

        public string City { get; set; }

        public int StateProvinceId { get; set; }

        public string StateProvince { get; set; }

        public string ZipCode { get; set; }

        public string Facebook { get; set; }

        public string LinkedIn { get; set; }

        public string Twitter { get; set; }

        public string Webpage { get; set; }

        public int EmploymentStatusId { get; set; }

        public string Bio { get; set; }

        public string ProgrammingExperience { get; set; }

        public string WorkExperience { get; set; }

        public string ExtraCurricularActivities { get; set; }

        public string LearningObjective { get; set; }

        public string ReferredBy { get; set; }

        public int DesiredTrack { get; set; }

        public int DesiredCampusLocation { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? DesiredSabioStartDate { get; set; }

        public string TargetEmploymentLocation { get; set; }

        public int AccreditationId { get; set; }

        public int OnboardStatus { get; set; }


    }
}