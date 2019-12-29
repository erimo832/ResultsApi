using NUnit.Framework;
using ResultManager.Managers;
using ResultManager.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Tests.Managers
{
    public class LeaderboardManagerTests
    {
        [SetUp]
        public void Setup()
        {
        }


        [Test]
        public void Managers_LeaderboardManager_Should_HandleEmptySerie()
        {
            var serie = new Serie
            {
                Name = "test1",
                Rounds = new List<Round>()
            };

            var manager = new LeaderboardManager();

            var value = manager.GetHcpLeaderboard(serie);

            Assert.IsTrue(value.Count == 0);

            Assert.Pass();
        }

        [Test]
        public void Managers_LeaderboardManager_Should_CalculateTotalScoreScore_Scenario_1_0()
        {
            var serie = new Serie
            { 
                Name = "test1",
                Settings = new SeriesSetting { RoundsToCount = 2 },
                Rounds = new List<Round>
                { 
                    new Round 
                    { 
                        EventName = "1",
                        Results = new List<PlayerResult>
                        {
                            new PlayerResult { FirstName = "John", LastName = "Smith", HcpScore = 59}
                        }
                    }
                }
            };

            var manager = new LeaderboardManager();

            var value = manager.GetHcpLeaderboard(serie);

            Assert.IsTrue(value.Count == 1);
            Assert.IsTrue(value[0].TotalHcpScore == 59.0);

            Assert.Pass();
        }

        [Test]
        public void Managers_LeaderboardManager_Should_CalculateTotalScoreScore_Scenario_1_1()
        {
            var serie = new Serie
            {
                Name = "test1",
                Settings = new SeriesSetting { RoundsToCount = 2 },
                Rounds = new List<Round>
                {
                    new Round
                    {
                        EventName = "1",
                        Results = new List<PlayerResult>
                        {
                            new PlayerResult { FirstName = "John", LastName = "Smith", HcpScore = 50, Points = 99.1 },
                            new PlayerResult { FirstName = "Jane", LastName = "Smith", HcpScore = 48, Points = 100.1},
                        }
                    }
                }
            };

            var manager = new LeaderboardManager();

            var value = manager.GetHcpLeaderboard(serie);

            Assert.IsTrue(value.Count == 2);
            Assert.IsTrue(value[0].TotalHcpScore == 48.0);
            Assert.IsTrue(value[0].TotalPoints == 100.1);

            Assert.Pass();
        }

        [Test]
        public void Managers_LeaderboardManager_Should_CalculateTotalScoreScore_Scenario_1_2()
        {
            var serie = new Serie
            {
                Name = "test1",
                Settings = new SeriesSetting { RoundsToCount = 2 },
                Rounds = new List<Round>
                {
                    new Round
                    {
                        EventName = "1",
                        Results = new List<PlayerResult>
                        {
                            new PlayerResult { FirstName = "John", LastName = "Smith", HcpScore = 50, Points = 99.1 },
                            new PlayerResult { FirstName = "Jane", LastName = "Smith", HcpScore = 48, Points = 100.1},
                        }
                    },
                    new Round
                    {
                        EventName = "2",
                        Results = new List<PlayerResult>
                        {
                            new PlayerResult { FirstName = "John", LastName = "Smith", HcpScore = 50, Points = 99.1 },
                            new PlayerResult { FirstName = "Agent", LastName = "Smith", HcpScore = 48, Points = 100.1},
                        }
                    },
                }
            };

            var manager = new LeaderboardManager();

            var value = manager.GetHcpLeaderboard(serie);

            Assert.IsTrue(value.Count == 3);
            Assert.IsTrue(value[0].TotalHcpScore == 100);
            Assert.IsTrue(value[0].TotalPoints == 198.2);

            Assert.Pass();
        }


        [Test]
        public void Managers_LeaderboardManager_Should_Calculate_With_RoundsToCountScoreSet_Scenario_1_0()
        {
            var serie = new Serie
            {
                Name = "test1",
                Settings = new SeriesSetting { RoundsToCount = 1 },
                Rounds = new List<Round>
                {
                    new Round
                    {
                        EventName = "1",
                        Results = new List<PlayerResult>
                        {
                            new PlayerResult { FirstName = "John", LastName = "Smith", HcpScore = 51, Points = 99.1 },
                            new PlayerResult { FirstName = "Jane", LastName = "Smith", HcpScore = 48, Points = 100.1},
                        }
                    },
                    new Round
                    {
                        EventName = "2",
                        Results = new List<PlayerResult>
                        {
                            new PlayerResult { FirstName = "John", LastName = "Smith", HcpScore = 50, Points = 99.1 },
                            new PlayerResult { FirstName = "Agent", LastName = "Smith", HcpScore = 48, Points = 100.1},
                        }
                    },
                }
            };

            var manager = new LeaderboardManager();

            var value = manager.GetHcpLeaderboard(serie);

            Assert.IsTrue(value.Count == 3);
            Assert.IsTrue(value[0].TotalHcpScore == 48);
            Assert.IsTrue(value[0].TotalPoints == 100.1);
            Assert.IsTrue(value[0].Place == 1);
            
            Assert.IsTrue(value[1].TotalHcpScore == 48);
            Assert.IsTrue(value[1].TotalPoints == 100.1);
            Assert.IsTrue(value[1].Place == 1);

            Assert.IsTrue(value[2].TotalHcpScore == 50);
            Assert.IsTrue(value[2].TotalPoints == 99.1);
            Assert.IsTrue(value[2].Place == 3);

            Assert.Pass();
        }
    }
}
