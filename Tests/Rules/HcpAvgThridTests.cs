using NUnit.Framework;
using ResultManager.Model;
using ResultManager.Rules;
using System;
using System.Collections.Generic;
using System.Text;
using Tests.Model.ObjectMother;

namespace Tests.Rules
{
    public class HcpAvgThridTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Rules_HcpAvgThrid_Should_CalculateHcp_NoRounds()
        {
            var player = PlayerObjectMother.GetPlayer();
            player.Rounds = new List<Round>();

            var rule = new RuleAvgThirdFloored() { TotalRounds = 18 };

            var value = rule.CalculateAvgScore(player);

            Assert.IsTrue(value == 0.0);

            Assert.Pass();
        }

        [Test]
        public void Rules_HcpAvgThrid_Should_CalculateHcp_NotAll_Scenario_1_0()
        {
            var player = PlayerObjectMother.GetPlayer();
            player.Rounds = RoundObjectMother.GetRounds(new List<Tuple<DateTime, int>>
            {
                new Tuple<DateTime, int>(DateTime.Parse("2020-01-01"), 59)
            });


            var rule = new RuleAvgThirdFloored() { TotalRounds = 18 };

            var value = rule.CalculateAvgScore(player);

            Assert.IsTrue(value == 59.0);

            Assert.Pass();
        }

        [Test]
        public void Rules_HcpAvgThrid_Should_CalculateHcp_NotAll_Scenario_1_1()
        {
            var player = PlayerObjectMother.GetPlayer();
            player.Rounds = RoundObjectMother.GetRounds(new List<Tuple<DateTime, int>>
            {
                new Tuple<DateTime, int>(DateTime.Parse("2020-01-01"), 59),
                new Tuple<DateTime, int>(DateTime.Parse("2020-01-02"), 60),
                new Tuple<DateTime, int>(DateTime.Parse("2020-01-03"), 61)
            });

            var rule = new RuleAvgThirdFloored() { TotalRounds = 18 };

            var value = rule.CalculateAvgScore(player);

            Assert.IsTrue(value == 59.0);
            
            Assert.Pass();
        }

        [Test]
        public void Rules_HcpAvgThrid_Should_CalculateHcp_NotAll_Scenario_1_2()
        {
            var player = PlayerObjectMother.GetPlayer();
            player.Rounds = RoundObjectMother.GetRounds(new List<Tuple<DateTime, int>>
            {
                new Tuple<DateTime, int>(DateTime.Parse("2020-01-01"), 59),
                new Tuple<DateTime, int>(DateTime.Parse("2020-01-02"), 60),
                new Tuple<DateTime, int>(DateTime.Parse("2020-01-03"), 61),
                new Tuple<DateTime, int>(DateTime.Parse("2020-01-04"), 62)
            });

            var rule = new RuleAvgThirdFloored() { TotalRounds = 18 };

            var value = rule.CalculateAvgScore(player);

            Assert.IsTrue(value == 59.0);

            Assert.Pass();
        }

        [Test]
        public void Rules_HcpAvgThrid_Should_CalculateHcp_NotAll_Scenario_1_3()
        {
            var player = PlayerObjectMother.GetPlayer();
            player.Rounds = RoundObjectMother.GetRounds(new List<Tuple<DateTime, int>>
            {
                new Tuple<DateTime, int>(DateTime.Parse("2020-01-01"), 59),
                new Tuple<DateTime, int>(DateTime.Parse("2020-01-02"), 60),
                new Tuple<DateTime, int>(DateTime.Parse("2020-01-03"), 61),
                new Tuple<DateTime, int>(DateTime.Parse("2020-01-04"), 62),
                new Tuple<DateTime, int>(DateTime.Parse("2020-01-04"), 62)
            });

            var rule = new RuleAvgThirdFloored() { TotalRounds = 18 };

            var value = rule.CalculateAvgScore(player);

            Assert.IsTrue(value == 59.0);

            Assert.Pass();
        }

        [Test]
        public void Rules_HcpAvgThrid_Should_CalculateHcp_NotAll_Scenario_1_4()
        {
            var player = PlayerObjectMother.GetPlayer();
            player.Rounds = RoundObjectMother.GetRounds(new List<Tuple<DateTime, int>>
            {
                new Tuple<DateTime, int>(DateTime.Parse("2020-01-01"), 59),
                new Tuple<DateTime, int>(DateTime.Parse("2020-01-02"), 60),
                new Tuple<DateTime, int>(DateTime.Parse("2020-01-03"), 61),
                new Tuple<DateTime, int>(DateTime.Parse("2020-01-04"), 62),
                new Tuple<DateTime, int>(DateTime.Parse("2020-01-04"), 63),
                new Tuple<DateTime, int>(DateTime.Parse("2020-01-04"), 64)
            });

            var rule = new RuleAvgThirdFloored() { TotalRounds = 18 };

            var value = rule.CalculateAvgScore(player);

            Assert.IsTrue(value == 59.5);

            Assert.Pass();
        }

        [Test]
        public void Rules_HcpAvgThrid_Should_CalculateHcp_All_Scenario_1_0()
        {
            var player = PlayerObjectMother.GetPlayer();
            player.Rounds = RoundObjectMother.GetRounds(new List<Tuple<DateTime, int>>
            {
                new Tuple<DateTime, int>(DateTime.Parse("2020-01-01"), 59),
                new Tuple<DateTime, int>(DateTime.Parse("2020-01-02"), 60),
                new Tuple<DateTime, int>(DateTime.Parse("2020-01-03"), 61)
            });

            var rule = new RuleAvgThirdFloored() { TotalRounds = 3 };

            var value = rule.CalculateAvgScore(player);

            Assert.IsTrue(value == 59.0);

            Assert.Pass();
        }


        [Test]
        public void Rules_HcpAvgThrid_Should_CalculateHcp_All18_Scenario_1_1()
        {
            var player = PlayerObjectMother.GetPlayer();
            player.Rounds = RoundObjectMother.GetRounds(new List<Tuple<DateTime, int>>
            {
                new Tuple<DateTime, int>(DateTime.Parse("2020-01-01"), 59),
                new Tuple<DateTime, int>(DateTime.Parse("2020-01-02"), 60),
                new Tuple<DateTime, int>(DateTime.Parse("2020-01-03"), 61),
                new Tuple<DateTime, int>(DateTime.Parse("2020-01-04"), 62)
            });

            var rule = new RuleAvgThirdFloored() { TotalRounds = 3 };

            var value = rule.CalculateAvgScore(player);

            Assert.IsTrue(value == 60.0);

            Assert.Pass();
        }

        //TODO order by date
    }
}
