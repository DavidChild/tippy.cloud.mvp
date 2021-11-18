using AntDesign;
using Microsoft.AspNetCore.Components;
using System.ComponentModel.DataAnnotations;
using tippy.cloud.Client;
using tippy.cloud.Shared.Domain;
using tippy.cloud.Shared;
namespace tippy.cloud.Pages.User
{
    

    public partial class Register
    {
        private readonly RegisterModel _user = new RegisterModel();
        [Inject] IMyHttp http { get; set; }
        [Inject] MessageService message { get; set; }
        [Inject] IAuthService AuthService { get; set; }
        [Inject] public NavigationManager navigation { get; set; }
        public void Reg()
        {
             http.Post<RegisterModel,object>("/tippy/accountchecked/joinnow", _user,
                (result) =>
                {
                    result.ActionByObject((e) =>
                    {
                        navigation.NavigateTo("/user/login");

                    }, message);
                });
        }
    }
}