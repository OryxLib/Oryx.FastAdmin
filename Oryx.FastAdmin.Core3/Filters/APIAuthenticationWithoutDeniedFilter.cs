using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Caching.Distributed;
using Oryx.Authentication.Business;
using Oryx.Utilities.Redis;
using System;
using System.Threading.Tasks;

namespace Oryx.FastAdmin.Filters
{
    public class APIAuthenticationWithoutDeniedFilter : Attribute, IAsyncActionFilter
    {
        private readonly IDistributedCache distributedCache;
        private readonly UserAuthBusiness userAccountBusiness;
        public APIAuthenticationWithoutDeniedFilter(IDistributedCache _distributedCache,
            UserAuthBusiness _userAccountBusiness)
        {
            distributedCache = _distributedCache;
            userAccountBusiness = _userAccountBusiness;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var accessToken = context.HttpContext.Request.Headers["AccessToken"];
            if (!string.IsNullOrEmpty(accessToken))
            {
                var resultDocument = await distributedCache.GetValue<string>(accessToken);
                if (resultDocument.ExpireTime > DateTime.Now)
                {
                    if (!string.IsNullOrEmpty(resultDocument.Value))
                    {
                        var identity = new System.Security.Claims.ClaimsIdentity();
                        identity.AddClaim(new System.Security.Claims.Claim("OryxUser", resultDocument.Value));
                        context.HttpContext.User.AddIdentity(identity); 
                    } 
                } 
            }

            await next();
        }
    }
}
