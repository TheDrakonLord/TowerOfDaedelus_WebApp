using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace TowerOfDaedelus_WebApp.Models
{
    public class RPSchedule
    {
        [Key]
        public int ScheduleID { get; set; }
        public int? LobbyStatus { get; set; }
        public int? StartDay { get; set; }
        public int? StartHour { get; set; }
        public int? StartMinute { get; set; }
        public int? EndDay { get; set; }
        public int? EndHour { get; set; }
        public int? EndMinute { get; set; }
    }
}
