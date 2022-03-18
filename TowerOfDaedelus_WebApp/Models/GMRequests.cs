using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace TowerOfDaedelus_WebApp.Models
{
    public class GMRequests
    {
        [Key]
        public int RequestID { get; set; }
        [ForeignKey("UserId")]
        public string RequesterID { get; set; }
        public bool IsComplete { get; set; }
        public bool IsRelayed { get; set; }
        public DateTime? Timestamp { get; set; }
        public string? RequesterLocation { get; set; }
        public string? RequesterReason { get; set; }
        public int? Urgency { get; set; }
    }
}
