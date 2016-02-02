using Sabio.Data;
using Sabio.Web.Models.Requests;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Sabio.Web.Domain.Tests;
using Sabio.Web.Models.Requests.Addresses;
using Sabio.Web.Domain;
using System.Data;
using Sabio.Web.Domain.Addresses;
using Sabio.Web.Services.Interfaces;

namespace Sabio.Web.Services
{
    public class AddressService : BaseService, IAddressService
    {
        public int Get()
        {
            return 0;
        }

        public int InsertAddress(AddressAddRequest model, string userId)
        {
            // Guid uid = Guid.Empty; 000-0000-0000-0000 >> unique identifier - huge number; hexa-decimal>> combinations of numbers and letters

            var id = 0;

            DataProvider.ExecuteNonQuery(GetConnection, "dbo.Addresses_Insert"
               , inputParamMapper: delegate(SqlParameterCollection AddressesInsert)
               {

                   AddressesInsert.AddWithValue("@Line1", model.Line1);
                   AddressesInsert.AddWithValue("@Line2", model.Line2);
                   AddressesInsert.AddWithValue("@City", model.City);
                   AddressesInsert.AddWithValue("@StateProvinceId", model.StateProvinceId);
                   AddressesInsert.AddWithValue("@ZipCode", model.ZipCode);


                   SqlParameter p = new SqlParameter("@Id", System.Data.SqlDbType.Int);
                   p.Direction = System.Data.ParameterDirection.Output;

                   AddressesInsert.Add(p);

               }, returnParameters: delegate(SqlParameterCollection param)
               {
                   int.TryParse(param["@Id"].Value.ToString(), out id);
               }
               );

            return id;  //returns just the numeric id which is the Scope Identity from SQL

        }

        public void Update(AddressUpdateRequest model) 
        {

            DataProvider.ExecuteNonQuery(GetConnection, "dbo.Addresses_Update"   
              , inputParamMapper: delegate(SqlParameterCollection AddressesUpdate)  
              {
                  AddressesUpdate.AddWithValue("@Id", model.Id);
                  AddressesUpdate.AddWithValue("@Line1", model.Line1);
                  AddressesUpdate.AddWithValue("@Line2", model.Line2);
                  AddressesUpdate.AddWithValue("@City", model.City);
                  AddressesUpdate.AddWithValue("@StateProvinceId", model.StateProvinceId);
                  AddressesUpdate.AddWithValue("@ZipCode", model.ZipCode);
              });
        }

        public void Delete(int id)
        {
            DataProvider.ExecuteNonQuery(GetConnection, "dbo.Addresses_Delete",
                inputParamMapper: delegate (SqlParameterCollection paramCollection)
                {
                    paramCollection.AddWithValue("@Id", id);

                });
        }
        
        public Address Get(int id)
        {  
            Address item = null;

            DataProvider.ExecuteCmd(GetConnection, "dbo.Addresses_SelectById"
               , inputParamMapper: delegate(SqlParameterCollection paramCollection)
               {
                   paramCollection.AddWithValue("@Id", id);
                   //model binding
               }, map: delegate(IDataReader reader, short set)
               {
                   item = MapAddress(reader);

               });
            return item;
        }

        private static Address MapAddress(IDataReader reader)
        {
            Address item = new Address();
            StateProvince sp = new StateProvince();
            int startingIndex = 0;

            item.Id = reader.GetSafeInt32(startingIndex++);
            item.Line1 = reader.GetSafeString(startingIndex++);
            item.Line2 = reader.GetSafeString(startingIndex++);
            item.City = reader.GetSafeString(startingIndex++);
            sp.StateProvinceId = reader.GetSafeInt32(startingIndex++);
            item.ZipCode = reader.GetSafeString(startingIndex++);
            sp.Name = reader.GetSafeString(startingIndex++);
            sp.StateProvinceCode = reader.GetSafeString(startingIndex++);
            sp.CountryId = reader.GetSafeInt32(startingIndex++);
            sp.CountryRegionCode = reader.GetSafeString(startingIndex++);
            sp.IsOnlyStateProvinceFlag = reader.GetSafeBool(startingIndex++);
            sp.TerritoryID = reader.GetSafeInt32(startingIndex++);
            sp.Rowguid = reader.GetGuid(startingIndex++);
            sp.ModifiedDate = reader.GetSafeDateTime(startingIndex++);
            sp.CountryName = reader.GetSafeString(startingIndex++);

            item.StateProvince = sp;
            return item;
        }

        public List<Address> GetAddressList()
        {
            List<Address> list = null;

            DataProvider.ExecuteCmd(GetConnection, "dbo.Addresses_SelectAll"
               , inputParamMapper: null
               , map: delegate(IDataReader reader, short set)
               {
                   Address item = MapAddress(reader);

                   if (list == null)
                   {
                       list = new List<Address>();
                   }

                   list.Add(item);
               });
            return list;
        }

        public List<StateProvince> GetState(int CountryId)
        {
            List<StateProvince> list = new List<StateProvince>();

            DataProvider.ExecuteCmd(GetConnection, "dbo.Countries_SelectStatesByCountry"
               , inputParamMapper: delegate (SqlParameterCollection paramCollection)
               {
                   paramCollection.AddWithValue("@CountryId", CountryId);
               }, map: delegate (IDataReader reader, short set)
               {
                   StateProvince item = new StateProvince();
                   int startingIndex = 0;

                   item.StateProvinceId = reader.GetSafeInt32(startingIndex++);
                   item.CountryId = reader.GetSafeInt32(startingIndex++);
                   item.StateProvinceCode = reader.GetSafeString(startingIndex++);
                   item.CountryRegionCode = reader.GetSafeString(startingIndex++);
                   item.IsOnlyStateProvinceFlag = reader.GetSafeBool(startingIndex++);
                   item.Name = reader.GetSafeString(startingIndex++);
                   item.TerritoryID = reader.GetSafeInt32(startingIndex++);
                   item.Rowguid = reader.GetGuid(startingIndex++);
                   item.ModifiedDate = reader.GetDateTime(startingIndex++);

                   list.Add(item);
               });
            return list;
        }

        public List<Country> GetCountries()
        {
            List<Country> list = null;

            DataProvider.ExecuteCmd(GetConnection, "dbo.Countries_SelectAll"
               , inputParamMapper: null
               , map: delegate (IDataReader reader, short set)
               {
                   Country item = new Country();
                   int startingIndex = 0;

                   item.Id = reader.GetSafeInt32(startingIndex++);
                   item.Name = reader.GetSafeString(startingIndex++);
                   item.Code = reader.GetSafeString(startingIndex++);
                   item.LongCode = reader.GetSafeString(startingIndex++);
                   item.DateAdded = reader.GetDateTime(startingIndex++);
                   item.DateModified = reader.GetDateTime(startingIndex++);

                   if (list == null)
                   {
                       list = new List<Country>();
                   }

                   list.Add(item);
               });
            return list;
        }

    }    
}


        