using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blazored.LocalStorage;
namespace tippy.cloud.Client
{
    public class LocalStorage : ILocalStorage
    {
        /// <summary>
        /// 操作LocalStorage帮助类
        /// </summary>
        private readonly ILocalStorageService _localStorage;
        public LocalStorage(ILocalStorageService localStorage)
        {
            _localStorage = localStorage;
        }
        /// <summary>
        /// 设置LocalStorage
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public override async Task SetLocalStorage<T>(string key, T value) where T : class
        {
            await _localStorage.SetItemAsync(key, value);
        }
        /// <summary>
        /// 获取LocalStorage
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public override async Task<T> GetLocalStorage<T>(string key)
        {
            return await _localStorage.GetItemAsync<T>(key);
        }
      

    }

}
