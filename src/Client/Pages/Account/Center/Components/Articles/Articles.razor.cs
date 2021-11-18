using System.Collections.Generic;
using tippy.cloud.Models;
using Microsoft.AspNetCore.Components;

namespace tippy.cloud.Pages.Account.Center
{
    public partial class Articles
    {
        [Parameter] public IList<ListItemDataType> List { get; set; }
    }
}