using Microsoft.AspNetCore.Authorization;

namespace Plandemic.App.Authorization
{
    public class UserGroupRequirement : IAuthorizationRequirement
    {
        public string Group { get; }

        public UserGroupRequirement(string group)
        {
            Group = group;
        }
    }
}
