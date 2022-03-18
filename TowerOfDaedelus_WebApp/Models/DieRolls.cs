using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace TowerOfDaedelus_WebApp.Models
{
    public class DieRolls
    {
        [Key]
        public int RollID { get; set; }
        [ForeignKey("UserId")]
        public int UserID { get; set; }
        public DateTime? Timestamp { get; set; }
        public int? NumberOfRolls { get; set; }
        public int? DieType { get; set; }
        public int? GMRollModifier { get; set; }
        public int? CharacterAbility { get; set; }
        [ForeignKey("CharacterID")]
        public int? SourceCharacterID { get; set; }
        public bool IsAdvantage { get; set; }
        public bool IsDisadvantage { get; set; }
        public int? Result { get; set; }
        public string? DetailedResult { get; set; }
    }
}
