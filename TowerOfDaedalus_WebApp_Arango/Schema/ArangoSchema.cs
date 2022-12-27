using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using ArangoDBNetStandard;

namespace TowerOfDaedalus_WebApp_Arango.Schema
{
    public static class ArangoSchema
    {
        public static readonly List<Collection> Collections = new List<Collection>
        {
            // Identity Framework
            new Collection("Users"),
            new Collection("Roles"),
            new Collection("UserLogins"),
            new Collection("UserClaims"),
            new Collection("RoleClaims"),
            new Collection("UserTokens"),
            // Custom Data
            new Collection("RPSchedule"),
            new Collection("FeaturedArticles"),
            new Collection("QuestList"),
            new Collection("NPCDescriptions"),
            new Collection("CharacterSheets"),
            new Collection("PrimaryTraits"),
            new Collection("EquipmentTraits"),
            new Collection("Equipment"),
            new Collection("TemporaryTraits"),
            new Collection("OriginTraits"),
            new Collection("Languages"),
            new Collection("VisionTypes"),
            new Collection("GMRequests"),
            new Collection("DieRolls"),
            new Collection("NPCApplications"),
            new Collection("MissionApplications"),
            new Collection("PlayerApplications"),
            new Collection("VisitorPass"),
            // Arango specific collections
            new Collection("edges", type: ArangoDBNetStandard.CollectionApi.Models.CollectionType.Edge)
        };

        public static readonly List<Graph> Graphs = new List<Graph>
        {
            new Graph("PrimaryGraph", new List<ArangoDBNetStandard.GraphApi.Models.EdgeDefinition>
            {
                // Identity Relationships
                // UserRoles Users->Roles
                new ArangoDBNetStandard.GraphApi.Models.EdgeDefinition{
                    From = new List<string>
                    {
                        "Users"
                    },
                    To = new List<string>
                    {
                        "Roles"
                    },
                    Collection = Collections.Last().Name
                },

                // UserRoles Roles->Users
                new ArangoDBNetStandard.GraphApi.Models.EdgeDefinition
                {
                    Collection = Collections.Last().Name,
                    From = new List<string>
                    {
                        "Roles"
                    },
                    To = new List<string>
                    {
                        "Users"
                    }
                },

                // Users -> UserLogins
                new ArangoDBNetStandard.GraphApi.Models.EdgeDefinition{
                    Collection = Collections.Last().Name,
                    From =  new List<string>
                    {
                        "Users"
                    },
                    To = new List<string>
                    {
                        "UserLogins"
                    }
                },

                // Users -> UserClaims
                new ArangoDBNetStandard.GraphApi.Models.EdgeDefinition{
                    Collection = Collections.Last().Name,
                    From = new List<string>
                    {
                        "Users"
                    },
                    To = new List<string>
                    {
                        "UserClaims"
                    }
                },

                // Users -> UserTokens
                new ArangoDBNetStandard.GraphApi.Models.EdgeDefinition{
                    Collection = Collections.Last().Name,
                    From = new List<string>
                    {
                        "Users"
                    },
                    To = new List<string>
                    {
                        "UserTokens"
                    }
                },

                // Roles -> RoleClaims
                new ArangoDBNetStandard.GraphApi.Models.EdgeDefinition{
                    Collection = Collections.Last().Name,
                    From = new List<string>
                    {
                        "Roles"
                    },
                    To = new List<string>
                    {
                        "RoleClaims"
                    }
                },

                // Custom Relations
                // NPCDescriptions -> QuestList
                new ArangoDBNetStandard.GraphApi.Models.EdgeDefinition{
                    Collection = Collections.Last().Name,
                    From = new List<string>
                    {
                        "NPCDescriptions"
                    },
                    To = new List<string>
                    {
                        "QuestList"
                    }
                },

                // Users -> QuestList
                new ArangoDBNetStandard.GraphApi.Models.EdgeDefinition{
                    Collection = Collections.Last().Name,
                    From = new List<string>
                    {
                        "Users"
                    },
                    To = new List<string>
                    {
                        "QuestList"
                    }
                },

                // Users -> CharacterSheets
                new ArangoDBNetStandard.GraphApi.Models.EdgeDefinition{
                    Collection = Collections.Last().Name,
                    From = new List<string>
                    {
                        "Users"
                    },
                    To = new List<string>
                    {
                        "CharacterSheets"
                    }
                },

                // Users -> GMRequests
                new ArangoDBNetStandard.GraphApi.Models.EdgeDefinition{
                    Collection = Collections.Last().Name,
                    From = new List<string>
                    {
                        "Users"
                    },
                    To = new List<string>
                    {
                        "GMRequests"
                    }
                },

                // Users -> DieRolls
                new ArangoDBNetStandard.GraphApi.Models.EdgeDefinition{
                    Collection = Collections.Last().Name,
                    From = new List<string>
                    {
                        "Users"
                    },
                    To = new List<string>
                    {
                        "DieRolls"
                    }
                },

                // Users -> NPCApplications
                new ArangoDBNetStandard.GraphApi.Models.EdgeDefinition{
                    Collection = Collections.Last().Name,
                    From = new List<string>
                    {
                        "Users"
                    },
                    To = new List<string>
                    {
                        "NPCApplications"
                    }
                },

                // Users -> MissionApplications
                new ArangoDBNetStandard.GraphApi.Models.EdgeDefinition{
                    Collection = Collections.Last().Name,
                    From = new List<string>
                    {
                        "Users"
                    },
                    To = new List<string>
                    {
                       "MissionApplications"
                    }
                },

                // Users -> PlayerApplications
                new ArangoDBNetStandard.GraphApi.Models.EdgeDefinition(Collections.Last(), new List<Collection>
                {
                    Collections.Where(Collection => Collection.Name == "Users").FirstOrDefault()
                },
                new List<Collection>
                {
                    Collections.Where(Collection => Collection.Name == "PlayerApplications").FirstOrDefault()
                }),

                // Users -> VisitorPass
                new ArangoDBNetStandard.GraphApi.Models.EdgeDefinition(Collections.Last(), new List<Collection>
                {
                    Collections.Where(Collection => Collection.Name == "Users").FirstOrDefault()
                },
                new List<Collection>
                {
                    Collections.Where(Collection => Collection.Name == "VisitorPass").FirstOrDefault()
                }),

                // CharacterSheets -> PrimaryTraits
                new ArangoDBNetStandard.GraphApi.Models.EdgeDefinition(Collections.Last(), new List<Collection>
                {
                    Collections.Where(Collection => Collection.Name == "CharacterSheets").FirstOrDefault()
                },
                new List<Collection>
                {
                    Collections.Where(Collection => Collection.Name == "PrimaryTraits").FirstOrDefault()
                }),

                // PlayerApplications -> CharacterSheets
                new ArangoDBNetStandard.GraphApi.Models.EdgeDefinition(Collections.Last(), new List<Collection>
                {
                    Collections.Where(Collection => Collection.Name == "PlayerApplications").FirstOrDefault()
                },
                new List<Collection>
                {
                    Collections.Where(Collection => Collection.Name == "CharacterSheets").FirstOrDefault()
                }),

                // CharacterSheets -> DieRolls
                new ArangoDBNetStandard.GraphApi.Models.EdgeDefinition(Collections.Last(), new List<Collection>
                {
                    Collections.Where(Collection => Collection.Name == "CharacterSheets").FirstOrDefault()
                },
                new List<Collection>
                {
                    Collections.Where(Collection => Collection.Name == "DieRolls").FirstOrDefault()
                }),

                // CharacterSheets -> MissionApplications
                new ArangoDBNetStandard.GraphApi.Models.EdgeDefinition(Collections.Last(), new List<Collection>
                {
                    Collections.Where(Collection => Collection.Name == "CharacterSheets").FirstOrDefault()
                },
                new List<Collection>
                {
                    Collections.Where(Collection => Collection.Name == "MissionApplications").FirstOrDefault()
                }),

                // MissionApplications -> CharacterSheets
                new ArangoDBNetStandard.GraphApi.Models.EdgeDefinition(Collections.Last(), new List<Collection>
                {
                    Collections.Where(Collection => Collection.Name == "MissionApplications").FirstOrDefault()
                },
                new List<Collection>
                {
                    Collections.Where(Collection => Collection.Name == "CharacterSheets").FirstOrDefault()
                }),

                // CharacterSheets -> EquipmentTraits
                new ArangoDBNetStandard.GraphApi.Models.EdgeDefinition(Collections.Last(), new List<Collection>
                {
                    Collections.Where(Collection => Collection.Name == "CharacterSheets").FirstOrDefault()
                },
                new List<Collection>
                {
                    Collections.Where(Collection => Collection.Name == "EquipmentTraits").FirstOrDefault()
                }),

                // CharacterSheets -> Equipment
                new ArangoDBNetStandard.GraphApi.Models.EdgeDefinition(Collections.Last(), new List<Collection>
                {
                    Collections.Where(Collection => Collection.Name == "CharacterSheets").FirstOrDefault()
                },
                new List<Collection>
                {
                    Collections.Where(Collection => Collection.Name == "Equipment").FirstOrDefault()
                }),

                // CharacterSheets -> TemporaryTraits
                new ArangoDBNetStandard.GraphApi.Models.EdgeDefinition(Collections.Last(), new List<Collection>
                {
                    Collections.Where(Collection => Collection.Name == "CharacterSheets").FirstOrDefault()
                },
                new List<Collection>
                {
                    Collections.Where(Collection => Collection.Name == "TemporaryTraits").FirstOrDefault()
                }),

                // CharacterSheets -> OriginTraits
                new ArangoDBNetStandard.GraphApi.Models.EdgeDefinition(Collections.Last(), new List<Collection>
                {
                    Collections.Where(Collection => Collection.Name == "CharacterSheets").FirstOrDefault()
                },
                new List<Collection>
                {
                    Collections.Where(Collection => Collection.Name == "OriginTraits").FirstOrDefault()
                }),

                // OriginTraits -> Languages
                new ArangoDBNetStandard.GraphApi.Models.EdgeDefinition(Collections.Last(), new List<Collection>
                {
                    Collections.Where(Collection => Collection.Name == "OriginTraits").FirstOrDefault()
                },
                new List<Collection>
                {
                    Collections.Where(Collection => Collection.Name == "Languages").FirstOrDefault()
                }),

                // OriginTraits -> VisionTypes
                new ArangoDBNetStandard.GraphApi.Models.EdgeDefinition(Collections.Last(), new List<Collection>
                {
                    Collections.Where(Collection => Collection.Name == "OriginTraits").FirstOrDefault()
                },
                new List<Collection>
                {
                    Collections.Where(Collection => Collection.Name == "VisionTypes").FirstOrDefault()
                })
            })
        };
    }
    public class Documents
    {
        // Identity Documents
        public class Users
        {
            public Users(string userName, int accessFailedCount = 0, bool emailConfirmed = false, bool lockoutEnabled = false, bool phoneNumberConfirmed = false,
                bool twoFactorEnabled = false)
            {
                AccessFailedCount = accessFailedCount;
                EmailConfirmed = emailConfirmed;
                LockoutEnabled = lockoutEnabled;
                PhoneNumberConfirmed = phoneNumberConfirmed;
                TwoFactorEnabled = twoFactorEnabled;
                UserName = userName;
            }
            public int AccessFailedCount { get; set; }
            public string? ConcurrencyStamp { get; set; }
            public string? Email { get; set; }
            public bool EmailConfirmed { get; set; }
            public bool LockoutEnabled { get; set; }
            public DateTime? LockoutEnd { get; set; }
            public string? NormalizedEmail { get; set; }
            public string? NormalizedUserName { get; set; }
            public string? PasswordHash { get; set; }
            public string? PhoneNumber { get; set; }
            public bool PhoneNumberConfirmed { get; set; }
            public string? SecurityStamp { get; set; }
            public bool TwoFactorEnabled { get; set; }
            public string UserName { get; set; }
            public string? DiscordUserName { get; set; }
        }

        public class Roles
        {
            public Roles()
            {

            }

            public string? ConcurrencyStamp { get; set; }
            public string? Name { get; set; }
            public string? NormalizedName { get; set; }
        }

        public class UserLogins
        {
            public UserLogins(string loginProvder, string providerKey, string userId)
            {
                LoginProvider = loginProvder;
                ProviderKey = providerKey;
                UserId = userId;
            }

            public string LoginProvider { get; set; }
            public string ProviderKey { get; set; }
            public string? ProviderDisplayName { get; set; }
            public string UserId { get; set; }
        }

        public class UserClaims
        {
            public UserClaims(string userid)
            {
                UserId = userid;
            }

            public string? ClaimType { get; set; }
            public string? ClaimValue { get; set; }
            public string UserId { get; set; }
        }

        public class RoleClaims
        {
            public RoleClaims(string roleid)
            {
                RoleId = roleid;
            }

            public string? ClaimType { get; set; }
            public string? ClaimValue { get; set; }
            public string RoleId { get; set; }
        }

        public class UserTokens
        {
            public UserTokens(string userId, string loginProvider, string name)
            {
                UserId = userId;
                LoginProvider = loginProvider;
                Name = name;
            }

            public string UserId { get; set; }
            public string LoginProvider { get; set; }
            public string Name { get; set; }
            public string? Value { get; set; }
        }

        // custom documents
        public class RPSchedule
        {
            public RPSchedule() { }

            public int? LobbyStatus { get; set; }
            public DateTime? StartDate { get; set; }
            public DateTime? EndDate { get; set; }
        }

        public class FeaturedArticles
        {
            public FeaturedArticles(bool isFeatured = false)
            {
                IsFeatured = isFeatured;
            }

            public string? ArticleUrl { get; set; }
            public bool IsFeatured { get; set; }
        }

        public class QuestList
        {
            public QuestList(string authorId, string associatedNPC, bool isActive = false)
            {
                AuthorId = authorId;
                AssociatedNPC = associatedNPC;
                IsActive = isActive;
            }

            public string AuthorId { get; set; }
            public string? Name { get; set; }
            public string? BriefDescription { get; set; }
            public string? Description { get; set; }
            public string? RewardDescription { get; set; }
            public string? EnemyDescription { get; set; }
            public string AssociatedNPC { get; set; }
            public bool IsActive { get; set; }
        }

        public class NPCDescriptions
        {
            public NPCDescriptions(bool isVisible = false, bool isRecurring = false, bool isGMNPC = false)
            {
                IsVisible = isVisible;
                IsRecurring = isRecurring;
                IsGMNPC = isGMNPC;
            }

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
            public string? AvatarUrl { get; set; }
        }

        public class CharacterSheets
        {
            public CharacterSheets(string userId, bool isVisible = false, bool previouslyNpc = false, bool isApproved = false, bool isReviewed = false)
            {
                UserId = userId;
                IsVisible = isVisible;
                PreviouslyNPC = previouslyNpc;
                IsApproved = isApproved;
                IsReviewed = isReviewed;
            }

            public string UserId { get; set; }
            public string? Name { get; set; }
            public string? Occupation { get; set; }
            public string? OccupationDescription { get; set; }
            public string? Biograph { get; set; }
            public string? PhysicalDescription { get; set; }
            public int? Mind { get; set; }
            public int? Strength { get; set; }
            public int? Agility { get; set; }
            public int? Constitution { get; set; }
            public int? Soul { get; set; }
            public int? TraitPoints { get; set; }
            public int? EnergyPoints { get; set; }
            public bool IsVisible { get; set; }
            public bool PreviouslyNPC { get; set; }
            public string? AvatarUrl { get; set; }
            public bool IsApproved { get; set; }
            public bool IsReviewed { get; set; }
            public DateTime? LastChanged { get; set; }
        }

        public class PrimaryTraits
        {
            public PrimaryTraits(string characterId, bool isVisible = false, bool isPassive = false, bool isMajor = false, bool isEdge = false)
            {
                CharacterId = characterId;
                IsVisible = isVisible;
                IsPassive = IsPassive;
                IsMajor = isMajor;
                IsEdge = isEdge;
            }

            public string CharacterId { get; set; }
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
            public EquipmentTraits(string characterId, bool isVisible = false, bool isPassive = false)
            {
                CharacterId = characterId;
                IsVisible = isVisible;
                IsPassive = IsPassive;
            }

            public string CharacterId { get; set; }
            public string? Name { get; set; }
            public bool IsVisible { get; set; }
            public string? Description { get; set; }
            public bool IsPassive { get; set; }
            public int? EnergyCost { get; set; }
            public int? AbilityPointCost { get; set; }
        }

        public class OriginTraits
        {
            public OriginTraits(string characterId, bool isVisible = false)
            {
                CharacterId = characterId;
                IsVisible = isVisible;
            }

            public string CharacterId { get; set; }
            public string? Name { get; set; }
            public bool IsVisible { get; set; }
            public string? Description { get; set; }
        }

        public class Languages
        {
            public Languages(string traitId)
            {
                TraitId = traitId;
            }

            public string TraitId { get; set; }
            public string? Name { get; set; }
            public string? Description { get; set; }
        }

        public class VisionTypes
        {
            public VisionTypes(string traitId)
            {
                TraitId = traitId;
            }

            public string TraitId { get; set; }
            public string? Name { get; set; }
            public string? Description { get; set; }
        }

        public class TemporaryTraits
        {
            public TemporaryTraits(string characterId, bool isVisible = false, bool isPassive = false)
            {
                CharacterId = characterId;
                IsVisible = isVisible;
                IsPassive = isPassive;
            }

            public string CharacterId { get; set; }
            public string? Name { get; set; }
            public bool IsVisible { get; set; }
            public string? Description { get; set; }
            public bool IsPassive { get; set; }
            public int? EnergyCost { get; set; }
        }

        public class Equipment
        {
            public Equipment(string characterId)
            {
                CharacterId = characterId;
            }

            public string CharacterId { get; set; }
            public string? Name { get; set; }
            public string? Description { get; set; }
            public int? Cost { get; set; }
            public decimal? Weight { get; set; }
        }

        public class GMRequests
        {
            public GMRequests(string requesterId, bool isComplete = false, bool isRelayed = false)
            {
                RequesterId = requesterId;
                IsComplete = isComplete;
                IsRelayed = isRelayed;
            }

            public string RequesterId { get; set; }
            public bool IsComplete { get; set; }
            public bool IsRelayed { get; set; }
            public DateTime? Timestamp { get; set; }
            public string? RequesterLocation { get; set; }
            public string? RequestReason { get; set; }
            public int? Urgency { get; set; }
        }

        public class DieRolls
        {
            public DieRolls(string userId, bool isAdvantage = false, bool isDisadvantage = false)
            {
                UserId = userId;
                IsAdvantage = isAdvantage;
                IsDisadvantage = isDisadvantage;
            }

            public string UserId { get; set; }
            public DateTime? Timestamp { get; set; }
            public int? NumOfRolls { get; set; }
            public int? DieType { get; set; }
            public int? GMRollModifier { get; set; }
            public int? CharacterAbility { get; set; }
            public string? SourceCharacterId { get; set; }
            public bool IsAdvantage { get; set; }
            public bool IsDisadvantage { get; set; }
            public int? Result { get; set; }
            public string? DetailedResult { get; set; }
        }

        public class NPCApplications
        {
            public NPCApplications(string userId, bool isApproved = false, bool isDenied = false, bool isUnderReview = false)
            {
                UserId = userId;
                IsApproved = isApproved;
                IsDenied = isDenied;
                IsUnderReview = isUnderReview;
            }

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

        public class MissionApplications
        {
            public MissionApplications(string userId, bool isApproved = false, bool isDenied = false, bool isUnderReview = false)
            {
                UserId = userId;
                IsApproved = isApproved;
                IsDenied = isDenied;
                IsUnderReview = isUnderReview;
            }

            public string UserId { get; set; }
            public bool IsApproved { get; set; }
            public bool IsDenied { get; set; }
            public bool IsUnderReview { get; set; }
            public string? RelevantExteriorGM { get; set; }
            public string? InformationSought { get; set; }
            public string? TeamDescription { get; set; }
        }

        public class PlayerApplications
        {
            public PlayerApplications(string userId, bool isApproved = false, bool isDenied = false, bool isUnderReview = false)
            {
                UserId = userId;
                IsApproved = isApproved;
                IsDenied = isDenied;
                IsUnderReview = isUnderReview;
            }

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

        public class VisitorPass
        {
            public VisitorPass(string userId, bool isApproved = false)
            {
                UserId = userId;
                IsApproved = isApproved;
            }

            public string UserId { get; set; }
            public bool IsApproved { get; set; }
            public string? CharacterId { get; set; }
            public DateTime? SessionDate { get; set; }
            public int? entryRank { get; set; }
            public DateTime? ValidStart { get; set; }
            public DateTime? ValidEnd { get; set; }
        }
    }
}

