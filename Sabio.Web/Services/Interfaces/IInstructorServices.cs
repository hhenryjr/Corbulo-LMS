using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Sabio.Web.Domain;
using Sabio.Web.Models.Requests.InstructorsRequest;
using Sabio.Web.Models.Requests;

namespace Sabio.Web.Services.Interfaces
{
    public interface IInstructorServices
    {
        int InsertInstructor(Sabio.Web.Models.Requests.AddInstructorInfo model, string userId);

        void Update(UpdateInstructorInfo model);

        List<Instructors> GetInstructors(List<int> ids);

        Instructors GetInstructors(int id);

        List<Instructors> Instructors();

        void Delete(int id);
    }
}