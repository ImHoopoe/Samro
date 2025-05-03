using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinWin.DataLayer.Entities.RolePernissionUser;

namespace WinWin.DataLayer.Entities.Roles
{
   public class Role
    {
        public int RoleId { get; set; }
        public string RoleName { get; set; }
        public bool IsDeleted { get; set; }

        #region Relations

        public IEnumerable<RolePermission> RolePermisions { get; set; }
        public IEnumerable<User> Users { get; set; }
        #endregion

    }
}
