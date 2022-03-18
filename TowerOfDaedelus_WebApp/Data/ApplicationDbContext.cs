using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TowerOfDaedelus_WebApp.Models;

namespace TowerOfDaedelus_WebApp.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<TeamMembers>().HasKey(tm => new { tm.MissionApplicationsApplicationID, tm.CharacterSheetsCharacterID });
        }
        public DbSet<TowerOfDaedelus_WebApp.Models.CharSheet> CharSheet { get; set; }
        public DbSet<TowerOfDaedelus_WebApp.Models.DieRolls> DieRolls { get; set; }
        public DbSet<TowerOfDaedelus_WebApp.Models.Equipment> Equipment { get; set; }
        public DbSet<TowerOfDaedelus_WebApp.Models.EquipmentTraits> EquipmentTraits { get; set; }
        public DbSet<TowerOfDaedelus_WebApp.Models.FeaturedArticles> FeaturedArticles { get; set; }
        public DbSet<TowerOfDaedelus_WebApp.Models.GMRequests> GMRequests { get; set; }
        public DbSet<TowerOfDaedelus_WebApp.Models.Languages> Languages { get; set; }
        public DbSet<TowerOfDaedelus_WebApp.Models.MissionApplications> MissionApplications { get; set; }
        public DbSet<TowerOfDaedelus_WebApp.Models.NPCApplications> NPCApplications { get; set; }
        public DbSet<TowerOfDaedelus_WebApp.Models.NPCDescriptions> NPCDescriptions { get; set; }
        public DbSet<TowerOfDaedelus_WebApp.Models.OriginTraits> OriginTraits { get; set; }
        public DbSet<TowerOfDaedelus_WebApp.Models.PlayerApplications> PlayerApplications { get; set; }
        public DbSet<TowerOfDaedelus_WebApp.Models.PrimaryTraits> PrimaryTraits { get; set; }
        public DbSet<TowerOfDaedelus_WebApp.Models.QuestList> QuestList { get; set; }
        public DbSet<TowerOfDaedelus_WebApp.Models.RPSchedule> RPSchedule { get; set; }
        public DbSet<TowerOfDaedelus_WebApp.Models.TeamMembers> TeamMembers { get; set; }
        public DbSet<TowerOfDaedelus_WebApp.Models.TemporaryTraits> TemporaryTraits { get; set; }
        public DbSet<TowerOfDaedelus_WebApp.Models.VisionTypes> VisionTypes { get; set; }
    }
}