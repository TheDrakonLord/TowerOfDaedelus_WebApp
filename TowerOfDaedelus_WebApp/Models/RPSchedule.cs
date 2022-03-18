using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace TowerOfDaedelus_WebApp.Models
{
    public class RPSchedule
    {
        [Key]
        public int ScheduleID { get; set; }
        public int? LobbyStatus { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
