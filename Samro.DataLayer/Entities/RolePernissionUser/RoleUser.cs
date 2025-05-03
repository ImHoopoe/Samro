using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinWin.DataLayer.Entities.Roles;

namespace WinWin.DataLayer.Entities.RolePernissionUser
{
    public class UserRole
    {
        public int Id { get; set; }
        public int RoleId { get; set; }
        public Guid UserId { get; set; }
        #region Relations
        public User User { get; set; }
        public Role Role { get; set; }
        

        #endregion
    }
}
