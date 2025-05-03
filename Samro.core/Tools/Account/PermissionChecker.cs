using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace WinWin.Core.Tools.Account
{
    public class PermissionCheckerAttribute : AuthorizeAttribute, IAuthorizationFilter
    {
        private readonly int _permissionId;
        private IRolePermission _permissionsServices;

        public PermissionCheckerAttribute(int id)
        {
            _permissionId = id;
        }

        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            if (context.HttpContext.User.Identity.IsAuthenticated)
            {
                string userName = context.HttpContext.User.Identity.Name;
                _permissionsServices = (IRolePermission)context.HttpContext.RequestServices.GetService(typeof(IRolePermission));

                if (_permissionsServices != null && await _permissionsServices.CheckPermissionAsync(_permissionId, userName))
                {
                    // User Access
                    return;
                }
                else
                {

                    context.Result = new RedirectResult("/AccessDenied");
                }
            }
            else
            {
                context.Result = new RedirectToActionResult("Login", "Account", null);
            }
        }


        public void OnAuthorization(AuthorizationFilterContext context)
        {
            OnAuthorizationAsync(context).Wait();
        }
    }
}