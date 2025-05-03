using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinWin.DataLayer.Entities.EventModels;
using WinWin.DataLayer.Entities.Roles;

namespace WinWin.DataLayer.Entities.TournamentMatch
{
    public class TournamentUser
    {
        public int Id { get; set; }
        public int? TournamentId { get; set; }
        public Guid? UserId { get; set; }

        #region Relations

        [ForeignKey("UserId")]
        public User? user { get; set; }
        [ForeignKey("TournamentId")]
        public Tournament? Tournament { get; set; }


        #endregion
    }
}
