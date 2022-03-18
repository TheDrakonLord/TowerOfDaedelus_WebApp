using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace TowerOfDaedelus_WebApp.Models
{
    public class NPCApplications
    {
        [Key]
        public int NPCApplicationID { get; set; }
        [ForeignKey("UserId")]
        public string UserId { get; set; }
        public bool IsApproved { get; set; }
        public bool IsDenied { get; set; }
        public bool IsUnderReview { get; set; }
        public int? ExperienceLevel { get; set; }
        public string? Experience1 { get; set; }
        public string? Experience2 { get; set; }
        public string? Experience3 { get; set; }
        public string? Experience4 { get; set; }
        public string? Experience5 { get; set; }
        public string? Vouch1 { get; set; }
        public string? Vouch2 { get; set; }
        public string? Vouch3 { get; set; }
    }
}
