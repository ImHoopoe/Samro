using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using WinWin.DataLayer.Entities.RolePernissionUser;

namespace WinWin.DataLayer.Entities.Roles
{
    public class Permission
    {
        [Key]
        public int PermissonId { get; set; }

        [Display(Name = "عنوان نقش")]
        [Required(ErrorMessage = "مقدار{0} را وارد کنید")]
        [MaxLength(100, ErrorMessage = "مقدار {0} نمی تواند بیشتر از {1} کارکتر باشد !")]
        public string PermissonTitle { get; set; }

        public int? ParentId { get; set; }

        [ForeignKey("ParentId")]
        public List<Permission> Permissons { get; set; }
        public List<RolePermission> RolePermisions { get; set; }
        //TODO : Optimize
    }
}
