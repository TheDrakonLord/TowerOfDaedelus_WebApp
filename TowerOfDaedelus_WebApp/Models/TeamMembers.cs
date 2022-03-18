using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace TowerOfDaedelus_WebApp.Models
{
    public class TeamMembers
    {
        [ForeignKey("MissionApplicationID")]
        public int MissionApplicationsApplicationID { get; set; }
        public MissionApplications MissionApplication { get; set; }

        [ForeignKey("CharacterID")]
        public int CharacterSheetsCharacterID { get; set; }
        public CharSheet CharSheet { get; set; }
    }
}
