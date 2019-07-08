using System;

namespace Plandemic.Common.Models.Multitenancy
{
    public interface ITenantAccessor
    {
        Guid GetTenantId();
    }
}
