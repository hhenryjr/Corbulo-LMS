using Sabio.Web.Domain.Addresses;
using Sabio.Web.Models.Requests.Addresses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sabio.Web.Services.Interfaces
{
    public interface IAddressService
    {
        int InsertAddress(AddressAddRequest model, string userId);

        void Update(AddressUpdateRequest model);

        void Delete(int id);

        Address Get(int id);

        List<Address> GetAddressList();

        List<StateProvince> GetState(int CountryId);

        List<Country> GetCountries();
    }
}