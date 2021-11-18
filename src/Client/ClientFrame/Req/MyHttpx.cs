using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;
using Bitter.Base;
using Bitter.Tools.Utils;
using Blazored.LocalStorage;
using Newtonsoft.Json;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions;
using AntDesign;
using Microsoft.AspNetCore.Components;
using System.Net;
namespace tippy.cloud.Client
{
    public class MyHttp : IMyHttp
    {


        private readonly ILocalStorage _localStorage;
        private readonly IConfiguration _config;

        private readonly NavigationManager _navigationManager;
        public MyHttp(ILocalStorage localStorage, IConfiguration config, NavigationManager navigationManager)
        {
            _localStorage = localStorage;
            _config = config;
            _navigationManager = navigationManager;
        }


        /// <summary>
        /// POST 请求
        /// </summary>
        /// <typeparam name="T">请求入参类型</typeparam>
        /// <typeparam name="Tout">请求出参类型</typeparam>
        /// <param name="uri">请求URI</param>
        /// <param name="value">请求入参实例</param>
        /// <param name="callback">回调函数</param>
        public async Task<bool> Post<T, Tout>(string uri, T value, Action<Result<Tout>> callback) where T : class, new() where Tout : class, new()
        {
            Result<Tout> result = new Result<Tout>();
           
            try
            {
                using (HttpClient _httpClient = new HttpClient())
                {
                    await SetRequestHeader.SetHttpHead(_httpClient,_config,_localStorage);
                    var rsp = await _httpClient.PostAsJsonAsync<T>(uri, value);
                    if (rsp.IsSuccessStatusCode)
                    {
                        if ((int)rsp.StatusCode == 401)
                        {
                            _navigationManager.NavigateTo("/user/login");
                        }
                        result = await rsp.Content.ReadFromJsonAsync<Result<Tout>>();
                    }
                    else
                    {
                        result.code = 0;
                        result.message = "request error";
                    }

                }


            }
            catch (HttpRequestException ex)
            {
                if (ex.Message.Contains(HttpStatusCode.Unauthorized.ToString()))
                {
                    _navigationManager.NavigateTo("/user/login");
                    return false;

                }
                result.code = 0;
                result.message = $"{ex.Message}";
            }

            if (callback != null)
            {
                callback(result);
            }
            return true;


        }

        /// <summary>
        /// GET 请求
        /// </summary>
        /// <typeparam name="Tout">输出类型</typeparam>
        /// <param name="uri">请求URI</param>
        /// <param name="dyParames">参数</param>
        /// <param name="callback">回调函数</param>
        public async Task<bool> Get<Tout>(string uri, dynamic dyParames, Action<Result<Tout>> callback) where Tout : class, new()
        {

            Result<Tout> result = new Result<Tout>();
         
            try
            {
                using (HttpClient _httpClient = new HttpClient())
                {
                  await SetRequestHeader.SetHttpHead(_httpClient,_config,_localStorage);
                    var info = JsonConvert.SerializeObject(dyParames);
                    Dictionary<string, string> dict = JsonConvert.DeserializeObject<Dictionary<string, string>>(info);
                    if (dict != null && dict.Count() > 0)
                    {
                        uri += "?";
                        foreach (KeyValuePair<string, string> pair in dict)
                        {
                            uri += $"{pair.Key}={pair.Value.ToSafeString()}&";
                        }
                        uri = uri.Remove(uri.Length - 1);
                    }



                    result = await _httpClient.GetFromJsonAsync<Result<Tout>>(uri);


                }
            }
            catch (HttpRequestException ex)
            {
                if (ex.Message.Contains(HttpStatusCode.Unauthorized.ToString()))
                {
                    _navigationManager.NavigateTo("/user/login");
                    return false;
                }
                Console.WriteLine("请求错误:" + ex.Message);
                result.code = 0;
                result.message = "request error.";

            }
            if (callback != null)
            {
                callback(result);
            }
            return true;
        }

      

    }
}
