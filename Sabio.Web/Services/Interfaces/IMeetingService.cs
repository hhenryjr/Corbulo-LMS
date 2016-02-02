using Sabio.Web.Domain;
using Sabio.Web.Models.Requests.Meeting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sabio.Web.Services.Interfaces
{
    public interface IMeetingService
    {
        int Insert(MeetingAddRequest model);
        int InsertOld(MeetingAddRequest model);
        void Update(MeetingUpdateRequest model);
        Meeting GetById(int id);
        Meeting GetByIdOld(int id);
        List<Meeting> GetAll();
        List<Meeting> GetAllOld();
        void Delete(int id);
    }
}
