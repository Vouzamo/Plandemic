using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using Plandemic.Common.Models.Multitenancy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Plandemic.App
{
    public class TenantAccessor : ITenantAccessor
    {
        protected IHttpContextAccessor HttpContextAccessor { get; }

        public TenantAccessor(IHttpContextAccessor httpContextAccessor)
        {
            HttpContextAccessor = httpContextAccessor;
        }

        public Guid GetTenantId()
        {
            var httpContext = HttpContextAccessor.HttpContext;

            if(httpContext.Request.Headers.TryGetValue("Tenant", out StringValues value))
            {
                if(Guid.TryParse(value, out Guid id))
                {
                    return id;
                }
            }

            throw new Exception("Couldn't retrieve tenant id from Tenant header");
        }
    }
}
