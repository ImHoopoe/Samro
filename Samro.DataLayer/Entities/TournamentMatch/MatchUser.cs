using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinWin.DataLayer.Entities.Roles;

namespace WinWin.DataLayer.Entities.TournamentMatch
{
    public class MatchUser
    {
        public int Id { get; set; }
        public Guid? PlayerId { get; set; }
        public int MatchId { get; set; }

        #region Relations

      
        public User? Player { get; set; }


        public Match Match { get; set; }
        #endregion
    }
}
