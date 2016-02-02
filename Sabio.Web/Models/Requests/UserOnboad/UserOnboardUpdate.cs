using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Sabio.Web.Domain;

namespace Sabio.Web.Models.Requests.UserOnboard
{
    public class UserOnboardUpdate1
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public int DesiredTrack { get; set; }

        [Required]
        public int DesiredCampusLocation { get; set; }

        public DateTime DesiredSabioStartDate { get; set; }

        [Required]
        public int AccreditationId { get; set; }
    }

    public class UserOnboardUpdate2
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [StringLength(20, MinimumLength = 2, ErrorMessage = "FirstName Must Contain 2-20 Characters")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(20, MinimumLength = 2, ErrorMessage = "LastName Must Contain 2-20 Characters")]
        public string LastName { get; set; }

        [Required]
        public int Gender { get; set; }

        [Required]
        public int EthnicityId { get; set; }

        public DateTime DateOfBirth { get; set; }

        [Required]
        [StringLength(20, MinimumLength = 10, ErrorMessage = "Phone number must have atleast 10 digits")]
        [DataType(DataType.PhoneNumber)]
        public string Phone { get; set; }
    }

    public class UserOnboardUpdate3
    {
        [Required]
        public int Id { get; set; }

        public int AddressId { get; set; }

        [Required]
        public string StreetAddress1 { get; set; }

        public string StreetAddress2 { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(20)]
        public string City { get; set; }

        [Required]
        public int StateProvinceId { get; set; }

        [Required]
        [MinLength(5)]
        [MaxLength(20)]
        public string ZipCode { get; set; }
    }

    public class UserOnboardUpdate4
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string ReferredBy { get; set; }

        [Required]
        public string ExtraCurricularActivities { get; set; }

        public string Facebook { get; set; }

        public string LinkedIn { get; set; }

        public string Twitter { get; set; }

        public string Webpage { get; set; }
    }

    public class UserOnboardUpdate5
        {
            [Required]
            public int Id { get; set; }

            [Required]
            public int EmploymentStatusId { get; set; }

            [Required]
            public string LearningObjective { get; set; }

            [Required]
            public string TargetEmploymentLocation { get; set; }

            [Required]
            [StringLength(300, MinimumLength = 15, ErrorMessage = "Bio must contain at least 15 characters")]
            public string Bio { get; set; }

            [Required]
            public string ProgrammingExperience { get; set; }

            [Required]
            public string WorkExperience { get; set; }

            [Required]
            public int OnboardStatus { get; set; }
        }

}