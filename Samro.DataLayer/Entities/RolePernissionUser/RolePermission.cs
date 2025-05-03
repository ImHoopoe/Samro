using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinWin.DataLayer.Entities.Roles;

namespace WinWin.DataLayer.Entities.RolePernissionUser
{
    public class RolePermission
    {
        [Key]
        public int RpI { get; set; }
        public int RoleId { get; set; }
        public int PermissonId { get; set; }
        public Role Role { get; set; }
        public Permission Permisson { get; set; }
    }
}
