using System.Net;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using Shared.DTOs;
using Shared.Model;

namespace BlazorrWASM.Services.HTTP;

public class JwtAuthService : IAuthService
{
    private readonly HttpClient _client = new();

    private static string? Jwt { get; set; } = "";

    public Action<ClaimsPrincipal> OnAuthStateChanged { get; set; } = null!;


    public async Task LoginAsync(string username, string password)
    {
        LoginDto userLoginDto = new()
        {
            Username = username,
            Password = password
        };

        var userAsJson = JsonSerializer.Serialize(userLoginDto);
        StringContent content = new(userAsJson, Encoding.UTF8, "application/json");

        var response = await _client.PostAsync("https://localhost:7203/auth/login", content);
        var responseContent = await response.Content.ReadAsStringAsync();
        Console.WriteLine(response.StatusCode);

        if (!response.IsSuccessStatusCode) throw new Exception(responseContent);


        var token = responseContent;
        Jwt = token;

        var principal = CreateClaimsPrincipal();

        OnAuthStateChanged.Invoke(principal);
    }

    public Task LogoutAsync()
    {
        Jwt = null;
        ClaimsPrincipal principal = new();
        OnAuthStateChanged.Invoke(principal);
        return Task.CompletedTask;
    }

    public async Task RegisterAsync(string username, string firstname, string lastname, string password, string role)
    {
        User user = new()
        {
            Username = username,
            Password = password,
            FirstName = firstname,
            LastName = lastname,
            Role = role
        };

        var userAsJson = JsonSerializer.Serialize(user);
        StringContent content = new(userAsJson, Encoding.UTF8, "application/json");
        var response = await _client.PostAsync("https://localhost:7203/auth/register", content);
        var responseContent = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode) throw new Exception(responseContent);
    }

    public Task<ClaimsPrincipal> GetAuthAsync()
    {
        var principal = CreateClaimsPrincipal();
        return Task.FromResult(principal);
    }

    private static IEnumerable<Claim> ParseClaimsFromJwt(string jwt)
    {
        var payload = jwt.Split('.')[1];
        var jsonBytes = ParseBase64WithoutPadding(payload);
        Dictionary<string, object>? keyValuePairs = JsonSerializer.Deserialize<Dictionary<string, object>>(jsonBytes);
        return keyValuePairs!.Select(kvp => new Claim(kvp.Key, kvp.Value.ToString()!));
    }

    private static byte[] ParseBase64WithoutPadding(string base64)
    {
        switch (base64.Length % 4)
        {
            case 2:
                base64 += "==";
                break;
            case 3:
                base64 += "=";
                break;
        }

        return Convert.FromBase64String(base64);
    }

    private static ClaimsPrincipal CreateClaimsPrincipal()
    {
        if (string.IsNullOrEmpty(Jwt)) return new ClaimsPrincipal();

        IEnumerable<Claim> claims = ParseClaimsFromJwt(Jwt);

        ClaimsIdentity identity = new(claims, "jwt");

        ClaimsPrincipal principal = new(identity);
        return principal;
    }
}