

using Sabio.Web.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;

namespace Sabio.Web.Core.Filters
{
    public class EntityAuthAttribute : BaseAuthActionFilterAttribute
    {
        [Microsoft.Practices.Unity.Dependency]
        public IIdentityProvider<string> IdentityProvider { get; set; }


        [Microsoft.Practices.Unity.Dependency]
        public ISecureEntities<string> ISecureEntities { get; set; }

        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            //keys >> model for a Model
            //in params the params are named
            //in route?
            if (actionContext != null && actionContext.ActionArguments != null)
            {
                if(!ValidateArguments(actionContext.ActionArguments))
                {
                    _HandleUnauthorizedRequest(actionContext);
                }
            }


        }

        private bool ValidateArguments(Dictionary<string, object> actionArguments)
        {
            IModelIdentifier requestModel = null;
            object id = null;
            string userId = this.IdentityProvider.GetCurrentUserId();
            string idField = this.EntityIdField ?? "id";

            if (actionArguments.ContainsKey("model"))
            {
                requestModel = actionArguments["model"] as IModelIdentifier;
                if (requestModel != null)
                {
                    id = requestModel.Id;
                }
            }
            else if (actionArguments.ContainsKey(idField))
            {
                id = actionArguments[idField];
            }

            return this.ISecureEntities.IsAuthorized(userId, id, this.Action, this.EntityTypeId);
          


        }

        private void _HandleUnauthorizedRequest(HttpActionContext actionContext)
        {
            HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.Unauthorized);

            response.ReasonPhrase = "Request denied by Application, please try again";

            throw new HttpResponseException(response);
        }
    }
}