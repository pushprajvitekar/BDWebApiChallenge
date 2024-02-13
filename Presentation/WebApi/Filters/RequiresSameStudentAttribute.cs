using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Diagnostics.CodeAnalysis;
using WebApi.Auth;

namespace WebApi.Filters
{
    public class RequiresSameStudentAttribute : TypeFilterAttribute
    {
        public RequiresSameStudentAttribute([StringSyntax("Route")] string template)
            : base(typeof(RequiresSameStudentAttributeImpl))
        {
            Arguments = new[] { template };
        }

        private class RequiresSameStudentAttributeImpl : Attribute, IAsyncResourceFilter
        {
            private readonly ILogger _logger;
            private readonly IAuthorizationService _authService;
            private readonly SameStudentAuthorizationRequirement _permissionRequirement;

            public RequiresSameStudentAttributeImpl(ILogger<RequiresSameStudentAttribute> logger,
                                                   IAuthorizationService authService,
                                                   SameStudentAuthorizationRequirement permissionRequirement)
            {
                _logger = logger;
                _authService = authService;
                _permissionRequirement = permissionRequirement;
            }

            public async Task OnResourceExecutionAsync(ResourceExecutingContext context,
                                                         ResourceExecutionDelegate next)
            {
                if (!await _authService.AuthorizeAsync(context.HttpContext.User,
                                            context.ActionDescriptor.ToString(),
                                            _permissionRequirement))
                {
                    context.Result = new ChallengeResult();
                }

                await next();
            }

            public Task OnResourceExecutionAsync(ResourceExecutingContext context, ResourceExecutionDelegate next)
            {
                throw new NotImplementedException();
            }
        }
    }
}
