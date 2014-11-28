using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http.Filters;
using System.Web.Http.Controllers;

using Events.Infrastructure;

namespace Events.Filters
{
    [AttributeUsage(AttributeTargets.Method, Inherited = true)]
    public class CheckOptionalIntArgAttribute : ActionFilterAttribute
    {
        private readonly Func<Dictionary<string, object>, bool> _validate;
        public CheckOptionalIntArgAttribute(params string[] pParams)
        {

            _validate =
                arguments => pParams.Select(p =>
                {
                    int tmp;
                    return arguments[p] == null || Int32.TryParse(arguments[p].ToString(), out tmp);
                }).All(boolVal => boolVal);
        }
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            if (!_validate(actionContext.ActionArguments))
            {
                actionContext.Response = actionContext.Request.CreateErrorResponse(HttpStatusCode.BadRequest,
                    Messages.Get("INVALID_DOUBLE"));
            }
        }
    }
}