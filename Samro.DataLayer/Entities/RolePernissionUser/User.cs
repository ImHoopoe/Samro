using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using Samro.DataLayer.Entities.TournamentMatch;
using WinWin.DataLayer.Entities.EventModels;
using WinWin.DataLayer.Entities.RolePernissionUser;
using WinWin.DataLayer.Entities.TournamentMatch;

namespace WinWin.DataLayer.Entities.Roles
{
    public class User
    {
        [Key]
        public Guid UserId { get; set; }
        public string UserName { get; set; }
        public string? Name { get; set; }
        public string? LastName { get; set; }
        public string Password { get; set; }
        public string? Email { get; set; }
        public string PhoneNumber { get; set; }
        public string? NationalId { get; set; }
        public string? Avatar { get; set; }
        public string? Address { get; set; }
        public string? BirthDay { get; set; }
        public double? Height { get; set; }
        public double? Weight { get; set; }
        public bool IsActivated { get; set; }
        public Guid ActivationCode { get; set; }
        public bool IsMan { get; set; }
        public int? Age  { get; set; }
        #region AccessbilityRelations
        public IEnumerable<UserRole> UserRoles { get; set; }
        #endregion
        #region SportRelations



        #endregion

        #region EventRelations
       
        public ICollection<Tournament>? CreatedTournaments { get; set; }
        public ICollection<TournamentParticipant>? TournamentParticipants { get; set; }


        #endregion


        #region GameRealtions



        #endregion
    }
}
