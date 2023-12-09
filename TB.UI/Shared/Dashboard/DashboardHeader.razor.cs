using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;
using TB.Shared.Dto.Site;
using TB.Shared.Enums;
using TB.UI.Helper;

namespace TB.UI.Shared.Dashboard
{
    public partial class DashboardHeader
    {
        [CascadingParameter]
        public Task<AuthenticationState> AuthState { get; set; }

        private SiteUserDto userData;
        protected override async Task OnParametersSetAsync()
        {
            var auth = await AuthState;

            if (auth.User.Identity.IsAuthenticated)
            {
                var claims = auth.User.Claims.ToList();

                userData = new SiteUserDto
                {
                    Name = claims.FirstOrDefault(p => p.Type == "Name")?.Value,
                    LastName = claims.FirstOrDefault(p => p.Type == "LastName")?.Value,
                    Role = SiteHelper.ParseEnum<RoleType>(claims.FirstOrDefault(p => p.Type == ClaimTypes.Role)?.Value),
                    Email = claims.FirstOrDefault(p => p.Type == "Email")?.Value,
                    Image = claims.FirstOrDefault(p => p.Type == "Image")?.Value,
                    Id = Convert.ToInt32(claims.FirstOrDefault(p => p.Type == "Id")?.Value),
                };
            }
            else
            {
                userData = null;
            }

            await base.OnParametersSetAsync();
        }

        private void Logout()
        {
            authService.Logout();
        }
    }
}
