using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TowerOfDaedelus_WebApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class CreatePrimaryData2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "CharSheet",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DiscordUserName",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "DieRolls",
                columns: table => new
                {
                    RollID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserID = table.Column<int>(type: "int", nullable: false),
                    Timestamp = table.Column<DateTime>(type: "datetime2", nullable: true),
                    NumberOfRolls = table.Column<int>(type: "int", nullable: true),
                    DieType = table.Column<int>(type: "int", nullable: true),
                    GMRollModifier = table.Column<int>(type: "int", nullable: true),
                    CharacterAbility = table.Column<int>(type: "int", nullable: true),
                    SourceCharacterID = table.Column<int>(type: "int", nullable: true),
                    IsAdvantage = table.Column<bool>(type: "bit", nullable: false),
                    IsDisadvantage = table.Column<bool>(type: "bit", nullable: false),
                    Result = table.Column<int>(type: "int", nullable: true),
                    DetailedResult = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ApplicationUserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    CharSheetCharacterID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DieRolls", x => x.RollID);
                    table.ForeignKey(
                        name: "FK_DieRolls_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_DieRolls_CharSheet_CharSheetCharacterID",
                        column: x => x.CharSheetCharacterID,
                        principalTable: "CharSheet",
                        principalColumn: "CharacterID");
                });

            migrationBuilder.CreateTable(
                name: "Equipment",
                columns: table => new
                {
                    EquipmentID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CharacterID = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Cost = table.Column<int>(type: "int", nullable: true),
                    Weight = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    CharSheetCharacterID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Equipment", x => x.EquipmentID);
                    table.ForeignKey(
                        name: "FK_Equipment_CharSheet_CharSheetCharacterID",
                        column: x => x.CharSheetCharacterID,
                        principalTable: "CharSheet",
                        principalColumn: "CharacterID");
                });

            migrationBuilder.CreateTable(
                name: "EquipmentTraits",
                columns: table => new
                {
                    EquipmentTraitID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CharacterID = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsVisible = table.Column<bool>(type: "bit", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsPassive = table.Column<bool>(type: "bit", nullable: false),
                    EnergyCost = table.Column<int>(type: "int", nullable: true),
                    AbilityPointCost = table.Column<int>(type: "int", nullable: true),
                    CharSheetCharacterID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EquipmentTraits", x => x.EquipmentTraitID);
                    table.ForeignKey(
                        name: "FK_EquipmentTraits_CharSheet_CharSheetCharacterID",
                        column: x => x.CharSheetCharacterID,
                        principalTable: "CharSheet",
                        principalColumn: "CharacterID");
                });

            migrationBuilder.CreateTable(
                name: "FeaturedArticles",
                columns: table => new
                {
                    ArticleID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ArticleUri = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsFeatured = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FeaturedArticles", x => x.ArticleID);
                });

            migrationBuilder.CreateTable(
                name: "GMRequests",
                columns: table => new
                {
                    RequestID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RequesterID = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsComplete = table.Column<bool>(type: "bit", nullable: false),
                    IsRelayed = table.Column<bool>(type: "bit", nullable: false),
                    Timestamp = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RequesterLocation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RequesterReason = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Urgency = table.Column<int>(type: "int", nullable: true),
                    ApplicationUserId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GMRequests", x => x.RequestID);
                    table.ForeignKey(
                        name: "FK_GMRequests_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "MissionApplications",
                columns: table => new
                {
                    MissionApplicationID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsApproved = table.Column<bool>(type: "bit", nullable: false),
                    IsDenied = table.Column<bool>(type: "bit", nullable: false),
                    IsUnderReview = table.Column<bool>(type: "bit", nullable: false),
                    RelevantExteriorGM = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InformationSought = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TeamDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ApplicationUserId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MissionApplications", x => x.MissionApplicationID);
                    table.ForeignKey(
                        name: "FK_MissionApplications_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "NPCApplications",
                columns: table => new
                {
                    NPCApplicationID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsApproved = table.Column<bool>(type: "bit", nullable: false),
                    IsDenied = table.Column<bool>(type: "bit", nullable: false),
                    IsUnderReview = table.Column<bool>(type: "bit", nullable: false),
                    ExperienceLevel = table.Column<int>(type: "int", nullable: true),
                    Experience1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Experience2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Experience3 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Experience4 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Experience5 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Vouch1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Vouch2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Vouch3 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ApplicationUserId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NPCApplications", x => x.NPCApplicationID);
                    table.ForeignKey(
                        name: "FK_NPCApplications_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "NPCDescriptions",
                columns: table => new
                {
                    NPCID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Occupation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OccupationDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImportantEvents = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Affiliation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Family = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BriefDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Trivia = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsVisible = table.Column<bool>(type: "bit", nullable: false),
                    IsRecurring = table.Column<bool>(type: "bit", nullable: false),
                    IsGMNPC = table.Column<bool>(type: "bit", nullable: false),
                    Player = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AvatarUrl = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NPCDescriptions", x => x.NPCID);
                });

            migrationBuilder.CreateTable(
                name: "OriginTraits",
                columns: table => new
                {
                    OriginTraitID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CharacterID = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsVisible = table.Column<bool>(type: "bit", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OriginTraits", x => x.OriginTraitID);
                });

            migrationBuilder.CreateTable(
                name: "PlayerApplications",
                columns: table => new
                {
                    PlayerApplicationID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsApproved = table.Column<bool>(type: "bit", nullable: false),
                    IsDenied = table.Column<bool>(type: "bit", nullable: false),
                    IsUnderReview = table.Column<bool>(type: "bit", nullable: false),
                    ExperienceLevel = table.Column<int>(type: "int", nullable: true),
                    Experience1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Experience2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Experience3 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Experience4 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Experience5 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Vouch1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Vouch2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Vouch3 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CharSheetCharacterID = table.Column<int>(type: "int", nullable: true),
                    ApplicationUserId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlayerApplications", x => x.PlayerApplicationID);
                    table.ForeignKey(
                        name: "FK_PlayerApplications_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PlayerApplications_CharSheet_CharSheetCharacterID",
                        column: x => x.CharSheetCharacterID,
                        principalTable: "CharSheet",
                        principalColumn: "CharacterID");
                });

            migrationBuilder.CreateTable(
                name: "PrimaryTraits",
                columns: table => new
                {
                    PrimaryTraitID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CharacterID = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsVisible = table.Column<bool>(type: "bit", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsPassive = table.Column<bool>(type: "bit", nullable: false),
                    EnergyCost = table.Column<int>(type: "int", nullable: true),
                    AbilityPointCost = table.Column<int>(type: "int", nullable: true),
                    IsMajor = table.Column<bool>(type: "bit", nullable: false),
                    IsEdge = table.Column<bool>(type: "bit", nullable: false),
                    CharSheetCharacterID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PrimaryTraits", x => x.PrimaryTraitID);
                    table.ForeignKey(
                        name: "FK_PrimaryTraits_CharSheet_CharSheetCharacterID",
                        column: x => x.CharSheetCharacterID,
                        principalTable: "CharSheet",
                        principalColumn: "CharacterID");
                });

            migrationBuilder.CreateTable(
                name: "RPSchedule",
                columns: table => new
                {
                    ScheduleID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LobbyStatus = table.Column<int>(type: "int", nullable: true),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RPSchedule", x => x.ScheduleID);
                });

            migrationBuilder.CreateTable(
                name: "TemporaryTraits",
                columns: table => new
                {
                    TemporaryTraitID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CharacterID = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsVisible = table.Column<bool>(type: "bit", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsPassive = table.Column<bool>(type: "bit", nullable: false),
                    EnergyCost = table.Column<int>(type: "int", nullable: true),
                    CharSheetCharacterID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TemporaryTraits", x => x.TemporaryTraitID);
                    table.ForeignKey(
                        name: "FK_TemporaryTraits_CharSheet_CharSheetCharacterID",
                        column: x => x.CharSheetCharacterID,
                        principalTable: "CharSheet",
                        principalColumn: "CharacterID");
                });

            migrationBuilder.CreateTable(
                name: "TeamMembers",
                columns: table => new
                {
                    MissionApplicationsApplicationID = table.Column<int>(type: "int", nullable: false),
                    CharacterSheetsCharacterID = table.Column<int>(type: "int", nullable: false),
                    MissionApplicationID = table.Column<int>(type: "int", nullable: false),
                    CharSheetCharacterID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeamMembers", x => new { x.MissionApplicationsApplicationID, x.CharacterSheetsCharacterID });
                    table.ForeignKey(
                        name: "FK_TeamMembers_CharSheet_CharSheetCharacterID",
                        column: x => x.CharSheetCharacterID,
                        principalTable: "CharSheet",
                        principalColumn: "CharacterID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TeamMembers_MissionApplications_MissionApplicationID",
                        column: x => x.MissionApplicationID,
                        principalTable: "MissionApplications",
                        principalColumn: "MissionApplicationID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "QuestList",
                columns: table => new
                {
                    QuestId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AuthroID = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BriefDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RewardDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EnemyDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AssociatedNPC = table.Column<int>(type: "int", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    ApplicationUserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    NPCDescriptionsNPCID = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuestList", x => x.QuestId);
                    table.ForeignKey(
                        name: "FK_QuestList_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_QuestList_NPCDescriptions_NPCDescriptionsNPCID",
                        column: x => x.NPCDescriptionsNPCID,
                        principalTable: "NPCDescriptions",
                        principalColumn: "NPCID");
                });

            migrationBuilder.CreateTable(
                name: "Languages",
                columns: table => new
                {
                    LanguageID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OriginTraitID = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OriginTraitsOriginTraitID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Languages", x => x.LanguageID);
                    table.ForeignKey(
                        name: "FK_Languages_OriginTraits_OriginTraitsOriginTraitID",
                        column: x => x.OriginTraitsOriginTraitID,
                        principalTable: "OriginTraits",
                        principalColumn: "OriginTraitID");
                });

            migrationBuilder.CreateTable(
                name: "VisionTypes",
                columns: table => new
                {
                    VisionID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OriginTraitID = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OriginTraitsOriginTraitID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VisionTypes", x => x.VisionID);
                    table.ForeignKey(
                        name: "FK_VisionTypes_OriginTraits_OriginTraitsOriginTraitID",
                        column: x => x.OriginTraitsOriginTraitID,
                        principalTable: "OriginTraits",
                        principalColumn: "OriginTraitID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_CharSheet_ApplicationUserId",
                table: "CharSheet",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_DieRolls_ApplicationUserId",
                table: "DieRolls",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_DieRolls_CharSheetCharacterID",
                table: "DieRolls",
                column: "CharSheetCharacterID");

            migrationBuilder.CreateIndex(
                name: "IX_Equipment_CharSheetCharacterID",
                table: "Equipment",
                column: "CharSheetCharacterID");

            migrationBuilder.CreateIndex(
                name: "IX_EquipmentTraits_CharSheetCharacterID",
                table: "EquipmentTraits",
                column: "CharSheetCharacterID");

            migrationBuilder.CreateIndex(
                name: "IX_GMRequests_ApplicationUserId",
                table: "GMRequests",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Languages_OriginTraitsOriginTraitID",
                table: "Languages",
                column: "OriginTraitsOriginTraitID");

            migrationBuilder.CreateIndex(
                name: "IX_MissionApplications_ApplicationUserId",
                table: "MissionApplications",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_NPCApplications_ApplicationUserId",
                table: "NPCApplications",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_PlayerApplications_ApplicationUserId",
                table: "PlayerApplications",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_PlayerApplications_CharSheetCharacterID",
                table: "PlayerApplications",
                column: "CharSheetCharacterID");

            migrationBuilder.CreateIndex(
                name: "IX_PrimaryTraits_CharSheetCharacterID",
                table: "PrimaryTraits",
                column: "CharSheetCharacterID");

            migrationBuilder.CreateIndex(
                name: "IX_QuestList_ApplicationUserId",
                table: "QuestList",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_QuestList_NPCDescriptionsNPCID",
                table: "QuestList",
                column: "NPCDescriptionsNPCID");

            migrationBuilder.CreateIndex(
                name: "IX_TeamMembers_CharSheetCharacterID",
                table: "TeamMembers",
                column: "CharSheetCharacterID");

            migrationBuilder.CreateIndex(
                name: "IX_TeamMembers_MissionApplicationID",
                table: "TeamMembers",
                column: "MissionApplicationID");

            migrationBuilder.CreateIndex(
                name: "IX_TemporaryTraits_CharSheetCharacterID",
                table: "TemporaryTraits",
                column: "CharSheetCharacterID");

            migrationBuilder.CreateIndex(
                name: "IX_VisionTypes_OriginTraitsOriginTraitID",
                table: "VisionTypes",
                column: "OriginTraitsOriginTraitID");

            migrationBuilder.AddForeignKey(
                name: "FK_CharSheet_AspNetUsers_ApplicationUserId",
                table: "CharSheet",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CharSheet_AspNetUsers_ApplicationUserId",
                table: "CharSheet");

            migrationBuilder.DropTable(
                name: "DieRolls");

            migrationBuilder.DropTable(
                name: "Equipment");

            migrationBuilder.DropTable(
                name: "EquipmentTraits");

            migrationBuilder.DropTable(
                name: "FeaturedArticles");

            migrationBuilder.DropTable(
                name: "GMRequests");

            migrationBuilder.DropTable(
                name: "Languages");

            migrationBuilder.DropTable(
                name: "NPCApplications");

            migrationBuilder.DropTable(
                name: "PlayerApplications");

            migrationBuilder.DropTable(
                name: "PrimaryTraits");

            migrationBuilder.DropTable(
                name: "QuestList");

            migrationBuilder.DropTable(
                name: "RPSchedule");

            migrationBuilder.DropTable(
                name: "TeamMembers");

            migrationBuilder.DropTable(
                name: "TemporaryTraits");

            migrationBuilder.DropTable(
                name: "VisionTypes");

            migrationBuilder.DropTable(
                name: "NPCDescriptions");

            migrationBuilder.DropTable(
                name: "MissionApplications");

            migrationBuilder.DropTable(
                name: "OriginTraits");

            migrationBuilder.DropIndex(
                name: "IX_CharSheet_ApplicationUserId",
                table: "CharSheet");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "CharSheet");

            migrationBuilder.DropColumn(
                name: "DiscordUserName",
                table: "AspNetUsers");
        }
    }
}
