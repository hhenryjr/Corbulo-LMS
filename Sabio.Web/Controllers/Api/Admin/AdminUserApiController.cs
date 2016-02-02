using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Sabio.Web.Domain;
using Sabio.Web.Models;
using Sabio.Web.Models.Responses;
using Sabio.Web.Services;
using Sabio.Web.Models.Requests.UsersData;
using Sabio.Web.Services.Interfaces;

namespace Sabio.Web.Controllers.Api.Admin
{
    //  TODO: add role check here
    [RoutePrefix("admin/users")]
    public class AdminUserApiController : BaseAdminApiController
    {

        private IUserDataService _userDataService;

        public AdminUserApiController(IUserDataService UserDataService)
        {
            _userDataService = UserDataService;
        }

        [Route("Roles"), HttpGet]
        public HttpResponseMessage GetRoles()
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            ItemsResponse<RolesDomain> response = new ItemsResponse<RolesDomain>();

            response.Items = UserService.GetRoles();

            return Request.CreateResponse(response);
        }

        [Route("All"), HttpGet]
        public HttpResponseMessage NetUsers()
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            ItemsResponse<NetUserDomain> response = new ItemsResponse<NetUserDomain>();

            response.Items = UserService.GetUserList();

            return Request.CreateResponse(response);
        }

        [Route("{id}"), HttpGet]
        public HttpResponseMessage UserById(string id)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            ItemResponse<ApplicationUser> response = new ItemResponse<ApplicationUser>();

            response.Item = UserService.GetUserById(id);

            return Request.CreateResponse(response);
        }

        [Route("{id}/role/{roleId}"), HttpPut]
        public HttpResponseMessage SpacePut(string id, string roleId)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            SucessResponse response = new SucessResponse();
            UserService.AddRole(id, roleId);
            return Request.CreateResponse(response);
        }

        [Route("{id}/role/{roleId}"), HttpDelete]
        public HttpResponseMessage DeleteRole(string id, string roleId)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
            SucessResponse response = new SucessResponse();
            UserService.DeleteRole(id, roleId);
            return Request.CreateResponse(response);
        }

        //userSection start
        [Route("detail/{id:int}"), HttpGet]
        public HttpResponseMessage GetUser(int id)
        {
            ItemResponse<UserInfo> response = new ItemResponse<UserInfo>();

            response.Item = _userDataService.Get(id);
            return Request.CreateResponse(response);
        }

        [Route("UserList"), HttpGet]
        public HttpResponseMessage GetsUsers()
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            ItemsResponse<UserInfo> response = new ItemsResponse<UserInfo>();

            response.Items = _userDataService.GetList();

            return Request.CreateResponse(response);
        }

        [Route("UserStatus/{id:int}"), HttpPut]
        public HttpResponseMessage EditUserInfo(UserStatusUpdate model, int id)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            SucessResponse response = new SucessResponse();

            _userDataService.UpdateUserStatus(model);

            return Request.CreateResponse(response);

        }
        //userSection end

    }
}
