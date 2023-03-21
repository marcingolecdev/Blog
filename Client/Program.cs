using Blazored.SessionStorage;
using Blog.Client.Services;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Blog.Client
{
    public class Program
    {
        public static async Task Main (string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");


            // below are the services that this application connects to via HttpClient

            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });


            builder.Services.AddHttpClient<MessageService>(client =>
            {
                client.BaseAddress = new Uri("https://localhost:44337/");
            });

            builder.Services.AddHttpClient<PostService>(client =>
            {
                client.BaseAddress = new Uri("https://localhost:44338/");
            });

            builder.Services.AddHttpClient<CommentService>(client =>
            {
                client.BaseAddress = new Uri("https://localhost:44338/");
            });

            // Adding login services
            builder.Services.AddBlazoredSessionStorage();
            builder.Services.AddAuthorizationCore();

            // Registration of the service related to logging in DI
            builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthenticationStateProvider>();


            // Registration of services in DependencyInjection
            builder.Services.TryAddScoped(typeof(MessageService));
            builder.Services.TryAddScoped(typeof(PostService));
            builder.Services.TryAddScoped(typeof(CommentService));


            await builder.Build().RunAsync();
        }
    }
}
