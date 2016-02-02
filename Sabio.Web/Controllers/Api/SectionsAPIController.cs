using Sabio.Web.Domain;
using Sabio.Web.Models.Responses;
using Sabio.Web.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Sabio.Web.Models.Requests.Sections;
using Sabio.Web.Models.Requests;

namespace Sabio.Web.Controllers.Api
{
    [RoutePrefix("api/sections")]
    public class SectionsAPIController : ApiController
    {
        ////GET SECTION BY ID
        //[Route("{id:int}"), HttpGet]
        //public HttpResponseMessage getSection(int id)
        //{
        //    ItemResponse<Section> response = new ItemResponse<Section>();

        //    response.Item = SectionsService.GetSection(id);

        //    return Request.CreateResponse(response);
        //}

        //// GET SECTIONS (LIST)
        //[Route("list"), HttpGet]
        //public HttpResponseMessage GetSections()
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
        //    }

        //    ItemsResponse<Section> response = new ItemsResponse<Section>();

        //    response.Items = SectionsService.GetSections();

        //    return Request.CreateResponse(response);
        //}

        ////GET CAMPUS LIST
        //[Route("campuses"), HttpGet]
        //public HttpResponseMessage GetCampuses()
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
        //    }

        //    ItemsResponse<Campuses> response = new ItemsResponse<Campuses>();

        //    response.Items = SectionsService.GetCampuses();

        //    return Request.CreateResponse(response);
        //}

        //[Route("register/user"), HttpPost]
        //public HttpResponseMessage AddUsers(UserSectionAddRequest model)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
        //    }

        //    ItemResponse<int> response = new ItemResponse<int>();

        //    string userId = UserService.GetCurrentUserId();
        //    response.Item = SectionsService.AddUserSection(model, userId);

        //    return Request.CreateResponse(response);
        //}

    }
}
