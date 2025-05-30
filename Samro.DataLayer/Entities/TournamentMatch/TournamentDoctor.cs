using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinWin.DataLayer.Entities.EventModels;
using WinWin.DataLayer.Entities.Roles;

namespace Samro.DataLayer.Entities.TournamentMatch
{
    public class TournamentDoctor
    {
        public int TournamentDoctorId { get; set; }
        public Guid? UserId { get; set; }
        public int? TournamentId { get; set; }
        public bool IsAdmin { get; set; } = false;
        #region Relations

        public User? Doctor { get; set; }
        public Tournament? Tournament { get; set; }
        #endregion
    }
}
