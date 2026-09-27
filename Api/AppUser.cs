using Microsoft.AspNetCore.Identity;

namespace Api;

public class AppUser(string firstName, string lastName, DateOnly dateOfBirth) : IdentityUser
{
    public string FirstName { get; set; } = firstName;
    public string LastName { get; set; } = lastName;
    public DateOnly DateOfBirth { get; set; } = dateOfBirth;
}
