using Sabio.Web.Domain;
using Sabio.Web.Models.Requests.Testimonials;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sabio.Web.Services.Interfaces
{
    public interface ITestimonialsService
    {
        Testimonials Get(int id);

        int Insert(TestimonialsAddRequest model);

        void Update(TestimonialsUpdateRequest model);

        List<Testimonials> GetList();

        void Delete(int id);
    }
}