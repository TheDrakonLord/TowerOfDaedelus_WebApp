using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TowerOfDaedelus_WebApp.Models
{
    public class NPCDescriptions
    {
        [Key]
        public string NPCID { get; set; }
        public string? Name { get; set; }
        public string? Title { get; set; }
        public string? Occupation { get; set; }
        public string? OccupationDescription { get; set; }
        public string? ImportantEvents { get; set; }
        public string? Affiliation { get; set; }
        public string? Family { get; set; }
        public string? BriefDescription { get; set; }
        public string? Description { get; set; }
        public string? Trivia { get; set; }
        public bool IsVisible { get; set; }
        public bool IsRecurring { get; set; }
        public bool IsGMNPC { get; set; }
        public string? Player { get; set; }
        public Uri? AvatarUrl { get; set; }
        public IList<QuestList>? questLists { get; set; }
    }
}
