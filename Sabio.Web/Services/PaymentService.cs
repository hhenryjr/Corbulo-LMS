using Sabio.Data;
using Sabio.Web.Models.Requests.Payment;
using System.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Sabio.Web.Services.Interfaces;

namespace Sabio.Web.Services
{
    public class PaymentService : BaseService , IPaymentService
    {

        public int Add(PaymentRequest model,string userId)
        {
            int PaymentId = 0;
            

            DataProvider.ExecuteNonQuery(GetConnection, "dbo.Payment_Insert",
            inputParamMapper: delegate (SqlParameterCollection para)
            {
                para.AddWithValue("@StripeId", model.Id); 
                para.AddWithValue("@UserId", userId);
                para.AddWithValue("@Object", model.Object);
                para.AddWithValue("@Used", model.Used);
                para.AddWithValue("@LiveMode", model.LiveMode);
                para.AddWithValue("@Created", model.Created);
              
                SqlParameter p = new SqlParameter("@PaymentId", System.Data.SqlDbType.Int);
                p.Direction = System.Data.ParameterDirection.Output;

                para.Add(p);

            }, returnParameters: delegate (SqlParameterCollection param)
            {
                int.TryParse(param["@PaymentId"].Value.ToString(), out PaymentId);
            });
            if (model.Cards != null)
            {
                foreach (var item in model.Cards)
                    DataProvider.ExecuteNonQuery(GetConnection, "dbo.StripeCard_Insert"
                        , inputParamMapper: delegate (SqlParameterCollection param)
                        {
                            param.AddWithValue("@Id", item.Id);
                            param.AddWithValue("@Last4", item.Last4);
                            param.AddWithValue("@Brand", item.Brand);
                            param.AddWithValue("@Country", item.Country);
                            param.AddWithValue("@ExpMonth", item.Exp_Month);
                            param.AddWithValue("@ExpYear", item.Exp_Year);
                            param.AddWithValue("@ExpYear", item.Exp_Year);
                            param.AddWithValue("@Funding", item.Funding);
                            param.AddWithValue("@Object", item.Object);
                            param.AddWithValue("@Name", item.Name);

                        });
            }
            return PaymentId;

        }
    }
}