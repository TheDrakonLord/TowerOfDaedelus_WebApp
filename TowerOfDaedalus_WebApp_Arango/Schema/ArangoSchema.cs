using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using ArangoDBNetStandard;
using Microsoft.AspNet.Identity;

namespace TowerOfDaedalus_WebApp_Arango.Schema
{
    /// <summary>
    /// Full arango schema definition to assist with creating the database
    /// </summary>
    public static class ArangoSchema
    {
        /// <summary>
        /// list of collections to be added
        /// </summary>
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

        /// <summary>
        /// list of graphs to be added
        /// </summary>
        public static readonly List<Graph> Graphs = new List<Graph>
        {
            new Graph("PrimaryGraph", new List<ArangoDBNetStandard.GraphApi.Models.EdgeDefinition>
            {
                // Identity Relationships
                // UserRoles Users->Roles
                new ArangoDBNetStandard.GraphApi.Models.EdgeDefinition
                {
                    Collection = Collections.Last().Name,
                    From = new List<string>
                    {
                        "Users"
                    },
                    To = new List<string>
                    {
                        "Roles"
                    }                    
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
                new ArangoDBNetStandard.GraphApi.Models.EdgeDefinition
                {
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
                new ArangoDBNetStandard.GraphApi.Models.EdgeDefinition
                {
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
                new ArangoDBNetStandard.GraphApi.Models.EdgeDefinition
                {
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
                new ArangoDBNetStandard.GraphApi.Models.EdgeDefinition
                {
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
                new ArangoDBNetStandard.GraphApi.Models.EdgeDefinition
                {
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
                new ArangoDBNetStandard.GraphApi.Models.EdgeDefinition
                {
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
                new ArangoDBNetStandard.GraphApi.Models.EdgeDefinition
                {
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
                new ArangoDBNetStandard.GraphApi.Models.EdgeDefinition
                {
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
                new ArangoDBNetStandard.GraphApi.Models.EdgeDefinition
                {
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
                new ArangoDBNetStandard.GraphApi.Models.EdgeDefinition
                {
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
                new ArangoDBNetStandard.GraphApi.Models.EdgeDefinition
                {
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
                new ArangoDBNetStandard.GraphApi.Models.EdgeDefinition
                {
                    Collection = Collections.Last().Name,
                    From = new List<string>
                    {
                        "Users"
                    },
                    To = new List<string>
                    {
                        "PlayerApplications"
                    }
                },

                // Users -> VisitorPass
                new ArangoDBNetStandard.GraphApi.Models.EdgeDefinition
                {
                    Collection = Collections.Last().Name,
                    From = new List<string>
                    {
                        "Users"
                    },
                    To = new List<string>
                    {
                        "VisitorPass"
                    }
                },

                // CharacterSheets -> PrimaryTraits
                new ArangoDBNetStandard.GraphApi.Models.EdgeDefinition
                {
                    Collection = Collections.Last().Name,
                    From = new List<string>
                    {
                        "CharacterSheets"
                    },
                    To = new List<string>
                    {
                        "PrimaryTraits"
                    }
                },

                // PlayerApplications -> CharacterSheets
                new ArangoDBNetStandard.GraphApi.Models.EdgeDefinition
                {
                    Collection = Collections.Last().Name,
                    From = new List<string>
                    {
                        "PlayerApplications"
                    },
                    To = new List<string>
                    {
                        "CharacterSheets"
                    }
                },

                // CharacterSheets -> DieRolls
                new ArangoDBNetStandard.GraphApi.Models.EdgeDefinition
                {
                    Collection = Collections.Last().Name, 
                    From = new List<string>
                    {
                        "CharacterSheets"
                    },
                    To = new List<string>
                    {
                        "DieRolls"
                    }
                },

                // CharacterSheets -> MissionApplications
                new ArangoDBNetStandard.GraphApi.Models.EdgeDefinition
                {
                    Collection = Collections.Last().Name,
                    From = new List<string>
                    {
                        "CharacterSheets"
                    },
                    To = new List<string>
                    {
                        "MissionApplications"
                    }
                },

                // MissionApplications -> CharacterSheets
                new ArangoDBNetStandard.GraphApi.Models.EdgeDefinition
                {
                    Collection = Collections.Last().Name, 
                    From = new List<string>
                    {
                        "MissionApplications"
                    },
                    To = new List<string>
                    {
                        "CharacterSheets"
                    }
                }, 

                // CharacterSheets -> EquipmentTraits
                new ArangoDBNetStandard.GraphApi.Models.EdgeDefinition
                {
                    Collection = Collections.Last().Name,
                    From = new List<string>
                    {
                        "CharacterSheets"
                    },
                    To = new List<string>
                    {
                        "EquipmentTraits"
                    }
                },

                // CharacterSheets -> Equipment
                new ArangoDBNetStandard.GraphApi.Models.EdgeDefinition
                {
                    Collection = Collections.Last().Name,
                    From = new List<string>
                    {
                        "CharacterSheets"
                    },
                    To = new List<string>
                    {
                        "Equipment"
                    }
                },

                // CharacterSheets -> TemporaryTraits
                new ArangoDBNetStandard.GraphApi.Models.EdgeDefinition
                {
                    Collection = Collections.Last().Name,
                    From = new List<string>
                    {
                        "CharacterSheets"
                    },
                    To = new List<string>
                    {
                        "TemporaryTraits"
                    }
                },

                // CharacterSheets -> OriginTraits
                new ArangoDBNetStandard.GraphApi.Models.EdgeDefinition
                {
                    Collection = Collections.Last().Name,
                    From = new List<string>
                    {
                        "CharacterSheets"
                    },
                    To = new List<string>
                    {
                        "OriginTraits"
                    }
                },

                // OriginTraits -> Languages
                new ArangoDBNetStandard.GraphApi.Models.EdgeDefinition
                {
                    Collection = Collections.Last().Name,
                    From = new List<string>
                    {
                        "OriginTraits"
                    },
                    To = new List<string>
                    {
                        "Languages"
                    }
                },

                // OriginTraits -> VisionTypes
                new ArangoDBNetStandard.GraphApi.Models.EdgeDefinition
                {
                    Collection = Collections.Last().Name,
                    From = new List<string>
                    {
                        "OriginTraits"
                    },
                    To = new List<string>
                    {
                        "VisionTypes"
                    }
                }
            })
        };
    }

    /// <summary>
    /// class defining documents that can be added to arango
    /// </summary>
    public class Documents
    {
        // Identity Documents
        /// <summary>
        /// AspNetCore IUser Implementation.
        /// Defines data to be stored as a user document
        /// </summary>
        public class Users :  IUser<string>
        {
            /// <summary>
            /// default constructor
            /// </summary>
            /// <param name="userName">the username for the user to log in with</param>
            /// <param name="accessFailedCount">the number of times the user has failed to log in</param>
            /// <param name="emailConfirmed">boolean indicating if the user has confirmed their email</param>
            /// <param name="lockoutEnabled">boolean indicating if the user can be locked out</param>
            /// <param name="phoneNumberConfirmed">boolean indicating if the user has confirmed their phone number</param>
            /// <param name="twoFactorEnabled">boolean if the user has enabled two factor authentication</param>
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

            /// <summary>
            /// the primary key automatically generated by arango
            /// </summary>
            public string? _key { get; set; }

            /// <summary>
            /// Alias for _key that enables the identity framework to reach it
            /// </summary>
            public string? Id { get { return _key; } set { _key = value;  } }

            /// <summary>
            /// the number of times the user has failed to log in
            /// </summary>
            public int AccessFailedCount { get; set; }

            /// <summary>
            /// value that should change each time the user data is updated
            /// </summary>
            public string? ConcurrencyStamp { get; set; }

            /// <summary>
            /// the user's email
            /// </summary>
            public string? Email { get; set; }

            /// <summary>
            /// whether or not the user has confirmed their email address
            /// </summary>
            public bool EmailConfirmed { get; set; }

            /// <summary>
            /// whether or not the user can be locked out
            /// </summary>
            public bool LockoutEnabled { get; set; }

            /// <summary>
            /// date and time the user will be unlocked
            /// if in the past, the user is unlocked
            /// </summary>
            public DateTime? LockoutEnd { get; set; }

            /// <summary>
            /// the user's normalized email address
            /// </summary>
            public string? NormalizedEmail { get; set; }

            /// <summary>
            /// the user's normalized user name
            /// </summary>
            public string? NormalizedUserName { get; set; }

            /// <summary>
            /// the user's hashed password
            /// </summary>
            public string? PasswordHash { get; set; }

            /// <summary>
            /// the user's phone number
            /// </summary>
            public string? PhoneNumber { get; set; }

            /// <summary>
            /// whether or not the user has confirmed their phone number
            /// </summary>
            public bool PhoneNumberConfirmed { get; set; }

            /// <summary>
            /// 
            /// </summary>
            public string? SecurityStamp { get; set; }

            /// <summary>
            /// whether or not the user has enabled two factor authentication
            /// </summary>
            public bool TwoFactorEnabled { get; set; }

            /// <summary>
            /// the user's user name
            /// </summary>
            public string UserName { get; set; }

            /// <summary>
            /// the user's discord user name
            /// </summary>
            public string? DiscordUserName { get; set; }
        }

        /// <summary>
        /// AspNetCore Identity IRole implementation
        /// defines roles that users can be members of
        /// </summary>
        public class Roles : IRole<string>
        {
            /// <summary>
            /// Default constructor
            /// </summary>
            public Roles()
            {
                // do nothing
            }

            /// <summary>
            /// primary key automatically generated by Arango
            /// </summary>
            public string? _key { get; set; }

            /// <summary>
            /// random value that should change any time the role is updated
            /// </summary>
            public string? ConcurrencyStamp { get; set; }

            /// <summary>
            /// The name of the role
            /// </summary>
            public string? Name { get; set; }

            /// <summary>
            /// the normalized name of the role
            /// </summary>
            public string? NormalizedName { get; set; }

            /// <summary>
            /// Alias for _key that allows the identity framework to access it
            /// </summary>
            public string? Id { get { return _key; } set { _key = value; } }
        }

        /// <summary>
        /// Represents each time a user logs in
        /// </summary>
        public class UserLogins
        {
            /// <summary>
            /// default constructor
            /// </summary>
            /// <param name="loginProvder">the login provider the user logged in with</param>
            /// <param name="providerKey">the key provided by the login provider</param>
            /// <param name="userId">the user's user id (the _key value of the user document)</param>
            public UserLogins(string loginProvder, string providerKey, string userId)
            {
                LoginProvider = loginProvder;
                ProviderKey = providerKey;
                UserId = userId;
            }

            /// <summary>
            /// the primary key automatically generated by arango
            /// </summary>
            public string? _key { get; set; }

            /// <summary>
            /// the login provider the user logged in with
            /// </summary>
            public string LoginProvider { get; set; }

            /// <summary>
            /// the key provided by the login provider
            /// </summary>
            public string ProviderKey { get; set; }
            
            /// <summary>
            /// the display name of the provider the user logged in with
            /// </summary>
            public string? ProviderDisplayName { get; set; }

            /// <summary>
            /// the user's user Id
            /// </summary>
            public string UserId { get; set; }
        }

        /// <summary>
        /// Claims that a user has
        /// </summary>
        public class UserClaims 
        {
            /// <summary>
            /// Default constructor
            /// </summary>
            /// <param name="userid">the user's user id (the _key value in the user document)</param>
            public UserClaims(string userid)
            {
                UserId = userid;
            }

            /// <summary>
            /// the primary key automatically genrated by Arango
            /// </summary>
            public string? _key { get; set; }

            /// <summary>
            /// the type of claim
            /// </summary>
            public string? ClaimType { get; set; }

            /// <summary>
            /// the value of the claim
            /// </summary>
            public string? ClaimValue { get; set; }

            /// <summary>
            /// the user's user id
            /// </summary>
            public string UserId { get; set; }
        }

        /// <summary>
        /// Claims that a role has
        /// </summary>
        public class RoleClaims
        {
            /// <summary>
            /// default constructor
            /// </summary>
            /// <param name="roleid">the role's primary key</param>
            public RoleClaims(string roleid)
            {
                RoleId = roleid;
            }

            /// <summary>
            /// the primary key automatically generated by arango
            /// </summary>
            public string? _key { get; set; }

            /// <summary>
            /// the type of claim
            /// </summary>
            public string? ClaimType { get; set; }

            /// <summary>
            /// the value of the claim
            /// </summary>
            public string? ClaimValue { get; set; }

            /// <summary>
            /// the Role's primary key
            /// </summary>
            public string RoleId { get; set; }
        }

        /// <summary>
        /// Tokens generated when a user logs in with a supported third party
        /// </summary>
        public class UserTokens
        {
            /// <summary>
            /// Default constructor
            /// </summary>
            /// <param name="userId">the primary key of the user that logged in</param>
            /// <param name="loginProvider">the login provider that the user logged in with</param>
            /// <param name="name">the name of the login provider</param>
            public UserTokens(string userId, string loginProvider, string name)
            {
                UserId = userId;
                LoginProvider = loginProvider;
                Name = name;
            }

            /// <summary>
            /// primary key automatically generated by arango
            /// </summary>
            public string? _key { get; set; }

            /// <summary>
            /// the user ID of the user that logged in
            /// </summary>
            public string UserId { get; set; }

            /// <summary>
            /// the login provider that the user logged in with
            /// </summary>
            public string LoginProvider { get; set; }

            /// <summary>
            /// The name of the login provider
            /// </summary>
            public string Name { get; set; }

            /// <summary>
            /// the value provided by the login provider
            /// </summary>
            public string? Value { get; set; }
        }

        // custom documents
        /// <summary>
        /// Represents a scheduled state of the RP lobby
        /// </summary>
        public class RPSchedule
        {
            /// <summary>
            /// default constructor
            /// </summary>
            public RPSchedule() { }

            /// <summary>
            /// the primary key automatically generated by arango
            /// </summary>
            public string? _key { get; set; }

            /// <summary>
            /// the value of the Enumeration representing the lobby status
            /// </summary>
            public int? LobbyStatus { get; set; }

            /// <summary>
            /// the date and time the status begins
            /// </summary>
            public DateTime? StartDate { get; set; }

            /// <summary>
            /// the date and time the status ends
            /// </summary>
            public DateTime? EndDate { get; set; }
        }

        /// <summary>
        /// Represents an article to be featured on the home page
        /// </summary>
        public class FeaturedArticles
        {
            /// <summary>
            /// default constructor
            /// </summary>
            /// <param name="isFeatured">whether or not the article is currently featured</param>
            public FeaturedArticles(bool isFeatured = false)
            {
                IsFeatured = isFeatured;
            }

            /// <summary>
            /// the primary key automatically generated by arango
            /// </summary>
            public string? _key { get; set; }

            /// <summary>
            /// the Url of the article
            /// </summary>
            public string? ArticleUrl { get; set; }

            /// <summary>
            /// whether or not the article is currently featured
            /// </summary>
            public bool IsFeatured { get; set; }
        }

        /// <summary>
        /// Represents a quest to be listed on the website
        /// </summary>
        public class QuestList
        {
            /// <summary>
            /// default constructor
            /// </summary>
            /// <param name="authorId">the primary key of the user who authored the quest</param>
            /// <param name="associatedNPC">the primary key of the NPC associated with the quest</param>
            /// <param name="isActive">whether or not the quest is currently active</param>
            public QuestList(string authorId, string associatedNPC, bool isActive = false)
            {
                AuthorId = authorId;
                AssociatedNPC = associatedNPC;
                IsActive = isActive;
            }

            /// <summary>
            /// the primary key automatically generated by arango
            /// </summary>
            public string? _key { get; set; }

            /// <summary>
            /// The primary key of the user who authored the quest
            /// </summary>
            public string AuthorId { get; set; }

            /// <summary>
            /// the name of the quest
            /// </summary>
            public string? Name { get; set; }

            /// <summary>
            /// a brief description of the quest
            /// </summary>
            public string? BriefDescription { get; set; }

            /// <summary>
            /// a full description of the quest
            /// </summary>
            public string? Description { get; set; }

            /// <summary>
            /// A description of the rewards offered for completing the quest
            /// </summary>
            public string? RewardDescription { get; set; }

            /// <summary>
            /// A description of enemies likely to be encountered during the quest
            /// </summary>
            public string? EnemyDescription { get; set; }

            /// <summary>
            /// the primary key of the NPC associated with the quest
            /// </summary>
            public string AssociatedNPC { get; set; }

            /// <summary>
            /// whether or not the quest is currently active
            /// </summary>
            public bool IsActive { get; set; }
        }

        /// <summary>
        /// represents a description of an NPC
        /// </summary>
        public class NPCDescriptions
        {
            /// <summary>
            /// default constructor
            /// </summary>
            /// <param name="isVisible">whether the NPC is publicly visible</param>
            /// <param name="isRecurring">whether or not the NPC is a recurring NPC</param>
            /// <param name="isGMNPC">whether or not the NPC is a GM NPC</param>
            public NPCDescriptions(bool isVisible = false, bool isRecurring = false, bool isGMNPC = false)
            {
                IsVisible = isVisible;
                IsRecurring = isRecurring;
                IsGMNPC = isGMNPC;
            }

            /// <summary>
            /// the primary key automatically generated by arango
            /// </summary>
            public string? _key { get; set; }

            /// <summary>
            /// the name of the NPC
            /// </summary>
            public string? Name { get; set; }

            /// <summary>
            /// the job title of the NPC
            /// </summary>
            public string? Title { get; set; }

            /// <summary>
            /// the occupation of the NPC
            /// </summary>
            public string? Occupation { get; set; }

            /// <summary>
            /// a description of the NPC's occupation
            /// </summary>
            public string? OccupationDescription { get; set; }

            /// <summary>
            /// A description of important events the NPC was involved in
            /// </summary>
            public string? ImportantEvents { get; set; }

            /// <summary>
            /// The npc's orgazational affiliation
            /// </summary>
            public string? Affiliation { get; set; }

            /// <summary>
            /// a description of the NPC's family
            /// </summary>
            public string? Family { get; set; }

            /// <summary>
            /// a brief description of the NPC
            /// </summary>
            public string? BriefDescription { get; set; }

            /// <summary>
            /// a full description of the NPC
            /// </summary>
            public string? Description { get; set; }

            /// <summary>
            /// Some trivia factoids about the NPC
            /// </summary>
            public string? Trivia { get; set; }

            /// <summary>
            /// whether or not the NPC is publicly visible
            /// </summary>
            public bool IsVisible { get; set; }

            /// <summary>
            /// whether or not the NPC is a recurring NPC
            /// </summary>
            public bool IsRecurring { get; set; }

            /// <summary>
            /// whether or not the NPC is a GM NPC
            /// </summary>
            public bool IsGMNPC { get; set; }

            /// <summary>
            /// The current player of the NPC
            /// </summary>
            public string? Player { get; set; }

            /// <summary>
            /// A url link to the image for the NPC's avatar
            /// </summary>
            public string? AvatarUrl { get; set; }
        }

        /// <summary>
        /// A character sheet for a player character
        /// </summary>
        public class CharacterSheets
        {
            /// <summary>
            /// default constuctor
            /// </summary>
            /// <param name="userId">the primary key of the user who owns the character</param>
            /// <param name="isVisible">whether or not the character sheet is publicly visible</param>
            /// <param name="previouslyNpc">whether or not the character was previously an NPC</param>
            /// <param name="isApproved">whether or not the character sheet is approved</param>
            /// <param name="isReviewed">whether or not the character sheet has been reviewed</param>
            public CharacterSheets(string userId, bool isVisible = false, bool previouslyNpc = false, bool isApproved = false, bool isReviewed = false)
            {
                UserId = userId;
                IsVisible = isVisible;
                PreviouslyNPC = previouslyNpc;
                IsApproved = isApproved;
                IsReviewed = isReviewed;
            }

            /// <summary>
            /// the primary key automatically generated by arango
            /// </summary>
            public string? _key { get; set; }

            /// <summary>
            /// the primary key of the user who owns the character sheet
            /// </summary>
            public string UserId { get; set; }

            /// <summary>
            /// the name of the character
            /// </summary>
            public string? Name { get; set; }

            /// <summary>
            /// the character's occupation
            /// </summary>
            public string? Occupation { get; set; }

            /// <summary>
            /// a description of the character's occupation
            /// </summary>
            public string? OccupationDescription { get; set; }

            /// <summary>
            /// A summary of the character's life history
            /// </summary>
            public string? Biography { get; set; }

            /// <summary>
            /// a description of the character's physical appearance
            /// </summary>
            public string? PhysicalDescription { get; set; }

            /// <summary>
            /// The character's Mind ability score
            /// </summary>
            public int? Mind { get; set; }

            /// <summary>
            /// the character's Strength ability score
            /// </summary>
            public int? Strength { get; set; }

            /// <summary>
            /// the character's Agility ability score
            /// </summary>
            public int? Agility { get; set; }

            /// <summary>
            /// the character's Constitution ability score
            /// </summary>
            public int? Constitution { get; set; }

            /// <summary>
            /// the character's Soul ability score
            /// </summary>
            public int? Soul { get; set; }

            /// <summary>
            /// the character's total used trait points
            /// </summary>
            public int? TraitPoints { get; set; }

            /// <summary>
            /// the character's total used energy points
            /// </summary>
            public int? EnergyPoints { get; set; }

            /// <summary>
            /// whether the character sheet is publicly visible
            /// </summary>
            public bool IsVisible { get; set; }

            /// <summary>
            /// whether or not the character was previously an NPC
            /// </summary>
            public bool PreviouslyNPC { get; set; }

            /// <summary>
            /// a Url link to an image to be used as the character's avatar
            /// </summary>
            public string? AvatarUrl { get; set; }

            /// <summary>
            /// whether or not the character sheet is approved
            /// </summary>
            public bool IsApproved { get; set; }

            /// <summary>
            /// whether or not the character sheet has been reviewed
            /// </summary>
            public bool IsReviewed { get; set; }

            /// <summary>
            /// the date and time the character was last changed
            /// </summary>
            public DateTime? LastChanged { get; set; }
        }

        /// <summary>
        /// a primary trait to be associated with a character sheet
        /// </summary>
        public class PrimaryTraits
        {
            /// <summary>
            /// default constructor
            /// </summary>
            /// <param name="characterId">the primary key of the character sheet the trait belongs to</param>
            /// <param name="isVisible">whether or not the trait is publicly visible</param>
            /// <param name="isPassive">whether or not the trait is a passive trait</param>
            /// <param name="isMajor">whether the trait is a major trait or minor trait</param>
            /// <param name="isEdge">whether the trait is an edge or a flaw</param>
            public PrimaryTraits(string characterId, bool isVisible = false, bool isPassive = false, bool isMajor = false, bool isEdge = false)
            {
                CharacterId = characterId;
                IsVisible = isVisible;
                IsPassive = isPassive;
                IsMajor = isMajor;
                IsEdge = isEdge;
            }

            /// <summary>
            /// the primary key automatically generated by Arango
            /// </summary>
            public string? _key { get; set; }

            /// <summary>
            /// the primary key of the character that the trait belongs to
            /// </summary>
            public string CharacterId { get; set; }

            /// <summary>
            /// the name of the trait
            /// </summary>
            public string? Name { get; set; }

            /// <summary>
            /// whether or not the trait is publicly visible
            /// </summary>
            public bool IsVisible { get; set; }

            /// <summary>
            /// a description of the trait
            /// </summary>
            public string? Description { get; set; }

            /// <summary>
            /// whether or not the trait is a passive trait
            /// </summary>
            public bool IsPassive { get; set; }

            /// <summary>
            /// the total energy point cost of the trait
            /// </summary>
            public int? EnergyCost { get; set; }

            /// <summary>
            /// the total ability point cost of the trait
            /// </summary>
            public int? AbilityPointCost { get; set; }

            /// <summary>
            /// whether the trait is major trait or a minor trait
            /// </summary>
            public bool IsMajor { get; set; }

            /// <summary>
            /// Whether the trait is an edge or a flaw
            /// </summary>
            public bool IsEdge { get; set; }
        }

        /// <summary>
        /// traits and benefits provided by a character's equipment
        /// </summary>
        public class EquipmentTraits
        {
            /// <summary>
            /// default constructor
            /// </summary>
            /// <param name="characterId">the primary key of the character sheet this trait belongs to</param>
            /// <param name="isVisible">whether or not the trait is publicly visible</param>
            /// <param name="isPassive">whether or not the trait is a passive trait</param>
            public EquipmentTraits(string characterId, bool isVisible = false, bool isPassive = false)
            {
                CharacterId = characterId;
                IsVisible = isVisible;
                IsPassive = isPassive;
            }

            /// <summary>
            /// the primary key automatically generated by Arango
            /// </summary>
            public string? _key { get; set; }

            /// <summary>
            /// the primary key of the character sheet this trait belongs to
            /// </summary>
            public string CharacterId { get; set; }

            /// <summary>
            /// the name of the trait
            /// </summary>
            public string? Name { get; set; }

            /// <summary>
            /// whether or not the trait is publicly visible
            /// </summary>
            public bool IsVisible { get; set; }

            /// <summary>
            /// a description of the trait
            /// </summary>
            public string? Description { get; set; }

            /// <summary>
            /// whether or not the trait is a passive trait
            /// </summary>
            public bool IsPassive { get; set; }

            /// <summary>
            /// the total energy cost of the trait
            /// </summary>
            public int? EnergyCost { get; set; }

            /// <summary>
            /// the total ability point cost of the trait
            /// </summary>
            public int? AbilityPointCost { get; set; }
        }

        /// <summary>
        /// a description of a character's origin
        /// </summary>
        public class OriginTraits
        {
            /// <summary>
            /// default constructor
            /// </summary>
            /// <param name="characterId">the primary key of the character sheet this trait belongs to</param>
            /// <param name="isVisible">whether or not the trait is publicly visible</param>
            public OriginTraits(string characterId, bool isVisible = false)
            {
                CharacterId = characterId;
                IsVisible = isVisible;
            }

            /// <summary>
            /// the primary key automatically generated by Arango
            /// </summary>
            public string? _key { get; set; }

            /// <summary>
            /// the primary key of the character sheet this trait belongs to
            /// </summary>
            public string CharacterId { get; set; }

            /// <summary>
            /// the name of the trait
            /// </summary>
            public string? Name { get; set; }

            /// <summary>
            /// Whether or not the trait is publicly visible
            /// </summary>
            public bool IsVisible { get; set; }

            /// <summary>
            /// a description of the trait
            /// </summary>
            public string? Description { get; set; }
        }

        /// <summary>
        /// A definition of a language
        /// </summary>
        public class Languages
        {
            /// <summary>
            /// default constructor
            /// </summary>
            /// <param name="traitId">the primary key of the origin trait this language belongs to</param>
            public Languages(string traitId)
            {
                TraitId = traitId;
            }

            /// <summary>
            /// primary key automatically generated by Arango
            /// </summary>
            public string? _key { get; set; }

            /// <summary>
            /// the primary key of the origin trait this language belongs to
            /// </summary>
            public string TraitId { get; set; }

            /// <summary>
            /// the name of this language
            /// </summary>
            public string? Name { get; set; }

            /// <summary>
            /// a description of this language
            /// </summary>
            public string? Description { get; set; }
        }

        /// <summary>
        /// a definition of how the character percieves the environment around it
        /// </summary>
        public class VisionTypes
        {
            /// <summary>
            /// default constructor
            /// </summary>
            /// <param name="traitId">the primary key of the origin trait this vision type belongs to</param>
            public VisionTypes(string traitId)
            {
                TraitId = traitId;
            }

            /// <summary>
            /// primary key automatically generated by arango
            /// </summary>
            public string? _key { get; set; }

            /// <summary>
            /// the primary key of the origin trait this vision type belongs to
            /// </summary>
            public string TraitId { get; set; }

            /// <summary>
            /// the name of this vision type
            /// </summary>
            public string? Name { get; set; }

            /// <summary>
            /// a description of this vision type
            /// </summary>
            public string? Description { get; set; }
        }

        /// <summary>
        /// a definition for traits such as status effects, curses, and other temporary traits
        /// </summary>
        public class TemporaryTraits
        {
            /// <summary>
            /// default constructor
            /// </summary>
            /// <param name="characterId">the primary key of the character sheet this trait belongs to</param>
            /// <param name="isVisible">whether or not this trait is publicly visible</param>
            /// <param name="isPassive">whehter or not this trait is passive</param>
            public TemporaryTraits(string characterId, bool isVisible = false, bool isPassive = false)
            {
                CharacterId = characterId;
                IsVisible = isVisible;
                IsPassive = isPassive;
            }

            /// <summary>
            /// the primary key automatically generated by arango
            /// </summary>
            public string? _key { get; set; }

            /// <summary>
            /// the primary key of the character sheet this trait belongs to
            /// </summary>
            public string CharacterId { get; set; }

            /// <summary>
            /// the name of this trait
            /// </summary>
            public string? Name { get; set; }

            /// <summary>
            /// whether or not this trait is publicly visible
            /// </summary>
            public bool IsVisible { get; set; }

            /// <summary>
            /// a description of this trait
            /// </summary>
            public string? Description { get; set; }

            /// <summary>
            /// whether or not this trait is passive
            /// </summary>
            public bool IsPassive { get; set; }

            /// <summary>
            /// the total energy cost of this trait
            /// </summary>
            public int? EnergyCost { get; set; }
        }

        /// <summary>
        /// represents a definition of a single piece of the character's equipment and items
        /// </summary>
        public class Equipment
        {
            /// <summary>
            /// default constructor
            /// </summary>
            /// <param name="characterId">primary key of the character sheet this equipment belongs to</param>
            public Equipment(string characterId)
            {
                CharacterId = characterId;
            }

            /// <summary>
            /// primary key automatically generated by Arango
            /// </summary>
            public string? _key { get; set; }

            /// <summary>
            /// the primary key of the character sheet this equipment belongs to
            /// </summary>
            public string CharacterId { get; set; }

            /// <summary>
            /// the name of this equipment
            /// </summary>
            public string? Name { get; set; }

            /// <summary>
            /// the description of this equipment
            /// </summary>
            public string? Description { get; set; }

            /// <summary>
            /// the monetary value of this equipment
            /// </summary>
            public int? Cost { get; set; }

            /// <summary>
            /// the weight of this equipment
            /// </summary>
            public decimal? Weight { get; set; }
        }

        /// <summary>
        /// represents a single request for game master's prescence
        /// </summary>
        public class GMRequests
        {
            /// <summary>
            /// default constructor
            /// </summary>
            /// <param name="requesterId">the primary key of the user who submitted the request</param>
            /// <param name="isComplete">whether or not the request is complete</param>
            /// <param name="isRelayed">whehter or not the request has been relayed</param>
            public GMRequests(string requesterId, bool isComplete = false, bool isRelayed = false)
            {
                RequesterId = requesterId;
                IsComplete = isComplete;
                IsRelayed = isRelayed;
            }

            /// <summary>
            /// the primary key automatically generated by arango
            /// </summary>
            public string? _key { get; set; }

            /// <summary>
            /// the primary key of the user who submitted the request
            /// </summary>
            public string RequesterId { get; set; }

            /// <summary>
            /// whether or not the request is complete
            /// </summary>
            public bool IsComplete { get; set; }

            /// <summary>
            /// whether or not the request has been relayed to discord
            /// </summary>
            public bool IsRelayed { get; set; }

            /// <summary>
            /// the date and time the request was submitted
            /// </summary>
            public DateTime? Timestamp { get; set; }

            /// <summary>
            /// the location (in the map) of the user who submitted the request
            /// </summary>
            public string? RequesterLocation { get; set; }

            /// <summary>
            /// the reason for the request
            /// </summary>
            public string? RequestReason { get; set; }

            /// <summary>
            /// the value of an Enumeration representing the urgency of the request
            /// </summary>
            public int? Urgency { get; set; }
        }

        /// <summary>
        /// represents a single die roll made within the system
        /// </summary>
        public class DieRolls
        {
            /// <summary>
            /// default constructor
            /// </summary>
            /// <param name="userId">the primary key of the user who made the die roll</param>
            /// <param name="isAdvantage">whether or not the roll was made with advantage</param>
            /// <param name="isDisadvantage">whether or not the roll was made with disadvantage</param>
            /// <param name="isDeathRoll">whether or not the roll is a death roll</param>
            public DieRolls(string userId, bool isAdvantage = false, bool isDisadvantage = false, bool isDeathRoll = false)
            {
                UserId = userId;
                IsAdvantage = isAdvantage;
                IsDisadvantage = isDisadvantage;
                IsDeathRoll = isDeathRoll;
            }

            /// <summary>
            /// the primary key automatically generated by arango
            /// </summary>
            public string? _key { get; set; }

            /// <summary>
            /// the primary key of the user who made the roll
            /// </summary>
            public string UserId { get; set; }

            /// <summary>
            /// the date and time the roll was made
            /// </summary>
            public DateTime? Timestamp { get; set; }

            /// <summary>
            /// the number of rolls made
            /// </summary>
            public int? NumOfRolls { get; set; }

            /// <summary>
            /// the type of dice rolled
            /// </summary>
            public int? DieType { get; set; }

            /// <summary>
            /// the modifier applied to the roll by the game master
            /// </summary>
            public int? GMRollModifier { get; set; }

            /// <summary>
            /// the ability score modifier applied to the roll
            /// </summary>
            public int? CharacterAbility { get; set; }

            /// <summary>
            /// the primary key of the character
            /// </summary>
            public string? SourceCharacterId { get; set; }

            /// <summary>
            /// whether or not the roll was made with advantage
            /// </summary>
            public bool IsAdvantage { get; set; }

            /// <summary>
            /// whether or not the roll was made with disadvantage
            /// </summary>
            public bool IsDisadvantage { get; set; }

            /// <summary>
            /// whether or not the roll is a death roll
            /// </summary>
            public bool IsDeathRoll { get; set; }

            /// <summary>
            /// the result of the roll
            /// </summary>
            public int? Result { get; set; }

            /// <summary>
            /// a detailed description of the result of the roll
            /// </summary>
            public string? DetailedResult { get; set; }
        }

        /// <summary>
        /// an application from a user to become an NPC
        /// </summary>
        public class NPCApplications
        {
            /// <summary>
            /// default constructor 
            /// </summary>
            /// <param name="userId">the primary key of the user who submitted the application</param>
            /// <param name="isApproved">whether or not the application has been approved</param>
            /// <param name="isDenied">whether or not the application has been denied</param>
            /// <param name="isUnderReview">whether or not the application is under review</param>
            public NPCApplications(string userId, bool isApproved = false, bool isDenied = false, bool isUnderReview = false)
            {
                UserId = userId;
                IsApproved = isApproved;
                IsDenied = isDenied;
                IsUnderReview = isUnderReview;
            }

            /// <summary>
            /// the primary key automatically generated by arango
            /// </summary>
            public string? _key { get; set; }
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

            public string? _key { get; set; }
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

            public string? _key { get; set; }
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

            public string? _key { get; set; }
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

