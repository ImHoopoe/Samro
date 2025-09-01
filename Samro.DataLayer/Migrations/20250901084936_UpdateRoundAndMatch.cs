using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Samro.DataLayer.Migrations
{
    /// <inheritdoc />
    public partial class UpdateRoundAndMatch : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BlogGroups",
                columns: table => new
                {
                    BlogGroupId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BlogGroupName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ParentId = table.Column<int>(type: "int", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BlogGroups", x => x.BlogGroupId);
                    table.ForeignKey(
                        name: "FK_BlogGroups_BlogGroups_ParentId",
                        column: x => x.ParentId,
                        principalTable: "BlogGroups",
                        principalColumn: "BlogGroupId");
                });

            migrationBuilder.CreateTable(
                name: "MatchRoles",
                columns: table => new
                {
                    MatchRoleId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MatchRoleName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MatchRoleDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MatchRoles", x => x.MatchRoleId);
                });

            migrationBuilder.CreateTable(
                name: "Permisions",
                columns: table => new
                {
                    PermissonId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PermissonTitle = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ParentId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Permisions", x => x.PermissonId);
                    table.ForeignKey(
                        name: "FK_Permisions_Permisions_ParentId",
                        column: x => x.ParentId,
                        principalTable: "Permisions",
                        principalColumn: "PermissonId");
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    RoleId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.RoleId);
                });

            migrationBuilder.CreateTable(
                name: "Sports",
                columns: table => new
                {
                    SportId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SportName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ParentId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sports", x => x.SportId);
                    table.ForeignKey(
                        name: "FK_Sports_Sports_ParentId",
                        column: x => x.ParentId,
                        principalTable: "Sports",
                        principalColumn: "SportId");
                });

            migrationBuilder.CreateTable(
                name: "Blogs",
                columns: table => new
                {
                    BlogId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ShortDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Thumbnail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PublishDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    BlogGroupId = table.Column<int>(type: "int", nullable: false),
                    Tags = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Blogs", x => x.BlogId);
                    table.ForeignKey(
                        name: "FK_Blogs_BlogGroups_BlogGroupId",
                        column: x => x.BlogGroupId,
                        principalTable: "BlogGroups",
                        principalColumn: "BlogGroupId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RolePermisions",
                columns: table => new
                {
                    RpI = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    PermissonId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RolePermisions", x => x.RpI);
                    table.ForeignKey(
                        name: "FK_RolePermisions_Permisions_PermissonId",
                        column: x => x.PermissonId,
                        principalTable: "Permisions",
                        principalColumn: "PermissonId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RolePermisions_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "RoleId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Matches",
                columns: table => new
                {
                    MatchId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MatchDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    TournamentId = table.Column<int>(type: "int", nullable: false),
                    Location = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Date = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    SportId = table.Column<int>(type: "int", nullable: true),
                    Player1Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Player2Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsCompleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Matches", x => x.MatchId);
                });

            migrationBuilder.CreateTable(
                name: "MatchScores",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MatchId = table.Column<int>(type: "int", nullable: false),
                    PlayerRedScore = table.Column<int>(type: "int", nullable: false),
                    PlayerBlueScore = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MatchScores", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MatchScores_Matches_MatchId",
                        column: x => x.MatchId,
                        principalTable: "Matches",
                        principalColumn: "MatchId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MatchWarnings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MatchId = table.Column<int>(type: "int", nullable: false),
                    PlayerNumber = table.Column<int>(type: "int", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    Reason = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IssuedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MatchWarnings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MatchWarnings_Matches_MatchId",
                        column: x => x.MatchId,
                        principalTable: "Matches",
                        principalColumn: "MatchId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Rounds",
                columns: table => new
                {
                    RoundId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MatchId = table.Column<int>(type: "int", nullable: false),
                    RoundNumber = table.Column<int>(type: "int", nullable: false),
                    StartTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Player1Score = table.Column<int>(type: "int", nullable: false),
                    Player2Score = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rounds", x => x.RoundId);
                    table.ForeignKey(
                        name: "FK_Rounds_Matches_MatchId",
                        column: x => x.MatchId,
                        principalTable: "Matches",
                        principalColumn: "MatchId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SportToMatch",
                columns: table => new
                {
                    SportToMatchId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SportId = table.Column<int>(type: "int", nullable: false),
                    MatchId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SportToMatch", x => x.SportToMatchId);
                    table.ForeignKey(
                        name: "FK_SportToMatch_Matches_MatchId",
                        column: x => x.MatchId,
                        principalTable: "Matches",
                        principalColumn: "MatchId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SportToMatch_Sports_SportId",
                        column: x => x.SportId,
                        principalTable: "Sports",
                        principalColumn: "SportId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NationalId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Avatar = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BirthDay = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Height = table.Column<double>(type: "float", nullable: true),
                    Weight = table.Column<double>(type: "float", nullable: true),
                    IsActivated = table.Column<bool>(type: "bit", nullable: false),
                    ActivationCode = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsMan = table.Column<bool>(type: "bit", nullable: false),
                    Age = table.Column<int>(type: "int", nullable: true),
                    MatchId = table.Column<int>(type: "int", nullable: true),
                    RoleId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_Users_Matches_MatchId",
                        column: x => x.MatchId,
                        principalTable: "Matches",
                        principalColumn: "MatchId");
                    table.ForeignKey(
                        name: "FK_Users_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "RoleId");
                });

            migrationBuilder.CreateTable(
                name: "MatchUsers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PlayerId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    MatchId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MatchUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MatchUsers_Matches_MatchId",
                        column: x => x.MatchId,
                        principalTable: "Matches",
                        principalColumn: "MatchId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MatchUsers_Users_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "Users",
                        principalColumn: "UserId");
                });

            migrationBuilder.CreateTable(
                name: "Rooms",
                columns: table => new
                {
                    RoomId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RoomName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    User1Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    User2Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rooms", x => x.RoomId);
                    table.ForeignKey(
                        name: "FK_Rooms_Users_User1Id",
                        column: x => x.User1Id,
                        principalTable: "Users",
                        principalColumn: "UserId");
                    table.ForeignKey(
                        name: "FK_Rooms_Users_User2Id",
                        column: x => x.User2Id,
                        principalTable: "Users",
                        principalColumn: "UserId");
                });

            migrationBuilder.CreateTable(
                name: "Tournaments",
                columns: table => new
                {
                    TournamentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MatchLocation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WeighInLocation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FaceToFaceLocation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HostelLocation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WeighInDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    FaceToFaceDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    TournamentType = table.Column<int>(type: "int", nullable: false),
                    CreatedByUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RegsiterStartsAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    RegsiterEndsAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    IsTimeEnds = table.Column<bool>(type: "bit", nullable: false),
                    IsFull = table.Column<bool>(type: "bit", nullable: false),
                    MaximnumPlayers = table.Column<int>(type: "int", nullable: false),
                    IsAccepted = table.Column<bool>(type: "bit", nullable: false),
                    IsFinal = table.Column<bool>(type: "bit", nullable: false),
                    TournamentDoctorId = table.Column<int>(type: "int", nullable: true),
                    TournamentRefereeId = table.Column<int>(type: "int", nullable: true),
                    Thumbnail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SportId = table.Column<int>(type: "int", nullable: true),
                    IsForMen = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tournaments", x => x.TournamentId);
                    table.ForeignKey(
                        name: "FK_Tournaments_Sports_SportId",
                        column: x => x.SportId,
                        principalTable: "Sports",
                        principalColumn: "SportId");
                    table.ForeignKey(
                        name: "FK_Tournaments_Users_CreatedByUserId",
                        column: x => x.CreatedByUserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserRoles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRoles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserRoles_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "RoleId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserRoles_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Messages",
                columns: table => new
                {
                    MessageId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Timestamp = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false),
                    FromUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RoomId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Messages", x => x.MessageId);
                    table.ForeignKey(
                        name: "FK_Messages_Rooms_RoomId",
                        column: x => x.RoomId,
                        principalTable: "Rooms",
                        principalColumn: "RoomId");
                    table.ForeignKey(
                        name: "FK_Messages_Users_FromUserId",
                        column: x => x.FromUserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TournamentParticipants",
                columns: table => new
                {
                    TournamentParticipantId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    TournamentId = table.Column<int>(type: "int", nullable: true),
                    RoleId = table.Column<int>(type: "int", nullable: true),
                    MatchRoleId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TournamentParticipants", x => x.TournamentParticipantId);
                    table.ForeignKey(
                        name: "FK_TournamentParticipants_MatchRoles_MatchRoleId",
                        column: x => x.MatchRoleId,
                        principalTable: "MatchRoles",
                        principalColumn: "MatchRoleId");
                    table.ForeignKey(
                        name: "FK_TournamentParticipants_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "RoleId");
                    table.ForeignKey(
                        name: "FK_TournamentParticipants_Tournaments_TournamentId",
                        column: x => x.TournamentId,
                        principalTable: "Tournaments",
                        principalColumn: "TournamentId");
                    table.ForeignKey(
                        name: "FK_TournamentParticipants_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId");
                });

            migrationBuilder.CreateTable(
                name: "TournamentUsers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TournamentId = table.Column<int>(type: "int", nullable: true),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TournamentUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TournamentUsers_Tournaments_TournamentId",
                        column: x => x.TournamentId,
                        principalTable: "Tournaments",
                        principalColumn: "TournamentId");
                    table.ForeignKey(
                        name: "FK_TournamentUsers_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_BlogGroups_ParentId",
                table: "BlogGroups",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_Blogs_BlogGroupId",
                table: "Blogs",
                column: "BlogGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_Matches_TournamentId",
                table: "Matches",
                column: "TournamentId");

            migrationBuilder.CreateIndex(
                name: "IX_MatchScores_MatchId",
                table: "MatchScores",
                column: "MatchId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MatchUsers_MatchId",
                table: "MatchUsers",
                column: "MatchId");

            migrationBuilder.CreateIndex(
                name: "IX_MatchUsers_PlayerId",
                table: "MatchUsers",
                column: "PlayerId");

            migrationBuilder.CreateIndex(
                name: "IX_MatchWarnings_MatchId",
                table: "MatchWarnings",
                column: "MatchId");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_FromUserId",
                table: "Messages",
                column: "FromUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_RoomId",
                table: "Messages",
                column: "RoomId");

            migrationBuilder.CreateIndex(
                name: "IX_Permisions_ParentId",
                table: "Permisions",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_RolePermisions_PermissonId",
                table: "RolePermisions",
                column: "PermissonId");

            migrationBuilder.CreateIndex(
                name: "IX_RolePermisions_RoleId",
                table: "RolePermisions",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_Rooms_User1Id",
                table: "Rooms",
                column: "User1Id");

            migrationBuilder.CreateIndex(
                name: "IX_Rooms_User2Id",
                table: "Rooms",
                column: "User2Id");

            migrationBuilder.CreateIndex(
                name: "IX_Rounds_MatchId",
                table: "Rounds",
                column: "MatchId");

            migrationBuilder.CreateIndex(
                name: "IX_Sports_ParentId",
                table: "Sports",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_SportToMatch_MatchId",
                table: "SportToMatch",
                column: "MatchId");

            migrationBuilder.CreateIndex(
                name: "IX_SportToMatch_SportId",
                table: "SportToMatch",
                column: "SportId");

            migrationBuilder.CreateIndex(
                name: "IX_TournamentParticipants_MatchRoleId",
                table: "TournamentParticipants",
                column: "MatchRoleId");

            migrationBuilder.CreateIndex(
                name: "IX_TournamentParticipants_RoleId",
                table: "TournamentParticipants",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_TournamentParticipants_TournamentId",
                table: "TournamentParticipants",
                column: "TournamentId");

            migrationBuilder.CreateIndex(
                name: "IX_TournamentParticipants_UserId",
                table: "TournamentParticipants",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Tournaments_CreatedByUserId",
                table: "Tournaments",
                column: "CreatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Tournaments_SportId",
                table: "Tournaments",
                column: "SportId");

            migrationBuilder.CreateIndex(
                name: "IX_TournamentUsers_TournamentId",
                table: "TournamentUsers",
                column: "TournamentId");

            migrationBuilder.CreateIndex(
                name: "IX_TournamentUsers_UserId",
                table: "TournamentUsers",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRoles_RoleId",
                table: "UserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRoles_UserId",
                table: "UserRoles",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_MatchId",
                table: "Users",
                column: "MatchId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_RoleId",
                table: "Users",
                column: "RoleId");

            migrationBuilder.AddForeignKey(
                name: "FK_Matches_Tournaments_TournamentId",
                table: "Matches",
                column: "TournamentId",
                principalTable: "Tournaments",
                principalColumn: "TournamentId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Matches_Tournaments_TournamentId",
                table: "Matches");

            migrationBuilder.DropTable(
                name: "Blogs");

            migrationBuilder.DropTable(
                name: "MatchScores");

            migrationBuilder.DropTable(
                name: "MatchUsers");

            migrationBuilder.DropTable(
                name: "MatchWarnings");

            migrationBuilder.DropTable(
                name: "Messages");

            migrationBuilder.DropTable(
                name: "RolePermisions");

            migrationBuilder.DropTable(
                name: "Rounds");

            migrationBuilder.DropTable(
                name: "SportToMatch");

            migrationBuilder.DropTable(
                name: "TournamentParticipants");

            migrationBuilder.DropTable(
                name: "TournamentUsers");

            migrationBuilder.DropTable(
                name: "UserRoles");

            migrationBuilder.DropTable(
                name: "BlogGroups");

            migrationBuilder.DropTable(
                name: "Rooms");

            migrationBuilder.DropTable(
                name: "Permisions");

            migrationBuilder.DropTable(
                name: "MatchRoles");

            migrationBuilder.DropTable(
                name: "Tournaments");

            migrationBuilder.DropTable(
                name: "Sports");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Matches");

            migrationBuilder.DropTable(
                name: "Roles");
        }
    }
}
