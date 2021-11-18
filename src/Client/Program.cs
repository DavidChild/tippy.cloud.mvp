using System;
using System.Net.Http;
using System.Threading.Tasks;
using AntDesign.ProLayout;
using tippy.cloud.Services;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Blazored.LocalStorage;
using System.Text.Json;

namespace tippy.cloud.Client
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");

            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
            builder.Services.AddAntDesign();
            builder.Services.Configure<ProSettings>(builder.Configuration.GetSection("ProSettings"));
            //builder.Services.Configure<ProSettings>(x =>
            //{
            //    x.Title = "Tippy";
            //    x.NavTheme = "realDark";
            //    x.Layout = "mix";
            //    x.PrimaryColor = "daybreak";
            //    x.ContentWidth = "Fluid";
            //    x.HeaderHeight = 64;
            //    x.
            //});
            builder.Services.AddScoped<IChartService, ChartService>();
            builder.Services.AddScoped<IProjectService, ProjectService>();
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<IAccountService, AccountService>();
            builder.Services.AddScoped<IProfileService, ProfileService>();
            //LocalStorage 
            builder.Services.AddBlazoredLocalStorage(config =>
            {
                config.JsonSerializerOptions.DictionaryKeyPolicy = JsonNamingPolicy.CamelCase;
                config.JsonSerializerOptions.IgnoreNullValues = true;
                config.JsonSerializerOptions.IgnoreReadOnlyProperties = true;
                config.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
                config.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
                config.JsonSerializerOptions.ReadCommentHandling = JsonCommentHandling.Skip;
                config.JsonSerializerOptions.WriteIndented = false;
            });
            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

            builder.Services.AddAuth();
            await builder.Build().RunAsync();
        }
    }
}