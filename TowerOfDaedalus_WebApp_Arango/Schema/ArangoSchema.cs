using System;
using System.Collections.Generic;
using System.Linq;
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
            new Graph("PrimaryGraph", new List<EdgeDefinition>
            {
                // Identity Relationships
                // UserRoles Users->Roles
                new EdgeDefinition(Collections.Last(), new List<Collection>
                {
                    Collections.Where(Collection => Collection.Name == "Users").FirstOrDefault()
                },
                new List<Collection>
                {
                    Collections.Where(Collection => Collection.Name == "Roles").FirstOrDefault()
                }),

                // UserRoles Roles->Users
                new EdgeDefinition(Collections.Last(), new List<Collection>
                {
                    Collections.Where(Collection => Collection.Name == "Roles").FirstOrDefault()
                },
                new List<Collection>
                {
                    Collections.Where(Collection => Collection.Name == "Users").FirstOrDefault()
                }),

                // Users -> UserLogins
                new EdgeDefinition(Collections.Last(), new List<Collection>
                {
                    Collections.Where(Collection => Collection.Name == "Users").FirstOrDefault()
                },
                new List<Collection>
                {
                    Collections.Where(Collection => Collection.Name == "UserLogins").FirstOrDefault()
                }),

                // Users -> UserClaims
                new EdgeDefinition(Collections.Last(), new List<Collection>
                {
                    Collections.Where(Collection => Collection.Name == "Users").FirstOrDefault()
                },
                new List<Collection>
                {
                    Collections.Where(Collection => Collection.Name == "UserClaims").FirstOrDefault()
                }),

                // Users -> UserTokens
                new EdgeDefinition(Collections.Last(), new List<Collection>
                {
                    Collections.Where(Collection => Collection.Name == "Users").FirstOrDefault()
                },
                new List<Collection>
                {
                    Collections.Where(Collection => Collection.Name == "UserTokens").FirstOrDefault()
                }),

                // Roles -> RoleClaims
                new EdgeDefinition(Collections.Last(), new List<Collection>
                {
                    Collections.Where(Collection => Collection.Name == "Roles").FirstOrDefault()
                },
                new List<Collection>
                {
                    Collections.Where(Collection => Collection.Name == "RoleClaims").FirstOrDefault()
                }),

                // Custom Relations
                // NPCDescriptions -> QuestList
                new EdgeDefinition(Collections.Last(), new List<Collection>
                {
                    Collections.Where(Collection => Collection.Name == "NPCDescriptions").FirstOrDefault()
                },
                new List<Collection>
                {
                    Collections.Where(Collection => Collection.Name == "QuestList").FirstOrDefault()
                }),

                // Users -> QuestList
                new EdgeDefinition(Collections.Last(), new List<Collection>
                {
                    Collections.Where(Collection => Collection.Name == "Users").FirstOrDefault()
                },
                new List<Collection>
                {
                    Collections.Where(Collection => Collection.Name == "QuestList").FirstOrDefault()
                }),

                // Users -> CharacterSheets
                new EdgeDefinition(Collections.Last(), new List<Collection>
                {
                    Collections.Where(Collection => Collection.Name == "Users").FirstOrDefault()
                },
                new List<Collection>
                {
                    Collections.Where(Collection => Collection.Name == "CharacterSheets").FirstOrDefault()
                }),

                // Users -> GMRequests
                new EdgeDefinition(Collections.Last(), new List<Collection>
                {
                    Collections.Where(Collection => Collection.Name == "Users").FirstOrDefault()
                },
                new List<Collection>
                {
                    Collections.Where(Collection => Collection.Name == "GMRequests").FirstOrDefault()
                }),

                // Users -> DieRolls
                new EdgeDefinition(Collections.Last(), new List<Collection>
                {
                    Collections.Where(Collection => Collection.Name == "Users").FirstOrDefault()
                },
                new List<Collection>
                {
                    Collections.Where(Collection => Collection.Name == "DieRolls").FirstOrDefault()
                }),

                // Users -> NPCApplications
                new EdgeDefinition(Collections.Last(), new List<Collection>
                {
                    Collections.Where(Collection => Collection.Name == "Users").FirstOrDefault()
                },
                new List<Collection>
                {
                    Collections.Where(Collection => Collection.Name == "NPCApplications").FirstOrDefault()
                }),

                // Users -> MissionApplications
                new EdgeDefinition(Collections.Last(), new List<Collection>
                {
                    Collections.Where(Collection => Collection.Name == "Users").FirstOrDefault()
                },
                new List<Collection>
                {
                    Collections.Where(Collection => Collection.Name == "MissionApplications").FirstOrDefault()
                }),

                // Users -> PlayerApplications
                new EdgeDefinition(Collections.Last(), new List<Collection>
                {
                    Collections.Where(Collection => Collection.Name == "Users").FirstOrDefault()
                },
                new List<Collection>
                {
                    Collections.Where(Collection => Collection.Name == "PlayerApplications").FirstOrDefault()
                }),

                // Users -> VisitorPass
                new EdgeDefinition(Collections.Last(), new List<Collection>
                {
                    Collections.Where(Collection => Collection.Name == "Users").FirstOrDefault()
                },
                new List<Collection>
                {
                    Collections.Where(Collection => Collection.Name == "VisitorPass").FirstOrDefault()
                }),

                // CharacterSheets -> PrimaryTraits
                new EdgeDefinition(Collections.Last(), new List<Collection>
                {
                    Collections.Where(Collection => Collection.Name == "CharacterSheets").FirstOrDefault()
                },
                new List<Collection>
                {
                    Collections.Where(Collection => Collection.Name == "PrimaryTraits").FirstOrDefault()
                }),

                // PlayerApplications -> CharacterSheets
                new EdgeDefinition(Collections.Last(), new List<Collection>
                {
                    Collections.Where(Collection => Collection.Name == "PlayerApplications").FirstOrDefault()
                },
                new List<Collection>
                {
                    Collections.Where(Collection => Collection.Name == "CharacterSheets").FirstOrDefault()
                }),

                // CharacterSheets -> DieRolls
                new EdgeDefinition(Collections.Last(), new List<Collection>
                {
                    Collections.Where(Collection => Collection.Name == "CharacterSheets").FirstOrDefault()
                },
                new List<Collection>
                {
                    Collections.Where(Collection => Collection.Name == "DieRolls").FirstOrDefault()
                }),

                // CharacterSheets -> MissionApplications
                new EdgeDefinition(Collections.Last(), new List<Collection>
                {
                    Collections.Where(Collection => Collection.Name == "CharacterSheets").FirstOrDefault()
                },
                new List<Collection>
                {
                    Collections.Where(Collection => Collection.Name == "MissionApplications").FirstOrDefault()
                }),

                // MissionApplications -> CharacterSheets
                new EdgeDefinition(Collections.Last(), new List<Collection>
                {
                    Collections.Where(Collection => Collection.Name == "MissionApplications").FirstOrDefault()
                },
                new List<Collection>
                {
                    Collections.Where(Collection => Collection.Name == "CharacterSheets").FirstOrDefault()
                }),

                // CharacterSheets -> EquipmentTraits
                new EdgeDefinition(Collections.Last(), new List<Collection>
                {
                    Collections.Where(Collection => Collection.Name == "CharacterSheets").FirstOrDefault()
                },
                new List<Collection>
                {
                    Collections.Where(Collection => Collection.Name == "EquipmentTraits").FirstOrDefault()
                }),

                // CharacterSheets -> Equipment
                new EdgeDefinition(Collections.Last(), new List<Collection>
                {
                    Collections.Where(Collection => Collection.Name == "CharacterSheets").FirstOrDefault()
                },
                new List<Collection>
                {
                    Collections.Where(Collection => Collection.Name == "Equipment").FirstOrDefault()
                }),

                // CharacterSheets -> TemporaryTraits
                new EdgeDefinition(Collections.Last(), new List<Collection>
                {
                    Collections.Where(Collection => Collection.Name == "CharacterSheets").FirstOrDefault()
                },
                new List<Collection>
                {
                    Collections.Where(Collection => Collection.Name == "TemporaryTraits").FirstOrDefault()
                }),

                // CharacterSheets -> OriginTraits
                new EdgeDefinition(Collections.Last(), new List<Collection>
                {
                    Collections.Where(Collection => Collection.Name == "CharacterSheets").FirstOrDefault()
                },
                new List<Collection>
                {
                    Collections.Where(Collection => Collection.Name == "OriginTraits").FirstOrDefault()
                }),

                // OriginTraits -> Languages
                new EdgeDefinition(Collections.Last(), new List<Collection>
                {
                    Collections.Where(Collection => Collection.Name == "OriginTraits").FirstOrDefault()
                },
                new List<Collection>
                {
                    Collections.Where(Collection => Collection.Name == "Languages").FirstOrDefault()
                }),

                // OriginTraits -> VisionTypes
                new EdgeDefinition(Collections.Last(), new List<Collection>
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
            public Users(string userName)
            {
                AccessFailedCount = 0;
                EmailConfirmed = false;
                LockoutEnabled = false;
                PhoneNumberConfirmed = false;
                TwoFactorEnabled = false;
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
            public string RoleId { get; set;}
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
            public FeaturedArticles(bool isFeatured)
            {
                IsFeatured = isFeatured;
            }

            public string? ArticleUrl { get; set; }
            public bool IsFeatured { get; set; }
        }

        public class QuestList
        {
            public QuestList(string authorId, string associatedNPC, bool isActive)
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
            public NPCDescriptions(bool isVisible, bool isRecurring, bool isGMNPC)
            {

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
    }
}

