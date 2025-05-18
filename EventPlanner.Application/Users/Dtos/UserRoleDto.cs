namespace EventPlanner.Application.Users.Dtos;

public record UserRoleDto(string UserId, string Email, IEnumerable<string> Roles);
