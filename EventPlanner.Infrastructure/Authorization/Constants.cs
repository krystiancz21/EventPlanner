namespace EventPlanner.Infrastructure.Authorization;

public class PolicyNames
{
    public const string HasNationality = "HasNationality";
    public const string AtLeast18 = "AtLeast18";
    public const string CreatedAtLeast2Events = "CreatedAtLeast2Events";
}

public class AppClaimTypes
{
    public const string Nationality = "Nationality";
    public const string DateOfBirth = "DateOfBirth";
}

