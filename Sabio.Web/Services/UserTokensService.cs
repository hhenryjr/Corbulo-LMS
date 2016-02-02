using Sabio.Data;
using Sabio.Web.Domain;
using Sabio.Web.Models.Requests.UserTokens;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Sabio.Web.Services
{
    public class UserTokensService : BaseService
    {
        public static Guid Add(UserTokensAddRequest model)
        {
            Guid TokenId = new Guid(); //asking for id number(huge) 

            DataProvider.ExecuteNonQuery(GetConnection, "dbo.UserTokens_Insert"
                , inputParamMapper: delegate(SqlParameterCollection paramCollection)
                { 
                    paramCollection.AddWithValue("@UserName", model.UserName);
                    paramCollection.AddWithValue("@TokenType", model.TokenType);

                    SqlParameter p = new SqlParameter("@TokenId", System.Data.SqlDbType.UniqueIdentifier);
                    p.Direction = System.Data.ParameterDirection.Output;

                    paramCollection.Add(p);
                }, returnParameters: delegate(SqlParameterCollection param)
                {
                    TokenId = Guid.Parse(param["@TokenId"].Value.ToString());
                }

            );
            return TokenId;
        }

        public static Token Get(Guid id)
        {
            Token Item = null;

            DataProvider.ExecuteCmd(GetConnection, "dbo.UserTokens_SelectUserIdByToken"
               , inputParamMapper: delegate(SqlParameterCollection paramCollection)
               {
                   paramCollection.AddWithValue("@TokenId", id);
               }, map: delegate(IDataReader reader, short set)
               {
                   Item = new Token();
                   int startingIndex = 0; //startingOrdinal

                   Item.TokenId = reader.GetSafeGuidNullable(startingIndex++);
                   Item.UserName = reader.GetString(startingIndex++);
                   Item.TokenType = reader.GetSafeInt32(startingIndex++);
                   Item.DateAdded = reader.GetDateTime(startingIndex++);
                   Item.DateModified = reader.GetDateTime(startingIndex++);
               }
               );
            return Item;

        }

        public static void Delete(Guid id)
        {
            DataProvider.ExecuteNonQuery(GetConnection, "dbo.UserTokens_DeleteUserIdByToken",
                inputParamMapper: delegate(SqlParameterCollection paramCollection)
                {
                    paramCollection.AddWithValue("@TokenId", id);

                });
        }

        public static bool IsValid(Guid id)
        {
            bool exist = false;

            DataProvider.ExecuteNonQuery(GetConnection, "dbo.UserTokens_ExistsByToken"
               , inputParamMapper: delegate(SqlParameterCollection paramCollection)
               {
                   paramCollection.AddWithValue("@TokenId", id);
                   SqlParameter p = new SqlParameter("@Exists", System.Data.SqlDbType.Bit);
                   p.Direction = System.Data.ParameterDirection.Output;

                   paramCollection.Add(p);
               },
               returnParameters: delegate(SqlParameterCollection param)
               {
                   bool.TryParse(param["@Exists"].Value.ToString(), out exist);
               }
               );
            return exist;
        } 
    }
}