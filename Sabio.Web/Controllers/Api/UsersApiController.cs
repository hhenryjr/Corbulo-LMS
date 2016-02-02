using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Amazon.S3.Util;
using Glimpse.Core.Extensions;
using Sabio.Web.Domain;
using Sabio.Web.Models.Requests;
using Sabio.Web.Models.Requests.UsersData;
using Sabio.Web.Models.Responses;
using Sabio.Web.Services;
using Sabio.Web.Models.Requests.Login;
using Microsoft.AspNet.Identity.EntityFramework;
using Sabio.Web.Exceptions;
using Sabio.Web.Models.Requests.UserTokens;
using Sabio.Web.Models;
using Sabio.Web.Models.Requests.ForgotPassword;
using System.Threading.Tasks;
using Sabio.Web.Models.Requests.UserSettings;
using Sabio.Web.Services.Interfaces;

namespace Sabio.Web.Controllers.Api
{
    [AllowAnonymous]
    [RoutePrefix("api/user")]
    public class UsersApiController : ApiController
    {
        private IUserDataService _userDataService;
        private IAttendanceService _attendanceService;
        private IMessagingService _messagingService;

        public UsersApiController(IUserDataService UserDataService, IAttendanceService AttendanceService, IMessagingService messagingService)
        {
            _userDataService = UserDataService;
            _attendanceService = AttendanceService;
            _messagingService = messagingService;
        }

        // GET: api/UsersData
        [Route("Profile/{id:int}"), HttpGet]
        public HttpResponseMessage GetUser(int id)
        {
            ItemResponse<UserInfo> response = new ItemResponse<UserInfo>();

            response.Item = _userDataService.Get(id);
            return Request.CreateResponse(response);
        }

        [Route("Profile"), HttpGet]
        public HttpResponseMessage GetsUser()
        {
            ItemResponse<UserInfo> response = new ItemResponse<UserInfo>();
            string UserId = UserService.GetCurrentUserId();
            response.Item = _userDataService.GetByUserId(UserId);
            return Request.CreateResponse(response);
        }

        [Route("Profile"), HttpPost]
        public HttpResponseMessage AddUserData(UserInfoAddRequest model)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            ItemResponse<int> response = new ItemResponse<int>();
            string userId = UserService.GetCurrentUserId();
            response.Item = _userDataService.Insert(model, userId);

            return Request.CreateResponse(response);
        }

        [Route("profile/{id:int}"), HttpPut]
        public HttpResponseMessage EditUserInfo(UserInfoUpdate model, int id)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            SucessResponse response = new SucessResponse();

            _userDataService.Update(model);

            return Request.CreateResponse(response);

        }

        [Route("profile"), HttpDelete]
        public HttpResponseMessage DeletsUserInfo(UserInfoAddRequest model)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
            return Request.CreateResponse(model);

        }

        [Route("List"), HttpGet]
        public HttpResponseMessage GetsUsers()
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            ItemsResponse<UserInfo> response = new ItemsResponse<UserInfo>();

            response.Items = _userDataService.GetsList();

            return Request.CreateResponse(response);
        }

        [Route("Delete/{id:int}"), HttpDelete]
        public HttpResponseMessage Delete(int id)
        {
            SucessResponse response = new SucessResponse();
            _userDataService.Delete(id);
            return Request.CreateResponse(response);
        }

        [Route("Login"), HttpPost]
        public HttpResponseMessage Login(LoginRequest model)
        {
            HttpResponseMessage responseMessage = null;
            BaseResponse response = null;

            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            ApplicationUser user = UserService.GetUser(model.UserName);

            if (user != null)
            {
                Boolean emailConfirmed = UserService.GetEmailConfirmed(model.UserName);

                if (emailConfirmed)
                {
                    bool isValidLogin = false;

                    isValidLogin = UserService.Signin(model.UserName, model.Password);

                    if (isValidLogin)
                    {
                        response = new SucessResponse();
                        responseMessage = Request.CreateResponse(response);
                    }
                    else
                    {
                        response = new ErrorResponse("Login failed! Please check if you typed in the correct Username and Password.");
                        responseMessage = Request.CreateResponse(HttpStatusCode.BadRequest, response);
                    }
                }
                else
                {
                    response = new ErrorResponse("Please confirm your email before logging in.");
                    responseMessage = Request.CreateResponse(HttpStatusCode.BadRequest, response);
                }
            }
            else
            {
                response = new ErrorResponse("Username does not exist! Please register.");
                responseMessage = Request.CreateResponse(HttpStatusCode.BadRequest, response);
            }
            return responseMessage;
        }

        // Register with one endpoint 
        [Route("Register"), HttpPost]
        public HttpResponseMessage RegisterAdd(IdentityUser model)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState); //if email is NOT valid return error response 
            }
            try
            {
                IdentityUser register = UserService.CreateUser(model.UserName, model.Email, model.PasswordHash);

                if (register != null)
                {
                    UserTokensAddRequest tokensAddRequest = new UserTokensAddRequest();

                    tokensAddRequest.UserName = register.UserName;
                    tokensAddRequest.TokenType = 1;
                    tokensAddRequest.TokenId = UserTokensService.Add(tokensAddRequest);

                    SendConfirmationEmailRequest request = new SendConfirmationEmailRequest();
                    request.UserName = register.UserName;
                    request.Email = register.Email;
                    request.Token = tokensAddRequest.TokenId;

                    Task t = _messagingService.SendConfirmationEmail(request);
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest,
                        "Register wasn't successfull Please try again");
                }
                return Request.CreateResponse(register);
            }

            catch (IdentityResultException iex)
            {

                ErrorResponse er = new ErrorResponse(iex.Result.Errors);
                return Request.CreateResponse(HttpStatusCode.BadRequest, er);

            }

            catch (Exception ex)
            {

                ErrorResponse er = new ErrorResponse(ex.Message);
                return Request.CreateResponse(HttpStatusCode.BadRequest, er);

            }

        }

        [Route("UserTokens"), HttpPost]
        public HttpResponseMessage AddUserTokens(UserTokensAddRequest model)
        {
            // if the Model does not pass validation, there will be an Error response returned with errors
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            ItemResponse<Guid> response = new ItemResponse<Guid>();

            response.Item = UserTokensService.Add(model);

            return Request.CreateResponse(response);
        }

        // possibly obsolete?
        [Route("UserTokens/{id:guid}"), HttpGet]
        public HttpResponseMessage Get(Guid id)
        {
            ItemResponse<Token> response = new ItemResponse<Token>();

            response.Item = UserTokensService.Get(id);

            return Request.CreateResponse(response);
        }

        // possibly obsolete?
        [Route("UserTokens/{id:guid}"), HttpDelete]
        public HttpResponseMessage Delete(Guid id)
        {
            SucessResponse response = new SucessResponse();

            // Temporarily comment out for debugging
            //UserTokensService.Delete(id);

            return Request.CreateResponse(response);
        }

        // possibly obsolete?
        [Route("UserTokens/IsValid/{id:guid}"), HttpGet]
        public HttpResponseMessage IsValid(Guid id)
        {
            ItemResponse<bool> response = new ItemResponse<Boolean>();

            response.Item = UserTokensService.IsValid(id);

            return Request.CreateResponse(response);
        }

        [Route("ConfirmEmail/{tokenId:guid}"), HttpPost]
        public HttpResponseMessage ConfirmEmail(Guid tokenId)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            bool tokenIsValid = UserTokensService.IsValid(tokenId);
            if (!tokenIsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Request token is invalid or expired.");
            }

            // Attempt to set AspNetUsers.EmailConfirmed value = true and insert
            // new record in Users table
            bool confirmSuccess = _userDataService.ConfirmEmail(tokenId);
            if (!confirmSuccess)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Failed to confirm registration.");
            }
            return Request.CreateResponse(new SucessResponse());
        }

        [Route("SendEmail"), HttpPost]
        public HttpResponseMessage AddEmail(ForgotPasswordRequest model)
        {
            HttpResponseMessage responseMessage = null;
            BaseResponse response = null;

            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState); //if email is NOT valid return error response 
            }

            ApplicationUser user = UserService.GetUserByEmail(model.Email); //grabs user email


            if (user != null)
            {

                UserTokensAddRequest foo = new UserTokensAddRequest();
                foo.UserName = user.UserName;
                foo.TokenType = 2;
                Guid tokenId = UserTokensService.Add(foo);

                SendConfirmationEmailRequest request = new SendConfirmationEmailRequest();
                request.UserName = user.UserName;
                request.Email = user.Email;
                request.Token = tokenId;

                Task t = _messagingService.SendForgotPasswordEmail(request); //calling it to run
            }
            else
            {
                response = new ErrorResponse("I'm sorry, but your email can't be found, Please use correct Email");
                responseMessage = Request.CreateResponse(HttpStatusCode.BadRequest, response);
                return responseMessage;
            }

            return Request.CreateResponse(user);
        }

        [Route("ResetPassword"), HttpPut]
        public HttpResponseMessage NewPassword(ResetPasswordRequest model)
        {
            BaseResponse resp = null;

            if (!ModelState.IsValid)  //if my data is NOT valid 
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState); //it will return as error response
            }

            var success = UserService.ResetPassword(model);

            if (!success)
            {
                resp = new ErrorResponse("Unable to change password! Your password must have the following: At least one uppercase ('A'-'Z'), at least one digit ('0'-'9'), and at least one special character.");
                return Request.CreateResponse(HttpStatusCode.BadRequest, resp);
            }

            ItemResponse<bool> response = new ItemResponse<bool>();
            response.Item = success;
            return Request.CreateResponse(response);

        }

        [Route, HttpPut]
        public HttpResponseMessage UpdateSetting(UserSettingsRequest model)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            SucessResponse response = new SucessResponse();
            UserService.UpdateSettings(model);
            return Request.CreateResponse(response);

        }

        //[Route("Settings"), HttpGet]
        //public HttpResponseMessage Get()
        //{
        //    ItemsResponse<UserSettings> response = new ItemsResponse<UserSettings>();

        //    response.Items = UserService.GetSettings();
        //    return Request.CreateResponse(response);
        //}

        [Route("Attendance"), HttpPost]
        public HttpResponseMessage Add(AttendanceAddRequest model)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
            ItemResponse<Attendance> response = new ItemResponse<Attendance>();
            string userId = UserService.GetCurrentUserId();
            response.Item = _attendanceService.Add(model, userId);

            return Request.CreateResponse(response);
        }

        [Route("students"), HttpGet]
        public HttpResponseMessage Gets()
        {

            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            ItemsResponse<UserBase> response = new ItemsResponse<UserBase>();
            response.Items = _userDataService.GetByRole();
            return Request.CreateResponse(response);
        }

        [Route("onboardedList"), HttpGet]
        public HttpResponseMessage GetOnboardedStudents()
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            ItemsResponse<UserInfo> response = new ItemsResponse<UserInfo>();

            response.Items = _userDataService.GetOnboardedStudents();

            return Request.CreateResponse(response);
        }
    }
}
