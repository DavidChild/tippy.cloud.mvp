using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bitter.Base;
using AntDesign;
namespace tippy.cloud.Client
{
    public static class BzeroDo
    {
        public  static async void ActionByObject<Tin>(this Result<Tin> result,Action<Tin> action, MessageService message) where Tin : class,new()
        {
            if (result.code == 0 && message != null)
            {
                if (result.message == "request error.")
                {
                    await message.Error(result.message);
                }
                else
                {
                    await message.Warn(result.message);
                }
                return;
            }
            if (action != null)
            {
                action(result.@object);
            }
         
        }

        public static async void ActionByResult<Tin>(this Result<Tin> result, Action<Result<Tin>> action, MessageService message) where Tin : class, new()
        {
            if (result.code == 0 && message != null)
            {
                if (result.message == "request error.")
                {
                    await message.Error(result.message);
                }
                else
                {
                    await message.Warn(result.message);
                }

                return;
            }
            if (action != null)
            {
                action(result);
            }

        }

    }
}
