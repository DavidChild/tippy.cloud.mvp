using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Bitter.Base;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using tippy.cloud.Shared;

namespace tippy.cloud.Client
{
  
    internal class AuthService : IAuthService
    {
     
        private readonly AuthenticationStateProvider _authenticationStateProvider;
        private readonly ILocalStorageService _localStorage;
        private readonly IConfiguration _config;
        private readonly ILocalStorage _mylocalstrage;
        private readonly IMyHttp myHttp;
        public AuthService(HttpClient httpClient,
                           AuthenticationStateProvider authenticationStateProvider,
                           ILocalStorageService localStorage, IConfiguration config, ILocalStorage mylocalstrage)
        {
         
            _authenticationStateProvider = authenticationStateProvider;
            _localStorage = localStorage;
            _config = config;
           _mylocalstrage= mylocalstrage;
        }

        public async Task<Result<LoginDto_Out>> Login(LoginDto rqtDto)
        {
            try
            {


                using (HttpClient _httpClient = new HttpClient())
                {
                    var content = new FormUrlEncodedContent(new KeyValuePair<string, string>[]
                        {
                        new(nameof(LoginDto.Account), rqtDto.Account),
                        new(nameof(LoginDto.Password), rqtDto.Password),
                        });
                    await SetRequestHeader.SetHttpHead(_httpClient, _config, _mylocalstrage);
                    var rsp = await _httpClient.PostAsJsonAsync<LoginDto>("/tippy/accountchecked/login", rqtDto);

                    if (!rsp.IsSuccessStatusCode)
                    {
                        return new Result<LoginDto_Out>() { code = 0, message = "request error;" };
                    }
                    Result<LoginDto_Out> authuser = await rsp.Content.ReadFromJsonAsync<Result<LoginDto_Out>>();
                    if (authuser.code == 1)
                    {
                        await _localStorage.SetItemAsync(GloableSettig.GetGloableSettigInsance(_config).authToken, authuser.@object.authtoken);
                        ((ApiAuthenticationStateProvider)_authenticationStateProvider).MarkUserAsAuthenticated(rqtDto.Account);
                        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(tippy.cloud.Shared.RequestHeadDefine.head_accesstocken, authuser.@object.authtoken);
                    }
                    return authuser;
                }
            }
            catch (Exception ex)
            {
                return new Result<LoginDto_Out>() { code=0,message=""};
            }



        }
        public async Task Logout()
        {
            await _localStorage.RemoveItemAsync(GloableSettig.GetGloableSettigInsance(_config).authToken);
            ((ApiAuthenticationStateProvider)_authenticationStateProvider).MarkUserAsLoggedOut();
        }
    }

}
