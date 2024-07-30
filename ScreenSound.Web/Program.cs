using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;
using ScreenSound.Web;
using ScreenSound.Web.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddMudServices();

builder.Services.AddTransient<ArtistaAPI>();
builder.Services.AddTransient<GeneroAPI>();
builder.Services.AddTransient<MusicaAPI>();

builder.Services.AddHttpClient("API", c =>
{
    c.BaseAddress = new Uri(builder.Configuration["APIServer:Url"]!);
    c.DefaultRequestHeaders.Add("Accept", "application/json");
});

await builder.Build().RunAsync();
