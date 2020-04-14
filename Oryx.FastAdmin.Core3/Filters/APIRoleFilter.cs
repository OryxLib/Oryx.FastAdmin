using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Caching.Distributed;
using Oryx.Authentication.Business;
using Oryx.FastAdmin.Core.ViewModel;
using Oryx.Utilities.Redis;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Oryx.FastAdmin.Filters
{
    public class APIRoleFilter : TypeFilterAttribute
    {
        public APIRoleFilter(params string[] _targetRoleList) : base(typeof(APIRoleFilterImp))
        {
            Arguments = new object[] { _targetRoleList }; ;
        }
        private class APIRoleFilterImp : Attribute, IAsyncActionFilter
        {
            private readonly IDistributedCache distributedCache;
            private readonly UserAuthBusiness userAccountBusiness;
            private readonly string[] targetRoleList;
            public APIRoleFilterImp(IDistributedCache _distributedCache,
                UserAuthBusiness _userAccountBusiness,
                string[] _targetRoles)
            {
                distributedCache = _distributedCache;
                userAccountBusiness = _userAccountBusiness;
                targetRoleList = _targetRoles;
            }

            public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
            {
                var accessToken = context.HttpContext.Request.Headers["AccessToken"];
                var identity = new System.Security.Claims.ClaimsIdentity();

                if (!string.IsNullOrEmpty(accessToken))
                {
                    var resultDocument = await distributedCache.GetValue<string>(accessToken);
                    if (resultDocument.ExpireTime > DateTime.Now)
                    {
                        if (!string.IsNullOrEmpty(resultDocument.Value))
                        {
                            //var identity = new System.Security.Claims.ClaimsIdentity();
                            identity.AddClaim(new System.Security.Claims.Claim("OryxUser", resultDocument.Value));
                            context.HttpContext.User.AddIdentity(identity);
                            var roles = await userAccountBusiness.GetRoles(resultDocument.Value);
                            if (roles.Any(x => targetRoleList.Any(c => c == x)))
                            {
                                await next();
                            }
                            else
                            {
                                context.Result = new JsonResult(new ApiMessage
                                {
                                    Success = false,
                                    Message = "Not in role",
                                    Code = "503"
                                });
                            }
                        }
                        else
                        {
                            context.Result = new JsonResult(new ApiMessage
                            {
                                Success = false,
                                Message = "Permission Empty",
                                Code = "503"
                            });
                        }
                    }
                    else
                    {
                        context.Result = new JsonResult(new ApiMessage
                        {
                            Success = false,
                            Message = "Permission Expired",
                            Code = "503"
                        });
                    }
                }
                else
                {
                    context.Result = new JsonResult(new ApiMessage
                    {
                        Success = false,
                        Message = "Permission denied",
                        Code = "503"
                    });
                }
            }
        }
    }
}
