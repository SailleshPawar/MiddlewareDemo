using Microsoft.AspNetCore.Builder;
using WebAPI.Controllers;

namespace WebAPI.Middleware
{
    public static class AuthorizationMiddleware
    {

        public static void UseAuthorizationMiddleware(this IApplicationBuilder builder)
        {
            builder.UseMiddleware<Authorize>();
        }


    }
}
