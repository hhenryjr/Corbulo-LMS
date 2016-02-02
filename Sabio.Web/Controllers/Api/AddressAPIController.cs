using Sabio.Web.Domain;
using Sabio.Web.Domain.Addresses;
using Sabio.Web.Models;
using Sabio.Web.Models.Requests;
using Sabio.Web.Models.Requests.Addresses;
using Sabio.Web.Models.Responses;
using Sabio.Web.Services;
using Sabio.Web.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Sabio.Web.Controllers.Api
{
    [RoutePrefix("api/Addresses")] //URL 
    public class AddressAPIController : ApiController
    {
        private IAddressService _addressService;
        //private IUserService _userService;

        public AddressAPIController(IAddressService addressService/*, IUserService UserService*/)
        {
            _addressService = addressService;
           // _userService = UserService;
        }

        [Route, HttpPost]
        public HttpResponseMessage AddAddress(AddressAddRequest model)
        {
            // if the Model does not pass validation, there will be an Error response returned with errors
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
            ItemResponse<int> response = new ItemResponse<int>(); //give a domain object //item = Post it in id from proc
            string userId = UserService.GetCurrentUserId();
            response.Item = _addressService.InsertAddress(model, userId); //inserting from model into database

            return Request.CreateResponse(response);

        }

        [Route("{id:int}"), HttpPut]   //its taking my URL/Ajax/settings into the route  //HTTP Put is the only request at this point       
        public HttpResponseMessage UpdateAddresses(AddressUpdateRequest model, int id)   //creating a name for HTTPResponseMessage >> UpdateAddresses
        {                                                                                //in the parameters includes the model and id >> it is being passed as a numeric id

            if (!ModelState.IsValid)  //if my data is NOT valid 
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState); //it will return as error response
            }

            SucessResponse response = new SucessResponse();
            _addressService.Update(model);                    //packaging data from your database
            return Request.CreateResponse(response);           //and sends it right back to your Ajax becasue the functions are there 

        }

        [Route("{id:int}"), HttpGet]
        public HttpResponseMessage Get(int id)
        {
            ItemResponse<Address> response = new ItemResponse<Address>();

            response.Item = _addressService.Get(id);

            return Request.CreateResponse(response);
        }

        [Route, HttpGet]
        public HttpResponseMessage GetAddressList()
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            ItemsResponse<Address> response = new ItemsResponse<Address>();

            response.Items = _addressService.GetAddressList();

            return Request.CreateResponse(response);

        }

        [Route("{id:int}"), HttpDelete]
        public HttpResponseMessage DeleteAddress(int id)
        {

            SucessResponse response = new SucessResponse();
            _addressService.Delete(id);

            return Request.CreateResponse(response);
        }

        [Route("Countries"), HttpGet]
        public HttpResponseMessage GetCountries()
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            ItemsResponse<Country> response = new ItemsResponse<Country>();

            response.Items = _addressService.GetCountries();

            return Request.CreateResponse(response);
        }

        [Route("States/{CountryId:int}"), HttpGet]
        public HttpResponseMessage GetState(int CountryId)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            ItemsResponse<StateProvince> response = new ItemsResponse<StateProvince>();

            response.Items = _addressService.GetState(CountryId);

            return Request.CreateResponse(response);
        }

    }
}







