using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace TowerOfDaedelus_WebApp.Models
{
    public class MissionApplications
    {
        [Key]
        public int MissionApplicationID { get; set; }
        [ForeignKey("UserId")]
        public string UserId { get; set; }
        public bool IsApproved { get; set; }
        public bool IsDenied { get; set; }
        public bool IsUnderReview { get; set; }
        public string? RelevantExteriorGM { get; set; }
        public string? InformationSought { get; set; }
        public IList<TeamMembers>? TeamMembers { get; set; }
        public string? TeamDescription { get; set; }
        
    }
}
