using System.Threading.Tasks;
using tippy.cloud.Models;
using tippy.cloud.Services;
using Microsoft.AspNetCore.Components;
using tippy.cloud.Shared;

namespace tippy.cloud.Pages.Account.Settings
{
    public partial class BaseView
    {
        private CurrentUser _currentUser = new CurrentUser();

        [Inject] protected IUserService UserService { get; set; }

        private void HandleFinish()
        {
        }

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
            _currentUser = await UserService.GetCurrentUserAsync();
        }
    }
}