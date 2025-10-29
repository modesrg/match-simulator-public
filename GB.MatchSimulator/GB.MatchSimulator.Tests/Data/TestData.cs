using GB.MatchSimulator.DAL.Entities;
using GB.MatchSimulator.Models;

namespace GB.MatchSimulator.Data;

public static class TestData
{
    public static readonly string TeamA = "Test_Team_A"; // Strongest
    public static readonly string TeamB = "Test_Team_B"; // Mid-Strong
    public static readonly string TeamC = "Test_Team_C"; // Mid-Weak
    public static readonly string TeamD = "Test_Team_D"; // Weakest

    public static List<TeamEntity> GetTestTeams()
    {
        //These are similar to the ones in DummyTeams but with stronger Strong/Weak stats to overcome randomness
        return new List<TeamEntity>
        {
            new TeamEntity
            {
                Id = "te_A7X9B2",
                Name = TeamA,
                Players = new List<PlayerEntity>
                {
                    new PlayerEntity { Id = "pl_QW3E8A", Name = "Alex Hunter", Offense = 10, Defense = 10 },
                    new PlayerEntity { Id = "pl_DF7P2K", Name = "Marcus Vale", Offense = 10, Defense = 10 },
                    new PlayerEntity { Id = "pl_HJ2N5R", Name = "Leo Costa", Offense = 10, Defense = 10 },
                    new PlayerEntity { Id = "pl_LK3Z9T", Name = "Chris Vaughn", Offense = 10, Defense = 10 },
                    new PlayerEntity { Id = "pl_PO6M4U", Name = "Jordan Reese", Offense = 10, Defense = 10 },
                }
            },

            new TeamEntity
            {
                Id = "te_B5Y7L4",
                Name = TeamB,
                Players = new List<PlayerEntity>
                {
                    new PlayerEntity { Id = "pl_AZ8L2K", Name = "Ethan Cole", Offense = 5, Defense = 5 },
                    new PlayerEntity { Id = "pl_BX5M9N", Name = "Rafael Cruz", Offense = 5, Defense = 5 },
                    new PlayerEntity { Id = "pl_CK1V4S", Name = "Oliver Grant", Offense = 5, Defense = 5 },
                    new PlayerEntity { Id = "pl_DM2J7R", Name = "Niko Alvarez", Offense = 5, Defense = 5 },
                    new PlayerEntity { Id = "pl_EN9P3W", Name = "Toby Carter", Offense = 5, Defense = 5 },
                }
            },

            new TeamEntity
            {
                Id = "te_C3V5P6",
                Name = TeamC,
                Players = new List<PlayerEntity>
                {
                    new PlayerEntity { Id = "pl_FG1S8T", Name = "Liam Frost", Offense = 4, Defense = 4 },
                    new PlayerEntity { Id = "pl_GH4D7E", Name = "Noah James", Offense = 4, Defense = 4 },
                    new PlayerEntity { Id = "pl_HJ7F2R", Name = "Eli Barrett", Offense = 4, Defense = 4 },
                    new PlayerEntity { Id = "pl_JK2L8Q", Name = "Sam Harper", Offense = 4, Defense = 4 },
                    new PlayerEntity { Id = "pl_KL9N6Z", Name = "Ryan Doyle", Offense = 4, Defense = 4 },
                }
            },

            new TeamEntity
            {
                Id = "te_D2F3M9",
                Name = TeamD,
                Players = new List<PlayerEntity>
                {
                    new PlayerEntity { Id = "pl_LM3A7T", Name = "Ben Mason", Offense = 1, Defense = 1 },
                    new PlayerEntity { Id = "pl_MN5B1Y", Name = "Kyle Ross", Offense = 1, Defense = 1 },
                    new PlayerEntity { Id = "pl_NO7C5K", Name = "Tom Lane", Offense = 1, Defense = 1 },
                    new PlayerEntity { Id = "pl_OP9D6J", Name = "Aaron Pike", Offense = 1, Defense = 1 },
                    new PlayerEntity { Id = "pl_PQ2E9L", Name = "Mason Reed", Offense = 1, Defense = 1 },
                }
            }
        };
    }

    public static List<MatchResult> GetTestMatchResults()
    {
        return new List<MatchResult>(){ new MatchResult
        {
            Home = new TeamResult
            {
                Name = TeamA,
                Score = 3
            },
            Away = new TeamResult
            {
                Name = TeamB,
                Score = 2
            }
        },
        new MatchResult
        {
            Home = new TeamResult
            {
                Name = TeamC,
                Score = 1
            },
            Away = new TeamResult
            {
                Name = TeamD,
                Score = 2
            }
        }
        };
    }

    public static List<Fixture> GetTestFixtures()
    {
        return new List<Fixture>()
        {
            new Fixture() { Home = TeamA, Away = TeamB },
            new Fixture() { Home = TeamC, Away = TeamD }
        };
    }

    public static List<Round> GetTestRounds()
    {
        var testFixture1 = GetTestFixtures();

        return new List<Round>()
        {

        new Round()
        {
            Name= "Test_Round_1",
            Matches = new List<Fixture>()
        {
                    new Fixture() { Home = TeamA, Away = TeamB },
                    new Fixture() { Home = TeamC, Away = TeamD }
                }
        },
        new Round()
        {
            Name= "Test_Round_2",
            Matches = new List<Fixture>()
                {
                    new Fixture() { Home = TeamB, Away = TeamA },
                    new Fixture() { Home = TeamD, Away = TeamC }
                }
        },
        new Round()
        {
            Name= "Test_Round_3",
            Matches = new List<Fixture>()
                {
                    new Fixture() { Home = TeamD, Away = TeamB },
                    new Fixture() { Home = TeamC, Away = TeamA }
                }
        }

        };
    }

    public static List<RoundResult> GetTestRoundResults()
    {

        return new List<RoundResult>
    {
        new RoundResult
        {
            Name = "Test_Round_1",
            Results = new List<MatchResult>
            {
                new MatchResult
                {
                    Home = new TeamResult { Name = TeamA, Score = 3 },
                    Away = new TeamResult { Name = TeamB, Score = 1 }
                },
                new MatchResult
                {
                    Home = new TeamResult { Name = TeamC, Score = 2 },
                    Away = new TeamResult { Name = TeamD, Score = 0 }
                }
            }
        },
        new RoundResult
        {
            Name = "Test_Round_2",
            Results = new List<MatchResult>
            {
                new MatchResult
                {
                    Home = new TeamResult { Name = TeamA, Score = 4 },
                    Away = new TeamResult { Name = TeamC, Score = 2 }
                },
                new MatchResult
                {
                    Home = new TeamResult { Name = TeamB, Score = 1 },
                    Away = new TeamResult { Name = TeamD, Score = 0 }
                }
            }
        },
        new RoundResult
        {
            Name = "Test_Round_3",
            Results = new List<MatchResult>
            {
                new MatchResult
                {
                    Home = new TeamResult { Name = TeamA, Score = 2 },
                    Away = new TeamResult { Name = TeamD, Score = 0 }
                },
                new MatchResult
                {
                    Home = new TeamResult { Name = TeamB, Score = 1 },
                    Away = new TeamResult { Name = TeamC, Score = 1 }
                }
            }
        }
    };
    }


}



