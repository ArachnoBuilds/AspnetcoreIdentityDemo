using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;

namespace Api;

internal static class Endpoint
{
    internal static async Task<IResult> SignUpAsync(
        [FromBody] SignInRequest request,
        [FromServices] UserManager<AppUser> userManager)
    {
        if (!DateOnly.TryParseExact(request.DateOfBirth, "dd-MM-yyyy", out var dob))
            return Results.BadRequest("Invalid date format. Date should be specified as dd-MM-yyyy");
        AppUser appUser = new(request.FirstName, request.LastName, dob)
        {
            UserName = request.Email,
            Email = request.Email,
        };
        var result = await userManager.CreateAsync(appUser, request.Password).ConfigureAwait(false);
        if (!result.Succeeded)
            return Results.BadRequest(result.Errors);
        return Results.Ok();
    }
}

public record SignInRequest(string FirstName, string LastName, string DateOfBirth, string Email, string Password);