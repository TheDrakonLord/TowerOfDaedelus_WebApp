using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace TowerOfDaedelus_WebApp.Models
{
    public class QuestList
    {
        [Key]
        public int QuestId { get; set; }
        [ForeignKey("UserId")]
        public int AuthroID { get; set; }
        public string? Name { get; set; }
        public string? BriefDescription { get; set; }
        public string? Description { get; set; }
        public string? RewardDescription { get; set; }
        public string? EnemyDescription { get; set; }
        [ForeignKey("NPCID")]
        public int? AssociatedNPC { get; set; }
        public bool IsActive { get; set; }
    }
}
