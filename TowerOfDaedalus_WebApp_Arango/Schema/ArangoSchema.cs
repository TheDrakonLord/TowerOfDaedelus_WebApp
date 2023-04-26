using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using ArangoDBNetStandard;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Primitives;
using Newtonsoft.Json;

namespace TowerOfDaedalus_WebApp_Arango.Schema
{
    /// <summary>
    /// Full arango schema definition to assist with creating the database
    /// </summary>
    public static class ArangoSchema
    {
        // Identity Framework
        /// <summary>
        /// name of the Users collection
        /// </summary>
        public const string collUsers = "Users";
        /// <summary>
        /// name of the roles collection
        /// </summary>
        public const string collRoles = "Roles";
        /// <summary>
        /// name of the user logins collection
        /// </summary>
        public const string collUserLogins = "UserLogins";
        /// <summary>
        /// name of the user claims collection
        /// </summary>
        public const string collUserClaims = "UserClaims";
        /// <summary>
        /// name of the role claims collection
        /// </summary>
        public const string collRoleClaims = "RoleClaims";
        /// <summary>
        /// name of hte user tokens collection
        /// </summary>
        public const string collUserTokens = "UserTokens";


        // Custom Data
        /// <summary>
        /// name of the rp schedule collection
        /// </summary>
        public const string collRPSchedule = "RPSchedule";
        /// <summary>
        /// name of the featured articles collection
        /// </summary>
        public const string collFeaturedArticles = "FeaturedArticles";
        /// <summary>
        /// name of the quest list collection
        /// </summary>
        public const string collQuestList = "QuestList";
        /// <summary>
        /// name of the npc descriptions collection
        /// </summary>
        public const string collNPCDescriptions = "NPCDescriptions";
        /// <summary>
        /// name of the character sheets collection
        /// </summary>
        public const string collCharacterSheets = "CharacterSheets";
        /// <summary>
        /// name of the primary traits collection
        /// </summary>
        public const string collPrimaryTraits = "PrimaryTraits";
        /// <summary>
        /// name of the equipment traits collection
        /// </summary>
        public const string collEquipmentTraits = "EquipmentTraits";
        /// <summary>
        /// name of the equipment collection
        /// </summary>
        public const string collEquipment = "Equipment";
        /// <summary>
        /// name of the temporary traits collection
        /// </summary>
        public const string collTemporaryTraits = "TemporaryTraits";
        /// <summary>
        /// name of the origin traits collection
        /// </summary>
        public const string collOriginTraits = "OriginTraits";
        /// <summary>
        /// name of the languages collection
        /// </summary>
        public const string collLanguages = "Languages";
        /// <summary>
        /// name of the vision types collection
        /// </summary>
        public const string collVisionTypes = "VisionTypes";
        /// <summary>
        /// name of the GM Requests collection
        /// </summary>
        public const string collGMRequests = "GMRequests";
        /// <summary>
        /// name of the Die rolls collection
        /// </summary>
        public const string collDieRolls = "DieRolls";
        /// <summary>
        /// name of the NPC Applications collection
        /// </summary>
        public const string collNPCApplications = "NPCApplications";
        /// <summary>
        /// name of the mission applications collection
        /// </summary>
        public const string collMissionApplications = "MissionApplications";
        /// <summary>
        /// name of the player applications collection
        /// </summary>
        public const string collPlayerApplications = "PlayerApplications";
        /// <summary>
        /// name of the visitor pass collection
        /// </summary>
        public const string collVisitorPass = "VisitorPass";


        // graph definitions
        /// <summary>
        /// name of the primary named graph
        /// </summary>
        public const string graphPrimary = "PrimaryGraph";


        // identity edge definitions
        /// <summary>
        /// name of the collection for edge definitions from Users -> roles
        /// </summary>
        public const string edgeUserRoles = "eUserRoles";
        /// <summary>
        /// name of the collection for edge definitions from Roles -> Users
        /// </summary>
        public const string edgeRoleUsers = "eRoleUsers";
        /// <summary>
        /// name of the collection for edge definitions from Users -> Logins
        /// </summary>
        public const string edgeUserLogins = "eUserLogins";
        /// <summary>
        /// name of the collection for edge definitions from Users -> Claims
        /// </summary>
        public const string edgeUserClaims = "eUserClaims";
        /// <summary>
        /// name of the collection for edge definitions from Users -> tokens
        /// </summary>
        public const string edgeUserTokens = "eUserTokens";
        /// <summary>
        /// name of the collection for edge definitions from Roles -> Claims
        /// </summary>
        public const string edgeRoleClaims = "eRoleClaims";


        // custom data edge definitions
        /// <summary>
        /// name of the collection for edge definitions from NPCDescriptions -> Quest List
        /// </summary>
        public const string edgeQuestGivers = "eQuestGivers";
        /// <summary>
        /// name of the collection for edge definitions from Users -> Quest List
        /// </summary>
        public const string edgeQuestAuthor = "eQuestAuthor";
        /// <summary>
        /// name of the collection for edge definitions from Users -> Character Sheets
        /// </summary>
        public const string edgeCharacterOwner = "eCharacterOwner";
        /// <summary>
        /// name of the collection for edge definitions from Users -> GM Requests
        /// </summary>
        public const string edgeRequestor = "eRequestor";
        /// <summary>
        /// name of the collection for edge definitions from Users -> Die rolls
        /// </summary>
        public const string edgeRoller = "eRoller";
        /// <summary>
        /// name of the collection for edge definitions from Users -> NPC Applications
        /// </summary>
        public const string edgeNPCApplicant = "eNPCApplicant";
        /// <summary>
        /// name of the collection for edge definitions from Users -> Mission Applications
        /// </summary>
        public const string edgeMissionApplicant = "eMissionApplicant";
        /// <summary>
        /// name of the collection for edge definitions from Users -> Player Applications
        /// </summary>
        public const string edgePlayerApplicant = "ePlayerApplicant";
        /// <summary>
        /// name of the collection for edge definitions from Users -> Visitor Passes
        /// </summary>
        public const string edgeVisitorApplicant = "eVisitorApplicant";
        /// <summary>
        /// name of the collection for edge definitions from Character Sheets -> Primary Traits
        /// </summary>
        public const string edgePrimaryTrait = "ePrimaryTrait";
        /// <summary>
        /// name of the collection for edge definitions from Player Applications -> Character Sheets
        /// </summary>
        public const string edgeCharacterApplication = "eCharacterApplication";
        /// <summary>
        /// name of the collection for edge definitions from Character Sheets -> Die Rolls
        /// </summary>
        public const string edgeCharacterRoll = "eCharacterRoll";
        /// <summary>
        /// name of the collection for edge definitions from Character Sheets -> Mission Applications
        /// </summary>
        public const string edgeCharacterMissions = "eCharacterMissions";
        /// <summary>
        /// name of the collection for edge definitions from Mission Applications -> Character Sheets
        /// </summary>
        public const string edgeTeamMembers = "eTeamMembers";
        /// <summary>
        /// name of the collection for edge definitions from Character Sheets -> Equipment Traits
        /// </summary>
        public const string edgeEquipmentTrait = "eEquipmentTrait";
        /// <summary>
        /// name of the collection for edge definitions from Character Sheets -> Equipment
        /// </summary>
        public const string edgeInventory = "eInventory";
        /// <summary>
        /// name of the collection for edge definitions from Character Sheets -> Temporary Traits
        /// </summary>
        public const string edgeTemporaryTrait = "eTemporaryTrait";
        /// <summary>
        /// name of the collection for edge definitions from Character Sheets -> Origin Traits
        /// </summary>
        public const string edgeOriginTrait = "eOriginTrait";
        /// <summary>
        /// name of the collection for edge definitions from Origin Traits -> Languages
        /// </summary>
        public const string edgeLanguage = "eLanguage";
        /// <summary>
        /// name of the collection for edge definitions from Origin Traits -> Vision Types
        /// </summary>
        public const string edgeVisionType = "eVisionType";


        /// <summary>
        /// list of collections to be added
        /// </summary>
        public static readonly List<Collection> Collections = new List<Collection>
        {
            // Identity Framework
            new Collection(collUsers),
            new Collection(collRoles),
            new Collection(collUserLogins),
            new Collection(collUserClaims),
            new Collection(collRoleClaims),
            new Collection(collUserTokens),
            // Custom Data
            new Collection(collRPSchedule),
            new Collection(collFeaturedArticles),
            new Collection(collQuestList),
            new Collection(collNPCDescriptions),
            new Collection(collCharacterSheets),
            new Collection(collPrimaryTraits),
            new Collection(collEquipmentTraits),
            new Collection(collEquipment),
            new Collection(collTemporaryTraits),
            new Collection(collOriginTraits),
            new Collection(collLanguages),
            new Collection(collVisionTypes),
            new Collection(collGMRequests),
            new Collection(collDieRolls),
            new Collection(collNPCApplications),
            new Collection(collMissionApplications),
            new Collection(collPlayerApplications),
            new Collection(collVisitorPass),
            // Identity framework edge definition collections
            new Collection(edgeUserRoles, type: ArangoDBNetStandard.CollectionApi.Models.CollectionType.Edge),
            new Collection(edgeRoleUsers, type: ArangoDBNetStandard.CollectionApi.Models.CollectionType.Edge),
            new Collection(edgeUserLogins, type: ArangoDBNetStandard.CollectionApi.Models.CollectionType.Edge),
            new Collection(edgeUserClaims, type: ArangoDBNetStandard.CollectionApi.Models.CollectionType.Edge),
            new Collection(edgeUserTokens, type: ArangoDBNetStandard.CollectionApi.Models.CollectionType.Edge),
            new Collection(edgeRoleClaims, type: ArangoDBNetStandard.CollectionApi.Models.CollectionType.Edge),
            // Custom data edge definition collections
            new Collection(edgeQuestGivers, type: ArangoDBNetStandard.CollectionApi.Models.CollectionType.Edge),
            new Collection(edgeQuestAuthor, type: ArangoDBNetStandard.CollectionApi.Models.CollectionType.Edge),
            new Collection(edgeCharacterOwner, type: ArangoDBNetStandard.CollectionApi.Models.CollectionType.Edge),
            new Collection(edgeRequestor, type: ArangoDBNetStandard.CollectionApi.Models.CollectionType.Edge),
            new Collection(edgeRoller, type: ArangoDBNetStandard.CollectionApi.Models.CollectionType.Edge),
            new Collection(edgeNPCApplicant, type: ArangoDBNetStandard.CollectionApi.Models.CollectionType.Edge),
            new Collection(edgeMissionApplicant, type: ArangoDBNetStandard.CollectionApi.Models.CollectionType.Edge),
            new Collection(edgePlayerApplicant, type: ArangoDBNetStandard.CollectionApi.Models.CollectionType.Edge),
            new Collection(edgeVisitorApplicant, type: ArangoDBNetStandard.CollectionApi.Models.CollectionType.Edge),
            new Collection(edgePrimaryTrait, type: ArangoDBNetStandard.CollectionApi.Models.CollectionType.Edge),
            new Collection(edgeCharacterApplication, type: ArangoDBNetStandard.CollectionApi.Models.CollectionType.Edge),
            new Collection(edgeCharacterRoll, type: ArangoDBNetStandard.CollectionApi.Models.CollectionType.Edge),
            new Collection(edgeCharacterMissions, type: ArangoDBNetStandard.CollectionApi.Models.CollectionType.Edge),
            new Collection(edgeTeamMembers, type: ArangoDBNetStandard.CollectionApi.Models.CollectionType.Edge),
            new Collection(edgeEquipmentTrait, type: ArangoDBNetStandard.CollectionApi.Models.CollectionType.Edge),
            new Collection(edgeInventory, type: ArangoDBNetStandard.CollectionApi.Models.CollectionType.Edge),
            new Collection(edgeTemporaryTrait, type: ArangoDBNetStandard.CollectionApi.Models.CollectionType.Edge),
            new Collection(edgeOriginTrait, type: ArangoDBNetStandard.CollectionApi.Models.CollectionType.Edge),
            new Collection(edgeLanguage, type: ArangoDBNetStandard.CollectionApi.Models.CollectionType.Edge),
            new Collection(edgeVisionType, type: ArangoDBNetStandard.CollectionApi.Models.CollectionType.Edge)
        };

        /// <summary>
        /// list of indicies to be added
        /// </summary>
        public static readonly List<ArangoIndex> indices = new List<ArangoIndex>
        {
            new ArangoIndex
            {
                CollectionName = collUsers,
                Fields = new List<string>
                {
                    "UserName"
                },
                InBackground = false,
                Name = "UserNameIndex",
                Type = ArangoDBNetStandard.IndexApi.Models.IndexTypes.Persistent
            }
        };

        /// <summary>
        /// list of graphs to be added
        /// </summary>
        public static readonly List<Graph> Graphs = new List<Graph>
        {
            new Graph(graphPrimary, new List<ArangoDBNetStandard.GraphApi.Models.EdgeDefinition>
            {
                // Identity Relationships
                // UserRoles Users->Roles
                new ArangoDBNetStandard.GraphApi.Models.EdgeDefinition
                {
                    Collection = edgeUserRoles,
                    From = new List<string>
                    {
                        collUsers
                    },
                    To = new List<string>
                    {
                        collRoles
                    }                    
                },

                // UserRoles Roles->Users
                new ArangoDBNetStandard.GraphApi.Models.EdgeDefinition
                {
                    Collection = edgeRoleUsers,
                    From = new List<string>
                    {
                        collRoles
                    },
                    To = new List<string>
                    {
                        collUsers
                    }
                },

                // Users -> UserLogins
                new ArangoDBNetStandard.GraphApi.Models.EdgeDefinition
                {
                    Collection = edgeUserLogins,
                    From =  new List<string>
                    {
                        collUsers
                    },
                    To = new List<string>
                    {
                        collUserLogins
                    }
                },

                // Users -> UserClaims
                new ArangoDBNetStandard.GraphApi.Models.EdgeDefinition
                {
                    Collection = edgeUserClaims,
                    From = new List<string>
                    {
                        collUsers
                    },
                    To = new List<string>
                    {
                        collUserClaims
                    }
                },

                // Users -> UserTokens
                new ArangoDBNetStandard.GraphApi.Models.EdgeDefinition
                {
                    Collection = edgeUserTokens,
                    From = new List<string>
                    {
                        collUsers
                    },
                    To = new List<string>
                    {
                        collUserTokens
                    }
                },

                // Roles -> RoleClaims
                new ArangoDBNetStandard.GraphApi.Models.EdgeDefinition
                {
                    Collection = edgeRoleClaims,
                    From = new List<string>
                    {
                        collRoles
                    },
                    To = new List<string>
                    {
                        collRoleClaims
                    }
                },

                // Custom Relations
                // NPCDescriptions -> QuestList
                new ArangoDBNetStandard.GraphApi.Models.EdgeDefinition
                {
                    Collection = edgeQuestGivers,
                    From = new List<string>
                    {
                        collNPCDescriptions
                    },
                    To = new List<string>
                    {
                        collQuestList
                    }
                },

                // Users -> QuestList
                new ArangoDBNetStandard.GraphApi.Models.EdgeDefinition
                {
                    Collection = edgeQuestAuthor,
                    From = new List<string>
                    {
                        collUsers
                    },
                    To = new List<string>
                    {
                        collQuestList
                    }
                },

                // Users -> CharacterSheets
                new ArangoDBNetStandard.GraphApi.Models.EdgeDefinition
                {
                    Collection = edgeCharacterOwner,
                    From = new List<string>
                    {
                        collUsers
                    },
                    To = new List<string>
                    {
                        collCharacterSheets
                    }
                },

                // Users -> GMRequests
                new ArangoDBNetStandard.GraphApi.Models.EdgeDefinition
                {
                    Collection = edgeRequestor,
                    From = new List<string>
                    {
                        collUsers
                    },
                    To = new List<string>
                    {
                        collGMRequests
                    }
                },

                // Users -> DieRolls
                new ArangoDBNetStandard.GraphApi.Models.EdgeDefinition
                {
                    Collection = edgeRoller,
                    From = new List<string>
                    {
                        collUsers
                    },
                    To = new List<string>
                    {
                        collDieRolls
                    }
                },

                // Users -> NPCApplications
                new ArangoDBNetStandard.GraphApi.Models.EdgeDefinition
                {
                    Collection = edgeNPCApplicant,
                    From = new List<string>
                    {
                        collUsers
                    },
                    To = new List<string>
                    {
                        collNPCApplications
                    }
                },

                // Users -> MissionApplications
                new ArangoDBNetStandard.GraphApi.Models.EdgeDefinition
                {
                    Collection = edgeMissionApplicant,
                    From = new List<string>
                    {
                        collUsers
                    },
                    To = new List<string>
                    {
                       collMissionApplications
                    }
                },

                // Users -> PlayerApplications
                new ArangoDBNetStandard.GraphApi.Models.EdgeDefinition
                {
                    Collection = edgePlayerApplicant,
                    From = new List<string>
                    {
                        collUsers
                    },
                    To = new List<string>
                    {
                        collPlayerApplications
                    }
                },

                // Users -> VisitorPass
                new ArangoDBNetStandard.GraphApi.Models.EdgeDefinition
                {
                    Collection = edgeVisitorApplicant,
                    From = new List<string>
                    {
                        collUsers
                    },
                    To = new List<string>
                    {
                        collVisitorPass
                    }
                },

                // CharacterSheets -> PrimaryTraits
                new ArangoDBNetStandard.GraphApi.Models.EdgeDefinition
                {
                    Collection = edgePrimaryTrait,
                    From = new List<string>
                    {
                        collCharacterSheets
                    },
                    To = new List<string>
                    {
                        collPrimaryTraits
                    }
                },

                // PlayerApplications -> CharacterSheets
                new ArangoDBNetStandard.GraphApi.Models.EdgeDefinition
                {
                    Collection = edgeCharacterApplication,
                    From = new List<string>
                    {
                        collPlayerApplications
                    },
                    To = new List<string>
                    {
                        collCharacterSheets
                    }
                },

                // CharacterSheets -> DieRolls
                new ArangoDBNetStandard.GraphApi.Models.EdgeDefinition
                {
                    Collection = edgeCharacterRoll, 
                    From = new List<string>
                    {
                        collCharacterSheets
                    },
                    To = new List<string>
                    {
                        collDieRolls
                    }
                },

                // CharacterSheets -> MissionApplications
                new ArangoDBNetStandard.GraphApi.Models.EdgeDefinition
                {
                    Collection = edgeCharacterMissions,
                    From = new List<string>
                    {
                        collCharacterSheets
                    },
                    To = new List<string>
                    {
                        collMissionApplications
                    }
                },

                // MissionApplications -> CharacterSheets
                new ArangoDBNetStandard.GraphApi.Models.EdgeDefinition
                {
                    Collection = edgeTeamMembers, 
                    From = new List<string>
                    {
                        collMissionApplications
                    },
                    To = new List<string>
                    {
                        collCharacterSheets
                    }
                }, 

                // CharacterSheets -> EquipmentTraits
                new ArangoDBNetStandard.GraphApi.Models.EdgeDefinition
                {
                    Collection = edgeEquipmentTrait,
                    From = new List<string>
                    {
                        collCharacterSheets
                    },
                    To = new List<string>
                    {
                        collEquipmentTraits
                    }
                },

                // CharacterSheets -> Equipment
                new ArangoDBNetStandard.GraphApi.Models.EdgeDefinition
                {
                    Collection = edgeInventory,
                    From = new List<string>
                    {
                        collCharacterSheets
                    },
                    To = new List<string>
                    {
                        collEquipment
                    }
                },

                // CharacterSheets -> TemporaryTraits
                new ArangoDBNetStandard.GraphApi.Models.EdgeDefinition
                {
                    Collection = edgeTemporaryTrait,
                    From = new List<string>
                    {
                        collCharacterSheets
                    },
                    To = new List<string>
                    {
                        collTemporaryTraits
                    }
                },

                // CharacterSheets -> OriginTraits
                new ArangoDBNetStandard.GraphApi.Models.EdgeDefinition
                {
                    Collection = edgeOriginTrait,
                    From = new List<string>
                    {
                        collCharacterSheets
                    },
                    To = new List<string>
                    {
                        collOriginTraits
                    }
                },

                // OriginTraits -> Languages
                new ArangoDBNetStandard.GraphApi.Models.EdgeDefinition
                {
                    Collection = edgeLanguage,
                    From = new List<string>
                    {
                        collOriginTraits
                    },
                    To = new List<string>
                    {
                        collLanguages
                    }
                },

                // OriginTraits -> VisionTypes
                new ArangoDBNetStandard.GraphApi.Models.EdgeDefinition
                {
                    Collection = edgeVisionType,
                    From = new List<string>
                    {
                        collOriginTraits
                    },
                    To = new List<string>
                    {
                        collVisionTypes
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
        public class Users
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
            public Users(string userName = "", int accessFailedCount = 0, bool emailConfirmed = false, bool lockoutEnabled = false, bool phoneNumberConfirmed = false,
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
            public DateTimeOffset? LockoutEnd { get; set; }

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
            public string? DiscordAvatar { get; set; }

        }

        /// <summary>
        /// AspNetCore Identity IRole implementation
        /// defines roles that users can be members of
        /// </summary>
        public class Roles
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
        /// 
        /// </summary>
        public class ArangoClaims
        {
            public ArangoClaims() { }

            public ArangoClaims(ArangoClaims other)
            {
                _key = other._key;
                type = other.type;
                value = other.value;
                valueType = other.valueType;
                issuer = other.issuer;
                originalIssuer = other.originalIssuer;
                subject = other.subject;
            }

            public ArangoClaims(Claim other)
            {
                type = other.Type;
                value = other.Value;
                valueType = other.ValueType;
                issuer = other.Issuer;
                originalIssuer = other.OriginalIssuer;
                subject = other.Subject;
            }

            /// <summary>
            /// the primary key automatically generated by arango
            /// </summary>
            public string? _key { get; set; }
            public string? type { get; set; }
            public string? value { get; set; }
            public string? valueType { get; set; }
            public string? issuer { get; set; }

            public string? originalIssuer { get; set; }
            public ClaimsIdentity? subject { get; set; }

            public Claim convertToClaim()
            {
                return new Claim(type, value, valueType, issuer, originalIssuer, subject);
            }
        }

        /// <summary>
        /// Represents each time a user logs in
        /// </summary>
        public class UserLogins : UserLoginInfo
        {
            /// <summary>
            /// default constructor
            /// </summary>
            /// <param name="loginProvder">the login provider the user logged in with</param>
            /// <param name="providerKey">the key provided by the login provider</param>
            /// <param name="displayName"></param>
            /// <param name="userId">the user's user id (the _key value of the user document)</param>
            public UserLogins(string loginProvder, string providerKey, string? displayName,  string userId) : base(loginProvder, providerKey, displayName)
            {
                UserId = userId;
            }

            /// <summary>
            /// the primary key automatically generated by arango
            /// </summary>
            public string? _key { get; set; }

            /// <summary>
            /// the user's user Id
            /// </summary>
            public string UserId { get; set; }

            /// <summary>
            /// 
            /// </summary>
            /// <returns></returns>
            public UserLoginInfo getUserLoginInfo()
            {
                return new UserLoginInfo(this.LoginProvider, this.ProviderKey, this.ProviderDisplayName);
            }
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

            /// <summary>
            /// The primary key of the user who submitted the application
            /// </summary>
            public string UserId { get; set; }

            /// <summary>
            /// whether or not the application has been approved
            /// </summary>
            public bool IsApproved { get; set; }

            /// <summary>
            /// whether or not the application has been denied
            /// </summary>
            public bool IsDenied { get; set; }

            /// <summary>
            /// whether or not the application is under review
            /// </summary>
            public bool IsUnderReview { get; set; }

            /// <summary>
            /// a numerical scale of how much experience the user has in roleplay
            /// </summary>
            public int? ExperienceLevel { get; set; }

            /// <summary>
            /// the first example of roleplay experience provided by the user
            /// </summary>
            public string? Experience1 { get; set; }

            /// <summary>
            /// the second example of roleplay experience provided by the user
            /// </summary>
            public string? Experience2 { get; set; }

            /// <summary>
            /// the third example of roleplay experience provided by the user
            /// </summary>
            public string? Experience3 { get; set; }

            /// <summary>
            /// the fourth example of roleplay experience provided by the user
            /// </summary>
            public string? Experience4 { get; set; }

            /// <summary>
            /// the fifth example of roleplay experience provided by the user
            /// </summary>
            public string? Experience5 { get; set; }

            /// <summary>
            /// the first person vouching for the user
            /// </summary>
            public string? Vouch1 { get; set; }

            /// <summary>
            /// the second person vouching for the user
            /// </summary>
            public string? Vouch2 { get; set; }

            /// <summary>
            /// the third person vouching for the user
            /// </summary>
            public string? Vouch3 { get; set; }
        }

        /// <summary>
        /// an application from a user for a mission to be held
        /// </summary>
        public class MissionApplications
        {
            /// <summary>
            /// default constructor
            /// </summary>
            /// <param name="userId"></param>
            /// <param name="isApproved"></param>
            /// <param name="isDenied"></param>
            /// <param name="isUnderReview"></param>
            public MissionApplications(string userId, bool isApproved = false, bool isDenied = false, bool isUnderReview = false)
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

            /// <summary>
            /// the primary key of the user who submitted the application
            /// </summary>
            public string UserId { get; set; }

            /// <summary>
            /// whether or not the mission has been approved
            /// </summary>
            public bool IsApproved { get; set; }

            /// <summary>
            /// whehter or not the mission has been denied
            /// </summary>
            public bool IsDenied { get; set; }

            /// <summary>
            /// whether or not the mission is udner review
            /// </summary>
            public bool IsUnderReview { get; set; }

            /// <summary>
            /// the external game master who is involved with the mission
            /// </summary>
            public string? RelevantExteriorGM { get; set; }

            /// <summary>
            /// the information the mission seeks to obtain
            /// </summary>
            public string? InformationSought { get; set; }

            /// <summary>
            /// a description of the team undertaking the mission
            /// </summary>
            public string? TeamDescription { get; set; }
        }

        /// <summary>
        /// represents an application for a user to become a player character
        /// </summary>
        public class PlayerApplications
        {
            /// <summary>
            /// default constructor
            /// </summary>
            /// <param name="userId">the primary key of the user who submitted the application</param>
            /// <param name="isApproved">whether or not the application has been approved</param>
            /// <param name="isDenied">whether or not the application has been denied</param>
            /// <param name="isUnderReview">whether or not the application is under review</param>
            public PlayerApplications(string userId, bool isApproved = false, bool isDenied = false, bool isUnderReview = false)
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

            /// <summary>
            /// the primary key of the user who submitted the applicaiton
            /// </summary>
            public string UserId { get; set; }

            /// <summary>
            /// whether or not the application has been approved
            /// </summary>
            public bool IsApproved { get; set; }

            /// <summary>
            /// whether or not the application has been denied
            /// </summary>
            public bool IsDenied { get; set; }

            /// <summary>
            /// whether or not the application is under review
            /// </summary>
            public bool IsUnderReview { get; set; }

            /// <summary>
            /// a numerical representation of the level of experience the user has in vr roleplay
            /// </summary>
            public int? ExperienceLevel { get; set; }

            /// <summary>
            /// an example of past experience in vr roleplay
            /// </summary>
            public string? Experience1 { get; set; }

            /// <summary>
            /// an example of past experience in vr roleplay
            /// </summary>
            public string? Experience2 { get; set; }

            /// <summary>
            /// an example of past experience in vr roleplay
            /// </summary>
            public string? Experience3 { get; set; }

            /// <summary>
            /// an example of past experience in vr roleplay
            /// </summary>
            public string? Experience4 { get; set; }

            /// <summary>
            /// an example of past experience in vr roleplay
            /// </summary>
            public string? Experience5 { get; set; }

            /// <summary>
            /// the first person vouching for the user
            /// </summary>
            public string? Vouch1 { get; set; }

            /// <summary>
            /// the second person vouching for the user
            /// </summary>
            public string? Vouch2 { get; set; }

            /// <summary>
            /// the third person vouching for the user
            /// </summary>
            public string? Vouch3 { get; set; }
        }

        /// <summary>
        /// represents a request to attend a session as a visiting character
        /// </summary>
        public class VisitorPass
        {
            /// <summary>
            /// default constructor
            /// </summary>
            /// <param name="userId">the primary key of the user submitting the request</param>
            /// <param name="isApproved">whether or not the request has been approved</param>
            public VisitorPass(string userId, bool isApproved = false)
            {
                UserId = userId;
                IsApproved = isApproved;
            }

            /// <summary>
            /// the primary key automatically generated by arango
            /// </summary>
            public string? _key { get; set; }

            /// <summary>
            /// the primary key of the user submitting the request
            /// </summary>
            public string UserId { get; set; }

            /// <summary>
            /// whether or not the request has been approved
            /// </summary>
            public bool IsApproved { get; set; }

            /// <summary>
            /// the primary key of the character sheet the user plans to use
            /// </summary>
            public string? CharacterId { get; set; }

            /// <summary>
            /// the date and time of the session the user is requesting to attend
            /// </summary>
            public DateTime? SessionDate { get; set; }

            /// <summary>
            /// a numerical value representing the number of users who have submitted requests prior to this one for the requested session
            /// </summary>
            public int? entryRank { get; set; }

            /// <summary>
            /// The earliest date and time that the the user is permitted to enter the rp. This is null if the request has not yet been approved.
            /// if this value is in the future, the session has not yet begun, and the user is not permitted to enter the rp yet.
            /// a value in the past means that the user is permitted to enter the rp, so long as ValidEnd is in the future.
            /// </summary>
            public DateTime? ValidStart { get; set; }

            /// <summary>
            /// The latest date and time the the user is permitted to enter the rp. This is null if the request has not yet been approved.
            /// if this falue is in the future, the user is permitted to enter the rp, so long as validStart is in the past.
            /// a value in the past means the the user is no longer permitted to enter the rp, regardless of the value of ValidStart.
            /// </summary>
            public DateTime? ValidEnd { get; set; }
        }

        /// <summary>
        /// Represents an edge or a relationshiop between two documents referred to as vertexes
        /// </summary>
        public class Edges
        {
            /// <summary>
            /// Default constructor
            /// </summary>
            /// <param name="from">a document handle representing the origin vertex of the edge expressed in the format collection/_key</param>
            /// <param name="to">a document handle representing the destination vertex of the edge expressed in the format collection/_key</param>
            public Edges(string from, string to)
            {
                _from = from;
                _to = to;
            }

            /// <summary>
            /// the primary key automatically generated by Arango
            /// </summary>
            public string? _key { get; set; }

            /// <summary>
            /// a document handle representing the origin vertex of the edge
            /// expressed in the format collection/_key
            /// </summary>
            public string _from { get; set; }

            /// <summary>
            /// a document handle representing the destination vertex of the edge
            /// expressed in the format collection/_key
            /// </summary>
            public string _to { get; set; }

            /// <summary>
            /// a description of the relationship the edge represents
            /// </summary>
            public string? Type { get; set; }
        }
    }
}

