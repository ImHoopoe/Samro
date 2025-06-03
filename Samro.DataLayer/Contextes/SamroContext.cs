using Microsoft.EntityFrameworkCore;
using System;
using System.Security;
using Samro.DataLayer.Entities.TournamentMatch;
using WinWin.DataLayer.Entities.BlogBlogGroup;
using WinWin.DataLayer.Entities.ChatHub;
using WinWin.DataLayer.Entities.EventModels;
using WinWin.DataLayer.Entities.RolePernissionUser;
using WinWin.DataLayer.Entities.Roles;
using WinWin.DataLayer.Entities.Sport;
using WinWin.DataLayer.Entities.TournamentMatch;

namespace WinWin.DataLayer.Contextes
{
    public class SamroContext : DbContext
    {
        public SamroContext(DbContextOptions<SamroContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BlogGroup>().HasQueryFilter(bg => bg.IsDeleted == false);
            modelBuilder.Entity<Blog>().HasQueryFilter(bg => bg.IsDeleted == false);
            modelBuilder.Entity<User>().HasQueryFilter(bg => bg.IsActivated == false);
            modelBuilder.Entity<Role>().HasQueryFilter(bg => bg.IsDeleted == false);
            modelBuilder.Entity<Match>().HasQueryFilter(bg => bg.IsDeleted == false);
            //modelBuilder.Entity<Tournament>().HasQueryFilter(bg => bg.IsDeleted == false);

            modelBuilder.Entity<Room>()
            .HasOne(r => r.User1)
                .WithMany()
                .HasForeignKey(r => r.User1Id)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Room>()
                .HasOne(r => r.User2)
                .WithMany()
                .HasForeignKey(r => r.User2Id)
                .OnDelete(DeleteBehavior.NoAction);

            
            modelBuilder.Entity<Message>()
                .HasOne(m => m.Room)
                .WithMany(r => r.Messages)
                .HasForeignKey(m => m.RoomId)
                .OnDelete(DeleteBehavior.NoAction);  
        }


        #region Authorization
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles  { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<Permission> Permisions { get; set; }
        public DbSet<RolePermission> RolePermisions { get; set; }
        
       
        #endregion

        #region BlogBlogGroup

        public DbSet<BlogGroup> BlogGroups { get; set; }
        public DbSet<Blog> Blogs { get; set; }


        #endregion

        #region ChatHub
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Message> Messages { get; set; }


        #endregion

        #region TournamentMatch
        public DbSet<Match> Matches { get; set; }
        public DbSet<Tournament> Tournaments { get; set; }
        public DbSet<MatchRole> MatchRoles { get; set; }
        public DbSet<MatchRound> MatchRounds { get; set; }
        public DbSet<MatchScore> MatchScores { get; set; }
        public DbSet<MatchWarning> MatchWarnings { get; set; }
        public DbSet<MatchUser> MatchUsers { get; set; }
        public DbSet<TournamentUser> TournamentUsers { get; set; }
        public DbSet<TournamentParticipant> TournamentParticipants { get; set; }





        #endregion

        #region Sport
        public DbSet<Sport> Sports { get; set; }
        


        #endregion
    }
}