using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using BlazorrWASM;
using BlazorrWASM.Auth;
using BlazorrWASM.Services;
using BlazorrWASM.Services.HTTP;
using HttpClients.ClientInterfaces;
using HttpClients.Implementations;
using Microsoft.AspNetCore.Components.Authorization;
using Shared.Auth;

var builder = WebAssemblyHostBuilder.CreateDefault(args);


builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");
builder.Services.AddScoped<IAuthService, JwtAuthService>();
builder.Services.AddScoped<IUserService, UserHttpClient>();
builder.Services.AddScoped<IProjectService,ProjectHttpClient>();
builder.Services.AddScoped<IUserStoryService,UserStoryHttpClient>();
builder.Services.AddScoped<ISprintService,SprintHttpClient>();

builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthProvider>();
AuthorizationPolicies.AddPolicies(builder.Services);
builder.Services.AddScoped(
    sp => 
        new HttpClient { 
            BaseAddress = new Uri("https://localhost:7203") 
        }
);
builder.Services.AddAuthorizationCore();

await builder.Build().RunAsync();