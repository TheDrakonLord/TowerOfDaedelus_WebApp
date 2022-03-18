using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TowerOfDaedelus_WebApp.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string? DiscordUserName { get; set; }
        public IList<CharSheet>? CharSheets { get; set; }
        public IList<GMRequests>? GMRequests { get; set; }
        public IList<QuestList>? QuestLists { get; set; }
        public IList<DieRolls>? DieRolls { get; set; }
        public IList<NPCApplications>? NPCApplications { get; set; }
        public IList<MissionApplications>? MissionApplications { get; set; }
        public IList<PlayerApplications>? PlayerApplications { get; set; }
    }
}
