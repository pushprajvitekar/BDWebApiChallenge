using Microsoft.AspNetCore.Authorization;

namespace WebApi.Auth
{
    public static class PolicyName
    {
        public const string SameStudentPolicy = "SameStudentPolicy";
    }
    public class SameStudentAuthorizationHandler : AuthorizationHandler<SameStudentAuthorizationRequirement, int>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context,
                                                       SameStudentAuthorizationRequirement requirement,
                                                       int resource)
        {
            if (context.User.GetUserId() == resource)
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
    public class SameStudentAuthorizationRequirement : IAuthorizationRequirement
    {
    }
}
