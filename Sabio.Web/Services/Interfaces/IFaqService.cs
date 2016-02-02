using Sabio.Web.Domain;
using Sabio.Web.Models.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Sabio.Web.Services.Interfaces
{
    public interface IFaqService
    {
        int Insert(FAQAddRequest model);

        void Update(FAQUpdateRequest model);

        void Delete(int FAQId);

        FAQ Get(int FAQId);

        List<FAQ> GetFAQList();

    }
}
