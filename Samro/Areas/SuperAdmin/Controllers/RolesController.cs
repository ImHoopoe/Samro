using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WinWin.Core.Interfaces.RoleInterfaces;
using WinWin.Core.Tools.Account;
using WinWin.DataLayer.DTOS;
using WinWin.DataLayer.Entities.RolePernissionUser;
using WinWin.DataLayer.Entities.Roles;

namespace Samro.Areas.SuperAdmin.Controllers
{
    [Authorize]
    [Area("SuperAdmin")]
    [PermissionChecker(1)]
    public class RolesController : Controller
    {
        private readonly IRole _roleServices;
        private readonly IRolePermission _rolePermission;
        public RolesController(IRole roleServices, IRolePermission rolePermission)
        {
            _roleServices = roleServices;
            _rolePermission = rolePermission;
        }

        public async Task<IActionResult> Index()
        {
            var Roles = await _roleServices.GetAllRolesAsync();
            return View(Roles);
        }

        [HttpGet]
        public async Task<IActionResult> CreateRole()
        {
           ViewBag.Permissions = await _rolePermission.GetAllPermissionsAsync();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateRole(CreateRoleViewModel createRoleViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(createRoleViewModel);
            }

            Role role = new Role()
            {
                RoleName = createRoleViewModel.RoleName,
                IsDeleted = false,
            };
            if (await _roleServices.CreateRole(role))
            {

                _rolePermission.AddPermissionsAsync(role.RoleId, createRoleViewModel.SelectedPermissions);

            }
            
            //await _roleServices.CreateRole(role);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> AddUserRole(Guid id)
        {
            return View();
        }
    }
}
