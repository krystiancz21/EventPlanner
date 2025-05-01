using Microsoft.AspNetCore.Authorization;

namespace EventPlanner.Infrastructure.Authorization.Requirements;
public class MinimumAgeRequirement(int minimumAge) : IAuthorizationRequirement
{
    public int MinimumAge { get; } = minimumAge;
}

