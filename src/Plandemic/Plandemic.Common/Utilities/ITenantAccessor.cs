using System;

namespace Plandemic.Common.Utilities
{
    public interface ITenantAccessor
    {
        Guid GetTenantId();
    }
}
