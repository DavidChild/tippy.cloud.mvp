using AntDesign;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using tippy.cloud.Client;
using tippy.cloud.Models;
using tippy.cloud.Shared;

namespace tippy.cloud.Services
{
    
    public interface IUserService
    {
        Task<CurrentUser> GetCurrentUserAsync();
    }

    public class UserService : IUserService
    {
        private readonly HttpClient _httpClient;

        private readonly IMyHttp http;
        private readonly MessageService message;
        private readonly IAuthService AuthService;
        private readonly  NavigationManager navigation;

        public UserService(HttpClient httpClient, IMyHttp http, IAuthService authService, NavigationManager navigation, MessageService message)
        {
            _httpClient = httpClient;
            this.message = message;
            this.http = http;
            this.AuthService = authService;
            this.navigation = navigation;
        }

        public async Task<CurrentUser> GetCurrentUserAsync()
        {
               CurrentUser current = new CurrentUser();
               await http.Get<CurrentUser>("/tippy/Frame/GetTenantUserInfo", null,
               (result) =>
               {
                   result.ActionByObject((e) =>
                       {
                            current=e;

                   }, message);
               });
            return current;

        }
    }
}