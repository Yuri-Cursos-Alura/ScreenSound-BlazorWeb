using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using ScreenSound.Web;
using ScreenSound.Web.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddTransient<ArtistaAPI>();

builder.Services.AddHttpClient(ArtistaAPI.HttpClientName, c =>
{
    c.BaseAddress = new Uri(builder.Configuration["APIServer:Url"]!);
    c.DefaultRequestHeaders.Add("Accept", "application/json");
});

await builder.Build().RunAsync();
