using GB.MatchSimulator.DAL.Entities;

namespace GB.MatchSimulator.DAL.Local;

public static class DummyTeams
{
    // This acts as a dummy placeholder to seed the Demo DB
    public static List<TeamEntity> GetDummyTeams()
    {
        return new List<TeamEntity>
        {
            new TeamEntity
            {
                Id = "te_A7X9B2",
                Name = "Team A",
                Players = new List<PlayerEntity>
                {
                    new PlayerEntity { Id = "pl_QW3E8A", Name = "Alex Hunter", Offense = 8, Defense = 9 },
                    new PlayerEntity { Id = "pl_DF7P2K", Name = "Marcus Vale", Offense = 9, Defense = 8 },
                    new PlayerEntity { Id = "pl_HJ2N5R", Name = "Leo Costa", Offense = 8, Defense = 9 },
                    new PlayerEntity { Id = "pl_LK3Z9T", Name = "Chris Vaughn", Offense = 9, Defense = 8 },
                    new PlayerEntity { Id = "pl_PO6M4U", Name = "Jordan Reese", Offense = 8, Defense = 9 },
                }
            },

            new TeamEntity
            {
                Id = "te_B5Y7L4",
                Name = "Team B",
                Players = new List<PlayerEntity>
                {
                    new PlayerEntity { Id = "pl_AZ8L2K", Name = "Ethan Cole", Offense = 7, Defense = 6 },
                    new PlayerEntity { Id = "pl_BX5M9N", Name = "Rafael Cruz", Offense = 6, Defense = 7 },
                    new PlayerEntity { Id = "pl_CK1V4S", Name = "Oliver Grant", Offense = 6, Defense = 5 },
                    new PlayerEntity { Id = "pl_DM2J7R", Name = "Niko Alvarez", Offense = 7, Defense = 6 },
                    new PlayerEntity { Id = "pl_EN9P3W", Name = "Toby Carter", Offense = 5, Defense = 7 },
                }
            },

            new TeamEntity
            {
                Id = "te_C3V5P6",
                Name = "Team C",
                Players = new List<PlayerEntity>
                {
                    new PlayerEntity { Id = "pl_FG1S8T", Name = "Liam Frost", Offense = 5, Defense = 4 },
                    new PlayerEntity { Id = "pl_GH4D7E", Name = "Noah James", Offense = 6, Defense = 5 },
                    new PlayerEntity { Id = "pl_HJ7F2R", Name = "Eli Barrett", Offense = 5, Defense = 5 },
                    new PlayerEntity { Id = "pl_JK2L8Q", Name = "Sam Harper", Offense = 5, Defense = 3 },
                    new PlayerEntity { Id = "pl_KL9N6Z", Name = "Ryan Doyle", Offense = 4, Defense = 4 },
                }
            },

            new TeamEntity
            {
                Id = "te_D2F3M9",
                Name = "Team D",
                Players = new List<PlayerEntity>
                {
                    new PlayerEntity { Id = "pl_LM3A7T", Name = "Ben Mason", Offense = 3, Defense = 2 },
                    new PlayerEntity { Id = "pl_MN5B1Y", Name = "Kyle Ross", Offense = 2, Defense = 3 },
                    new PlayerEntity { Id = "pl_NO7C5K", Name = "Tom Lane", Offense = 4, Defense = 3 },
                    new PlayerEntity { Id = "pl_OP9D6J", Name = "Aaron Pike", Offense = 3, Defense = 1 },
                    new PlayerEntity { Id = "pl_PQ2E9L", Name = "Mason Reed", Offense = 2, Defense = 4 },
                }
            }
        };
    }
}



