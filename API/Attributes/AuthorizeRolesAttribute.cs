using Identity.Enums;
using Microsoft.AspNetCore.Authorization;

namespace API.Attributes
{
    public class AuthorizeRolesAttribute : AuthorizeAttribute
    {
        public AuthorizeRolesAttribute(params UserRolesEnum[] roles) : base()
        {
            Roles = string.Join(",", roles.Select(r => Enum.GetName(r.GetType(), r)));
        }
    }
}
