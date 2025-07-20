using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using Samro.DataLayer.Entities.TournamentMatch;
using WinWin.DataLayer.Entities.Roles;
using WinWin.DataLayer.Entities.Sport;
using WinWin.DataLayer.Entities.TournamentMatch;

namespace WinWin.DataLayer.Entities.EventModels
{
    public class Tournament
    {
        public int TournamentId { get; set; }
        public string? Title { get; set; }
        public string Address { get; set; }
        public string MatchLocation { get; set; }
        public string WeighInLocation { get; set; }
        public string FaceToFaceLocation { get; set; }
        public string HostelLocation { get; set; }
        public DateTimeOffset WeighInDate { get; set; }
        public DateTimeOffset FaceToFaceDate { get; set; }
        public string Description { get; set; }
        public bool IsDeleted { get; set; }
        public TournamentType TournamentType { get; set; }
        public Guid CreatedByUserId { get; set; }
        public DateTimeOffset RegsiterStartsAt { get; set; }
        public DateTimeOffset RegsiterEndsAt { get; set; }
        public bool IsTimeEnds { get; set; } = false;
        public bool IsFull { get; set; } = false;
        public int MaximnumPlayers { get; set; }
        public bool IsAccepted { get; set; } = false;
        public bool IsFinal { get; set; } = false;
        public int? TournamentDoctorId { get; set; }
        public int? TournamentRefereeId { get; set; }
        public int? RegisteredUsersCount
        {
            get;
            
                //return RegisteredUsers.Count;
            

        }
        public string Thumbnail { get; set; } = "No.png";
        public int? SportId { get; set; }
        public bool IsForMen { get; set; }

        #region Relations
        public ICollection<Match> Matches { get; set; }
        public User CreatedByUser { get; set; }
        public ICollection<TournamentUser> RegisteredUsers { get; set; }
        public ICollection<TournamentParticipant> TournamentParticipants { get; set; }
        public Sport.Sport Sport { get; set; }
        #endregion
    }
}
