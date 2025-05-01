using Microsoft.AspNetCore.Authorization;

namespace EventPlanner.Infrastructure.Authorization.Requirements;
public class CreatedMultipleEventRequirement(int minimumEventsCreated) : IAuthorizationRequirement
{
    public int MinimumEventsCreated { get; set; } = minimumEventsCreated;
}

