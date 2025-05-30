using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinWin.DataLayer.Entities.EventModels;
using WinWin.DataLayer.Entities.Roles;

namespace Samro.DataLayer.Entities.TournamentMatch
{
    public class TournamentReferees
    {
        public int TournamentRefereesId { get; set; }
        public Guid? UserId { get; set; }
        public int? TournamentId { get; set; }

        #region Relations

        public User? Referee { get; set; }
        public Tournament? Tournament { get; set; }
        

        #endregion

    }
}
