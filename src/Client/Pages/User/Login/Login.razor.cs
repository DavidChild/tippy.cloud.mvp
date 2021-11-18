using System.Threading.Tasks;
using tippy.cloud.Models;
using tippy.cloud.Services;
using Microsoft.AspNetCore.Components;
using AntDesign;
using tippy.cloud.Shared;
using tippy.cloud.Client;
namespace tippy.cloud.Pages.User {
  public partial class Login {
    private readonly LoginParamsType _model = new LoginParamsType();

    [Inject] public NavigationManager NavigationManager { get; set; }

    [Inject] public IAccountService AccountService { get; set; }

    [Inject] public MessageService Message { get; set; }

        [Inject] IMyHttp _http{ get; set; }
        [Inject] MessageService _message { get; set; }
        [Inject] IAuthService AuthService { get; set; }
        public async void HandleSubmit() {

            var result = await AuthService.Login(new LoginDto() { Password = _model.Password, Account = string.IsNullOrEmpty(_model.UserName) ? _model.Mobile : _model.UserName, Captcha = _model.Captcha, LoginType = _model.LoginType }); ;
            if (result.code == 1)
            {
                NavigationManager.NavigateTo("/");
            }
            else
            {
                await _message.Error(result.message);
            }
        }

         
    

    public async Task GetCaptcha() {
      var captcha = await AccountService.GetCaptchaAsync(_model.Mobile);
      await Message.Success($"Verification code validated successfully! The verification code is: {captcha}");
    }
  }
}