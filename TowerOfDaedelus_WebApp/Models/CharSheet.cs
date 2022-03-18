using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TowerOfDaedelus_WebApp.Models
{
    public class CharSheet
    {
        [Key]
        public int CharacterID { get; set; }

        [ForeignKey("User")]
        public string UserID { get; set; }

        public string? Name { get; set; }
        public string? Occupation { get; set; }
        public string? OccupationDescription { get; set; }
        public string? Biography { get; set; }
        public string? PhysicalDescriptions { get; set; }
        public int? Mind { get; set; }
        public int? Strength { get; set; }
        public int? Agility { get; set; }
        public int? Constitution { get; set; }
        public int? Soul { get; set; }
        public int? TraitPoints { get; set; }
        public int? EnergyPoints { get; set; }
        public bool IsVisible { get; set; }
        public bool PreviouslyNPC { get; set; }
        public Uri? AvatarURL { get; set; }
        public bool IsApproved { get; set; }
        public bool IsReviewed { get; set; }
        public DateTime? LastChanged { get; set; }
        public IList<TeamMembers>? TeamMembers { get; set; }
        public IList<PrimaryTraits>? PrimaryTraits { get; set; }
        public IList<DieRolls>? DieRolls { get; set; }
        public IList<EquipmentTraits>? EquipmentTraits { get; set; }
        public IList<TemporaryTraits>? TemporaryTraits { get; set; }
        public IList<Equipment>? Equipment { get; set; }
    }

    public class PrimaryTraits
    {
        [Key]
        public int PrimaryTraitID { get; set; }
        [ForeignKey("CharacterID")]
        public int CharacterID { get; set; }
        public string? Name { get; set; }
        public bool IsVisible { get; set; }
        public string? Description { get; set; }
        public bool IsPassive { get; set; }
        public int? EnergyCost { get; set; }
        public int? AbilityPointCost { get; set; }
        public bool IsMajor { get; set; }
        public bool IsEdge { get; set; }
    }

    public class EquipmentTraits
    {
        [Key]
        public int EquipmentTraitID { get; set; }
        [ForeignKey("CharacterID")]
        public string CharacterID { get; set; }
        public string? Name { get; set; }
        public bool IsVisible { get; set; }
        public string? Description { get; set; }
        public bool IsPassive { get; set; }
        public int? EnergyCost { get; set; }
        public int? AbilityPointCost { get; set; }
    }

    public class OriginTraits
    {
        [Key]
        public int OriginTraitID { get; set; }
        [ForeignKey("CharacterID")]
        public int CharacterID { get; set; }
        public string? Name { get; set; }
        public bool IsVisible { get; set; }
        public string? Description { get; set; }
        public IList<VisionTypes>? VisionTypes { get; set; }
        public IList<Languages>? Languages { get; set; }
    }

    public class Languages
    {
        [Key]
        public int LanguageID { get; set; }
        [ForeignKey("OriginTraitID")]
        public int OriginTraitID { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
    }

    public class VisionTypes
    {
        [Key]
        public int VisionID { get; set; }
        [ForeignKey("OriginTraitID")]
        public int OriginTraitID { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
    }

    public class Equipment
    {
        [Key]
        public int EquipmentID { get; set; }
        [ForeignKey("CharacterID")]
        public int CharacterID { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public int? Cost { get; set; }
        public decimal? Weight { get; set; }
    }

    public class TemporaryTraits
    {
        [Key]
        public int TemporaryTraitID { get; set; }
        [ForeignKey("CharacterID")]
        public int CharacterID { get; set; }
        public string? Name { get; set; }
        public bool IsVisible { get; set; }
        public string? Description { get; set; }
        public bool IsPassive { get; set; }
        public int? EnergyCost { get; set; }
    }

}
