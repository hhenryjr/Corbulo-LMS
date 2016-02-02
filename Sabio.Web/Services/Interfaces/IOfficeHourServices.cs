using Sabio.Web.Domain;
using Sabio.Web.Models.Requests;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using Sabio.Web.Models.Requests.OfficeHours;
using System.Net.Http;

namespace Sabio.Web.Services.Interfaces
{
    public interface IOfficeHourServices
    {
        int Add(OfficeHourAddRequest model, string userId);

        void Update(OfficeHourUpdateRequest model, string userId);

        OfficeHour Get(int Id);

        List<OfficeHour> GetList();

        void Delete(int id);

        List<OfficeHour> GetByDate();

        void AddQuestionResponse(OfficeHourQuestionsUpdateRequest model, int oId, int Id);

        OfficeHourQuestion getQuestion(int oId, int Id);

        void DeleteQuestion(int id);

        List<UserSection> GetEmailList(int Id);

        List<OfficeHourQuestion> GetByOfficeHourId(int id);

        void InsertQuestion(int Id,OfficeHourQuestion model,string userId);
    }
}