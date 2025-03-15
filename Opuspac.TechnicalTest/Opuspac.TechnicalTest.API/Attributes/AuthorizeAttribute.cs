using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Opuspac.TechnicalTest.Application.DTOs;
using Opuspac.TechnicalTest.Application.Interfaces;

namespace Opuspac.TechnicalTest.API.Attributes
{
    public class AuthorizeAttribute : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            string headerAuthorization = context.HttpContext.Request.Headers.Authorization.ToString();

            if (string.IsNullOrWhiteSpace(headerAuthorization) || !headerAuthorization.StartsWith("Bearer ", StringComparison.InvariantCultureIgnoreCase))
            {
                context.Result = new UnauthorizedResult();
            }
            else
            {
                try
                {
                    string token = headerAuthorization.Substring(7);
                    ITokenService tokenService = (ITokenService)context.HttpContext.RequestServices.GetRequiredService(typeof(ITokenService));
                    UserDTO userDTO = tokenService.ValidateToken(token);
                    context.HttpContext.Items.Add("User", userDTO);
                }
                catch (Exception)
                {
                    context.Result = new UnauthorizedResult();
                }
            }
        }
    }
}
